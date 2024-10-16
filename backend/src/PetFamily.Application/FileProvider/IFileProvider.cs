﻿using CSharpFunctionalExtensions;
using PetFamily.Domain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetFamily.Application.FileProvider;
public interface IFileProvider
{
    Task<Result<string, Error>> UploadFile(FileData fileData, CancellationToken token = default);
}
