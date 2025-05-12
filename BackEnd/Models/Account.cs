using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BackEnd.Models;

[Table("Accounts")]
public class Account
{
    [Key] public int AccountId { get; set; }
    public required string Username { get; set; }
    public required string Password { get; set; }
}
