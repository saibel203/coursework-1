﻿namespace UserAuthentication.Models;

public class UserModel
{
    public string UserName { get; set; } = string.Empty;
    public string? Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string? Role { get; set; } = "User";
}
