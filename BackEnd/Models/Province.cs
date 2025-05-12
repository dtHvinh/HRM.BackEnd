using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BackEnd.Models;

[Table("Provinces")]
public class Province
{
    [Key] public int ProvinceId { get; set; }
    public required string ProvinceName { get; set; }
}
