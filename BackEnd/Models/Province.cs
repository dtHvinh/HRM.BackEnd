using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BackEnd.Models;

[Table("Provinces")]
[Index(nameof(ProvinceName), IsUnique = true)]
public class Province
{
    [Key] public int ProvinceId { get; set; }
    public required string ProvinceName { get; set; }
}
