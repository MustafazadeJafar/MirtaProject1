using System.ComponentModel.DataAnnotations;

namespace Server01.Models;

public class RRPsessions
{
    public int ID { get; set; }

    [Required, StringLength(7), DataType("VARCHAR")]
    public string Code { get; set; }
}
