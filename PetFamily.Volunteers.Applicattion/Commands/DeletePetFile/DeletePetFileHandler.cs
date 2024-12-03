using CSharpFunctionalExtensions;
using Microsoft.Extensions.Logging;
using PetFamily.Core.Abstraction;
using PetFamily.Core.FileProvider;
using PetFamily.Core.Messaging;
using PetFamily.SharedKernel;
using PetFamily.SharedKernel.ValueObjects;
using PetFamily.Volunteers.Application;
using PetFamily.Volunteers.Domain.PetsValueObjects;

namespace PetFamily.Volunteers.Application.Commands.DeletePetFile;
public class DeletePetFileHandler : ICommandHandler<Guid, DeletePetFileCommand>
{
    private const string BUCKET_NAME = "photos";

    private readonly IWriteVolunteersRepository _volunteersRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IFileProvider _fileProvider;
    private readonly IMessageQueue<IEnumerable<FileInfos>> _messageQueue;
    private readonly ILogger<DeletePetFileHandler> _logger;

    public DeletePetFileHandler(
        IWriteVolunteersRepository volunteersRepository,
        IUnitOfWork unitOfWork,
        IFileProvider fileProvider,
        IMessageQueue<IEnumerable<FileInfos>> messageQueue,
        ILogger<DeletePetFileHandler> logger)
    {
        _volunteersRepository = volunteersRepository;
        _unitOfWork = unitOfWork;
        _fileProvider = fileProvider;
        _messageQueue = messageQueue;
        _logger = logger;
    }
    public async Task<Result<Guid, ErrorList>> Handle(DeletePetFileCommand command, CancellationToken token)
    {
        var volunteerResult = await _volunteersRepository.GetById(command.VolunteerId, token);
        if (volunteerResult.IsFailure)
            return volunteerResult.Error.ToErrorList();

        var petResult = volunteerResult.Value.GetPetById(command.PetId);
        if (petResult.IsFailure)
            return petResult.Error.ToErrorList();

        var filePath = FilePath.Create(command.FilePath).Value;

        var petFiles = new PetFiles(filePath);

        var fileInfos = new FileInfos(filePath, BUCKET_NAME);

        await _fileProvider.RemoveFiles(fileInfos, token);

        petResult.Value.RemoveFiles(petFiles);

        await _unitOfWork.SaveChanges(token);

        _logger.LogInformation("Deleted the file {filePath} from the animal", filePath);

        return petResult.Value.Id.Value;
    }
}
