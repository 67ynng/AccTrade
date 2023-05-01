using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Windows.Media;

public class Form
{
    [Key]
    public int Id { get; set; }
    public string username { get; set; }
    public string title { get; set; }
    public byte[]? ImageData { get; set; }
    public string? GameCategory { get; set; }
    public string? Describe { get; set; }
    public int? Price { get; set; }
}