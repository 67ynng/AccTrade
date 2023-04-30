﻿using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

public class Login
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public int Balance { get; set; }
    public string? Username { get; set; }
    public string? Password { get; set; }
    public bool? isAdmin { get; set; }
    public string? Email { get; set; }
   
}