using System;
using System.ComponentModel.DataAnnotations;

namespace TMS.Domain.Entities;

public class UserTask
{
    public Guid Id { get; set;}
    
    [MaxLength(200)]
    public string Title { get; set; } = string.Empty;

    [MaxLength(2000)]
    public string Description { get; set; } = string.Empty;
    public bool IsCompleted { get; set; }
    public DateTime CreatedAt { get; set;}
    public DateTime? DueDate { get; set;}

    [MaxLength(100)]
    public string OwnerUserId { get; set; } = string.Empty;
}
