using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Travel_agency_Lyapynova.Models;

public partial class User
{
    public int UserId { get; set; }

    public string Login { get; set; } = null!;

    public string Password { get; set; } = null!;

    public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();

    public virtual ICollection<Klient> Klients { get; set; } = new List<Klient>();

    public virtual ICollection<Supplier> Suppliers { get; set; } = new List<Supplier>();

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        List<ValidationResult> errors = new List<ValidationResult>();

        if (string.IsNullOrEmpty(Login) || Login.Length < 5 || Login.Length > 20)
            errors.Add(new ValidationResult("Логин должен быть от 5 до 20 символов."));

        if (string.IsNullOrEmpty(Password) || Password.Length < 8 || Password.Length > 20)
            errors.Add(new ValidationResult("Пароль должен быть от 8 до 20 символов."));

        return errors;
    }
}
