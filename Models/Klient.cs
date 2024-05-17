using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;


namespace Travel_agency_Lyapynova.Models;

public partial class Klient
{
    public int KlientId { get; set; }

    public string Surname { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string? Patronymic { get; set; }

    public string Email { get; set; } = null!;

    public string PhoneNumber { get; set; } = null!;

    public string PassportNumber { get; set; } = null!;

    public string PassportSerias { get; set; } = null!;

    public int UserId { get; set; }

    public virtual ICollection<History> Histories { get; set; } = new List<History>();

    public virtual ICollection<ServiceAgreement> ServiceAgreements { get; set; } = new List<ServiceAgreement>();

    public virtual User User { get; set; } = null!;

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        List<ValidationResult> errors = new List<ValidationResult>();

        if (string.IsNullOrEmpty(Surname) || !Regex.IsMatch(Surname, "^[a-zA-Zа-яА-Я]+$") || Surname.Length < 3 || Surname.Length > 50)
            errors.Add(new ValidationResult("Фамилия должна состоять только из букв и быть от 3 до 50 символов."));

        if (string.IsNullOrEmpty(Name) || !Regex.IsMatch(Name, "^[a-zA-Zа-яА-Я]+$") || Name.Length < 3 || Name.Length > 50)
            errors.Add(new ValidationResult("Имя должно состоять только из букв и быть от 3 до 50 символов."));

        if (!string.IsNullOrEmpty(Patronymic) && (!Regex.IsMatch(Patronymic, "^[a-zA-Zа-яА-Я]+$") || Patronymic.Length < 3 || Patronymic.Length > 50))
            errors.Add(new ValidationResult("Отчество должно состоять только из букв и быть от 3 до 50 символов."));

        if (string.IsNullOrEmpty(PhoneNumber) || PhoneNumber.Length != 11)
            errors.Add(new ValidationResult("Номер телефона должен состоять из 11 символов."));

        if (string.IsNullOrEmpty(PassportNumber) || !Regex.IsMatch(PassportNumber, "^[0-9]{4}$"))
            errors.Add(new ValidationResult("Номер паспорта должен содержать только 4 цифры."));

        if (string.IsNullOrEmpty(PassportSerias) || !Regex.IsMatch(PassportSerias, "^[0-9]{6}$"))
            errors.Add(new ValidationResult("Серия паспорта должна содержать только 6 цифр."));

        if (!string.IsNullOrEmpty(Email))
        {
            bool isValidEmail = new System.ComponentModel.DataAnnotations.EmailAddressAttribute().IsValid(Email);
            if (!isValidEmail)
                errors.Add(new ValidationResult("Некорректный адрес электронной почты."));
        }

        return errors;
    }
}
