using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Minio;
using Minio.AspNetCore;
using PetFamily.Application.Volunteers.CreateVolunteer;
using PetFamily.Infrastructure.Interceptors;
using PetFamily.Infrastructure.Repositories;
using PetFamily.Infrastructure.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.FileProviders;
using PetFamily.Infrastructure.Services;
using IFileProvider = PetFamily.Application.FileProvider.IFileProvider;
using PetFamily.Application.Database;
using PetFamily.Application.Specieses;
using PetFamily.Infrastructure.BackgroundServices;
using PetFamily.Application.Messaging;
using PetFamily.Infrastructure.MessageQueues;
using PetFamily.Application.FileProvider;

namespace PetFamily.Infrastructure;

public static class Inject
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<ApplicationDbContext>();
        services.AddScoped<IVolunteersRepository, VolunteersRepository>();
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
