using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
public class Roles
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public string Role { get; set; }
    public int MembersInThisRole { get; set; }

}