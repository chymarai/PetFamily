using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Minio;
using Minio.AspNetCore;
using PetFamily.Infrastructure.Interceptors;
using PetFamily.Infrastructure.Repositories;
using PetFamily.Infrastructure.Services;
using IFileProvider = PetFamily.Application.FileProvider.IFileProvider;
using PetFamily.Application.Database;
using PetFamily.Application.Specieses;
using PetFamily.Infrastructure.BackgroundServices;
using PetFamily.Application.Messaging;
using PetFamily.Infrastructure.MessageQueues;
using PetFamily.Application.FileProvider;
using PetFamily.Infrastructure.DbContexts;

namespace PetFamily.Infrastructure;

public static class Inject
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<WriteDbContext>();
        services.AddScoped<IReadDbContext, ReadDbContext>();
        services.AddScoped<IReadVolunteersRepository, WriteVolunteersRepository>();
        services.AddScoped<ISpeciesesRepository, SpeciesesRepository>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        services.AddMinio(configuration);
        
        services.AddHostedService<FilesCleanerBackgroundService>();

        services.AddSingleton<SoftDeleteInterceptor>();

        services.AddSingleton<IMessageQueue<IEnumerable<FileInfos>>, InMemoryCleanerMessageQueue<IEnumerable<FileInfos>>>();

        return services;
    }

    private static IServiceCollection AddMinio(
        this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<Options.MinioOptions>(configuration.GetSection(Options.MinioOptions.MINIO));

        services.AddMinio(options =>
        {
            var minioOptions = configuration.GetSection(Options.MinioOptions.MINIO).Get<Options.MinioOptions>()
                               ?? throw new ArgumentNullException("Missing minio configuration");

            options.WithEndpoint(minioOptions.Endpoint);

            options.WithCredentials(minioOptions.AccessKey, minioOptions.SecretKey);

            options.WithSSL(minioOptions.WithSsl);

        });

        services.AddScoped<IFileProvider, MinioProvider>();

        return services;
    }
}
