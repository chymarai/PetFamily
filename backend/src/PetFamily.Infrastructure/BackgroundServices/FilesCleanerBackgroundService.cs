using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using PetFamily.Application.FileProvider;
using PetFamily.Application.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetFamily.Infrastructure.BackgroundServices;
public class FilesCleanerBackgroundService : BackgroundService //запускается в фоне при запуске приложения
{
    private readonly ILogger<FilesCleanerBackgroundService> _logger;
    private readonly IMessageQueue<IEnumerable<FileInfos>> _messageQueue;
    private readonly IServiceProvider _serviceProvider;

    public FilesCleanerBackgroundService(
        ILogger<FilesCleanerBackgroundService> logger,
        IMessageQueue<IEnumerable<FileInfos>> messageQueue,
        IServiceProvider serviceProvider)
    {
        _logger = logger;
        _messageQueue = messageQueue;
        _serviceProvider = serviceProvider;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("FilesCleanerBackgroundService is starting");

        await using var scope = _serviceProvider.CreateAsyncScope();
        
        var fileProvider = scope.ServiceProvider.GetRequiredService<IFileProvider>();
    
        while (!stoppingToken.IsCancellationRequested)
        {
            var fileInfos = await _messageQueue.ReadAsync(stoppingToken);

            foreach (var fileInfo in fileInfos)
            {
                await fileProvider.RemoveFiles(fileInfo, stoppingToken);
            }
        }

        await Task.CompletedTask;
    }
}
