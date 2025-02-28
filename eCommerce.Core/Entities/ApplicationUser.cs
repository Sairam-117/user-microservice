﻿using System;
using System.ComponentModel.DataAnnotations;

public class ApplicationUser
{
    [Key]
    public Guid UserId { get; set; }
    public string? Email { get; set; }
    public string? Password { get; set; }
    public string? PersonName { get; set; }
    public string? Gender { get; set; }
}
