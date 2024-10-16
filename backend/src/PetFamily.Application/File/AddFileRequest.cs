using PetFamily.Application.FileProvider;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetFamily.Application.File;
public record AddFileRequest(Stream Stream, string BucketName, string ObjectName);
