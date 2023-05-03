using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Windows.Media;

public class Form
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public int LoginId { get; set; }
    public string username { get; set; }
    public Nullable<int> PhoneNumber { get; set; }
    public string title { get; set; }
    public byte[]? ImageData { get; set; }
    public string? GameCategory { get; set; }
    public string? Describe { get; set; }
    public int? Price { get; set; }
}