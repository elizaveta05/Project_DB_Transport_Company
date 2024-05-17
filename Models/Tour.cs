using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Travel_agency_Lyapynova.Models;

public partial class Tour
{
    public int TourId { get; set; }

    public string Name { get; set; } = null!;

    public string Descriptions { get; set; } = null!;

    public int Duration { get; set; }

    public int CountryId { get; set; }

    public int CityId { get; set; }

    public decimal Cost { get; set; }

    public int SupplierId { get; set; }

    public virtual City City { get; set; } = null!;

    public virtual Country Country { get; set; } = null!;

    public virtual ICollection<Representative> Representatives { get; set; } = new List<Representative>();

    public virtual ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();

    public virtual Supplier Supplier { get; set; } = null!;

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        List<ValidationResult> errors = new List<ValidationResult>();

        if (string.IsNullOrEmpty(Name) || Name.Length < 2 || Name.Length > 200)
            errors.Add(new ValidationResult("Наименование должно быть от 2 до 200 символов.", new[] { nameof(Name) }));
        if (string.IsNullOrEmpty(Descriptions) || Descriptions.Length < 2 || Descriptions.Length > 255)
            errors.Add(new ValidationResult("Описание должно быть от 2 до 255 символов.", new[] { nameof(Descriptions) }));

        if (Duration <= 0 || Duration > 365)
            errors.Add(new ValidationResult("Продолжительность должна быть больше 0 и не превышать 365 дней.", new[] { nameof(Duration) }));

        if (Cost <= 0)
            errors.Add(new ValidationResult("Стоимость должна быть больше 0.", new[] { nameof(Cost) }));

        return errors;
    }
}
