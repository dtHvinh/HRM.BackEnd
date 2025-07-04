﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BackEnd.Models;

[Table("Employees")]
public class Employee
{
    [Key] public int EmployeeId { get; set; }
    public required string FullName { get; set; }
    public DateTimeOffset DOB { get; set; }
    public Gender Gender { get; set; }
    public required string Email { get; set; }
    public required string Phone { get; set; }

    public EmployeeAddress? EmployeeAddress { get; set; }
    public EmployeeBenefit? EmployeeBenefit { get; set; }
    public ICollection<EmployeeSalary>? EmployeeSalaries { get; set; }
    public ICollection<EmployeeDepartment>? EmployeeDepartments { get; set; }
}

public enum Gender
{
    Male,
    Female,
    Other
}