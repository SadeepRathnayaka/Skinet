using System;
using System.ComponentModel.DataAnnotations;

namespace API.DTOs;

public class RegisterDto
{
    // FirstName and LastName are additiional fields which we add
    [Required]
    public string FirstName { get; set; } = string.Empty;  // even we set this as empty in the initiation, 
                                                           //this field should have a value, otherwise there'll be an error

    [Required]
    public string LastName { get; set; } = string.Empty;


    // Email and Passwords are required anyway for the Identity. So the validation of those attributes will be done by Identity.
    [Required]
    public string Email { get; set; } = string.Empty;

    [Required]
    public string Password { get; set; } = string.Empty;

}
