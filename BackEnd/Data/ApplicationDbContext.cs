#nullable disable

using BackEnd.Models;
using Microsoft.EntityFrameworkCore;

namespace BackEnd.Data;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
{
    public DbSet<Employee> Employees { get; set; }
    public DbSet<Department> Departments { get; set; }
    public DbSet<Notification> Notifications { get; set; }
    public DbSet<Position> Positions { get; set; }
    public DbSet<Salary> Salaries { get; set; }
    public DbSet<Account> Accounts { get; set; }
    public DbSet<Insurance> Insurances { get; set; }
    public DbSet<Allowance> Allowances { get; set; }
    public DbSet<Ward> Wards { get; set; }
    public DbSet<Province> Province { get; set; }


    public DbSet<EmployeeAddress> EmployeeAddresses { get; set; }
    public DbSet<EmployeeBenefit> EmployeeBenefits { get; set; }
    public DbSet<EmployeeDepartment> EmployeeDepartments { get; set; }
    public DbSet<EmployeeNotification> EmployeeNotifications { get; set; }
    public DbSet<EmployeeSalary> EmployeeSalaries { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<EmployeeAddress>().Navigation(e => e.Province).AutoInclude();
        modelBuilder.Entity<EmployeeAddress>().Navigation(e => e.Ward).AutoInclude();

        modelBuilder.Entity<EmployeeDepartment>().Navigation(e => e.Position).AutoInclude();
        modelBuilder.Entity<EmployeeDepartment>().Navigation(e => e.Department).AutoInclude();
    }
}
