﻿using DomainModel.Domain;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModel.Validation
{
    public class ProvinceValidator : AbstractValidator<Province>
    {
        public ProvinceValidator()
        {

        }
    }
}
