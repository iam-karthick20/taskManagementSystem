using System;
using System.ComponentModel.DataAnnotations;

namespace TMS.Domain.Entities;

public class User
{
    [Key]
    public Guid UserId { get; set; }

    public string? FirstName { get; set; }
    public string? LastName { get; set; }

    [MaxLength(100)]
    public string? Username { get; set; }

    [MaxLength(2000)]
    public string? PasswordHash { get; set; }

    [MaxLength(20)]
    public string? Role { get; set; }

    [MaxLength(100)]
    public string? EmailAddress { get; set; }
    public bool IsActive { get; set; }
    public DateTime CreatedOn { get; set; }
}