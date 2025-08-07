using System;
using Microsoft.EntityFrameworkCore;
using TMS.Domain.Entities;

namespace TMS.Infrastructure.Data;

public class ProjectDbContext(DbContextOptions<ProjectDbContext> options) : DbContext(options)
{
    public DbSet<User> Users { get; set; }
    public DbSet<UserLoginHistory> UsersLoginHistory { get; set; }
    public DbSet<UserTask> UsersTask { get; set; }
}
