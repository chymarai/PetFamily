﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetFamily.Domain
{
    public interface IRequisite
    {
        public string Name { get; set; }
        public string Description { get; set; } 
    }
}
