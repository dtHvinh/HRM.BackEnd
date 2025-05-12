using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BackEnd.Models;

[Table("Wards")]
[Index(nameof(WardName), IsUnique = true)]
public class Ward
{
    [Key] public int WardId { get; set; }
    public required string WardName { get; set; }
}
