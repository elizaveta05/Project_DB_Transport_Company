using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace Travel_agency_Lyapynova.Models;

public partial class Supplier
{
    public int SupplierId { get; set; }

    public string Name { get; set; } = null!;

    public string PhoneNumber { get; set; } = null!;

    public string Address { get; set; } = null!;

    public int? UserId { get; set; }

    public virtual ICollection<CooperationAgreement> CooperationAgreements { get; set; } = new List<CooperationAgreement>();

    public virtual ICollection<Tour> Tours { get; set; } = new List<Tour>();

    public virtual User? User { get; set; }

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        List<ValidationResult> errors = new List<ValidationResult>();

        if (string.IsNullOrEmpty(Name) || !Regex.IsMatch(Name, "^[a-zA-Zа-яА-Я]+$") || Name.Length < 3 || Name.Length > 50)
            errors.Add(new ValidationResult("Наименование должно состоять только из букв и быть от 3 до 50 символов.", new[] { nameof(Name) }));

        if (string.IsNullOrEmpty(Address) || !Regex.IsMatch(Name, "^[a-zA-Zа-яА-Я]+$") || Address.Length < 3 || Address.Length > 50)
            errors.Add(new ValidationResult("Адрес должен состоять только из букв и быть от 3 до 50 символов.", new[] { nameof(Address) }));

        if (string.IsNullOrEmpty(PhoneNumber) || PhoneNumber.Length != 11)
            errors.Add(new ValidationResult("Номер телефона должен состоять из 11 символов", new[] { nameof(PhoneNumber) }));

        return errors;
    }
}
