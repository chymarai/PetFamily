﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetFamily.Application.DTOs;
public class SpeciesDto
{
    public Guid Id { get; set; }
    public string SpeciesName { get; set; } = string.Empty;
}

