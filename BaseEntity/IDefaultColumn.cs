﻿using System.ComponentModel.DataAnnotations;

namespace SandboxRazor.BaseEntity;

public interface IDefaultColumn
{
    [Required]
    [StringLength(15)]
    public string Code { get; set; }

    [Required]
    [StringLength(255)]
    public string Name { get; set; }
}
