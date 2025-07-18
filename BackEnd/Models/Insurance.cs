﻿using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BackEnd.Models;

[Table("Insurances")]
[Index(nameof(InsuranceName), IsUnique = true)]
public class Insurance
{
    [Key] public int InsuranceId { get; set; }
    public required string InsuranceName { get; set; }
    public decimal InsuranceCoefficient { get; set; }
}
