﻿using System.ComponentModel.DataAnnotations;

namespace SandboxRazor.Models.Pharmacy
{
    public class Supplier : BaseEntity.BaseCodeName
    {
        public int PhoneNumber { get; set; }

        [MaxLength(255)]
        public string Address { get; set; } = default!;
    }
}