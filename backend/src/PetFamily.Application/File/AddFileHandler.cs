using CSharpFunctionalExtensions;
using PetFamily.Application.FileProvider;
using PetFamily.Domain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetFamily.Application.File;
//public class AddFileHandler
//{
//    private readonly IFileProvider _fileProvider;

//    public AddFileHandler(IFileProvider fileProvider)
//    {
//        _fileProvider = fileProvider;
//    }

//    public async Task<Result<string, Error>> Handle(AddFileRequest request, CancellationToken token = default)
//    {
//        var fileData = new FileData(request.Stream, request.FilePath, request.ObjectName);

//        return await _fileProvider.UploadFile(fileData, token);
//    }
//}
