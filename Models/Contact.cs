﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MVC_Core.Models;

public partial class Contact
{
    [Key]

    public int Id { get; set; }

    public string? FullName { get; set; }

    public string? Email { get; set; }

    public string? Phone { get; set; }

    public string Message { get; set; } = null!;
}
