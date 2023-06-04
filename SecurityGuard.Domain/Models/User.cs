﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityGuard.Domain.Models
{
    public class User
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Patronomic { get; set; } = string.Empty;

        public string JobTitle { get; set; } = string.Empty;
        public DateTime BirthDay { get; set; } = DateTime.MinValue;
        public string HashedPassword { get; set; } = string.Empty;

        public override string ToString()
        {
            return $"{FirstName} + \" \" +{LastName} + \" \" + {Patronomic}";
        }
    }
}
