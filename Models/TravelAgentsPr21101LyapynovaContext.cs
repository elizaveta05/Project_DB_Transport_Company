using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Travel_agency_Lyapynova.Models;

public partial class TravelAgentsPr21101LyapynovaContext : DbContext
{
    private static TravelAgentsPr21101LyapynovaContext Model = null;

    public TravelAgentsPr21101LyapynovaContext()
    {
    }

    public TravelAgentsPr21101LyapynovaContext(DbContextOptions<TravelAgentsPr21101LyapynovaContext> options)
        : base(options)
    {
    }

    public static TravelAgentsPr21101LyapynovaContext GetContext()
    {
        if (Model == null)
        {
            Model = new TravelAgentsPr21101LyapynovaContext();
        }
        return Model;
    }
    public virtual DbSet<City> Cities { get; set; }

    public virtual DbSet<CooperationAgreement> CooperationAgreements { get; set; }

    public virtual DbSet<Country> Countries { get; set; }

    public virtual DbSet<Employee> Employees { get; set; }

    public virtual DbSet<Excursion> Excursions { get; set; }

    public virtual DbSet<ExcursionsOrder> ExcursionsOrders { get; set; }

    public virtual DbSet<History> Histories { get; set; }

    public virtual DbSet<HistoryRegistration> HistoryRegistrations { get; set; }

    public virtual DbSet<Hotel> Hotels { get; set; }

    public virtual DbSet<Klient> Klients { get; set; }

    public virtual DbSet<Payment> Payments { get; set; }

    public virtual DbSet<PaymentMethod> PaymentMethods { get; set; }

    public virtual DbSet<Position> Positions { get; set; }

    public virtual DbSet<RegistrationOfDocument> RegistrationOfDocuments { get; set; }

    public virtual DbSet<RegistrationOfInsurance> RegistrationOfInsurances { get; set; }

    public virtual DbSet<Representative> Representatives { get; set; }

    public virtual DbSet<Reservation> Reservations { get; set; }

    public virtual DbSet<ServiceAgreement> ServiceAgreements { get; set; }

    public virtual DbSet<StatusRegistration> StatusRegistrations { get; set; }

    public virtual DbSet<Supplier> Suppliers { get; set; }

    public virtual DbSet<Tour> Tours { get; set; }

    public virtual DbSet<Transfer> Transfers { get; set; }

    public virtual DbSet<TransferOrder> TransferOrders { get; set; }

    public virtual DbSet<Transport> Transports { get; set; }

    public virtual DbSet<TypeOfInsurance> TypeOfInsurances { get; set; }

    public virtual DbSet<TypeOfTransport> TypeOfTransports { get; set; }

    public virtual DbSet<TypeOfVisa> TypeOfVisas { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<VisaProcessing> VisaProcessings { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=Travel_Agents_PR-21.101_Lyapynova;Username=postgres;Password=Elizaveta05;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<City>(entity =>
        {
            entity.HasKey(e => e.CityId).HasName("cities_pkey");

            entity.ToTable("cities");

            entity.Property(e => e.CityId).HasColumnName("city_id");
            entity.Property(e => e.CountryId).HasColumnName("country_id");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name");

            entity.HasOne(d => d.Country).WithMany(p => p.Cities)
                .HasForeignKey(d => d.CountryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("cities_country_id_fkey");
        });

        modelBuilder.Entity<CooperationAgreement>(entity =>
        {
            entity.HasKey(e => e.ContractId).HasName("cooperation_agreement_pkey");

            entity.ToTable("cooperation_agreement");

            entity.Property(e => e.ContractId).HasColumnName("contract_id");
            entity.Property(e => e.EndDate).HasColumnName("end_date");
            entity.Property(e => e.StartDate).HasColumnName("start_date");
            entity.Property(e => e.SupplierId).HasColumnName("supplier_id");

            entity.HasOne(d => d.Supplier).WithMany(p => p.CooperationAgreements)
                .HasForeignKey(d => d.SupplierId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("cooperation_agreement_supplier_id_fkey");
        });

        modelBuilder.Entity<Country>(entity =>
        {
            entity.HasKey(e => e.CountryId).HasName("countries_pkey");

            entity.ToTable("countries");

            entity.Property(e => e.CountryId).HasColumnName("country_id");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name");
        });

        modelBuilder.Entity<Employee>(entity =>
        {
            entity.HasKey(e => e.EmployeeId).HasName("employees_pkey");

            entity.ToTable("employees");

            entity.Property(e => e.EmployeeId).HasColumnName("employee_id");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("name");
            entity.Property(e => e.Patronymic)
                .HasMaxLength(50)
                .HasColumnName("patronymic");
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(11)
                .HasColumnName("phone_number");
            entity.Property(e => e.PositionId).HasColumnName("position_id");
            entity.Property(e => e.Surname)
                .HasMaxLength(50)
                .HasColumnName("surname");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.Position).WithMany(p => p.Employees)
                .HasForeignKey(d => d.PositionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("employees_position_id_fkey");

            entity.HasOne(d => d.User).WithMany(p => p.Employees)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("employees_user_id_fkey");
        });

        modelBuilder.Entity<Excursion>(entity =>
        {
            entity.HasKey(e => e.ExcursionId).HasName("excursions_pkey");

            entity.ToTable("excursions");

            entity.Property(e => e.ExcursionId).HasColumnName("excursion_id");
            entity.Property(e => e.CityId).HasColumnName("city_id");
            entity.Property(e => e.CompanyNumberPhone)
                .HasMaxLength(11)
                .HasColumnName("company_number_phone");
            entity.Property(e => e.Cost)
                .HasColumnType("money")
                .HasColumnName("cost");
            entity.Property(e => e.CountryId).HasColumnName("country_id");
            entity.Property(e => e.Description)
                .HasMaxLength(255)
                .HasColumnName("description");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name");

            entity.HasOne(d => d.City).WithMany(p => p.Excursions)
                .HasForeignKey(d => d.CityId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("city");

            entity.HasOne(d => d.Country).WithMany(p => p.Excursions)
                .HasForeignKey(d => d.CountryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("country");
        });

        modelBuilder.Entity<ExcursionsOrder>(entity =>
        {
            entity.HasKey(e => e.ExcursionOrderId).HasName("excursions_order_pkey");

            entity.ToTable("excursions_order");

            entity.Property(e => e.ExcursionOrderId).HasColumnName("excursion_order_id");
            entity.Property(e => e.ContractId).HasColumnName("contract_id");
            entity.Property(e => e.DateOrder).HasColumnName("date_order");
            entity.Property(e => e.ExcursionId).HasColumnName("excursion_id");

            entity.HasOne(d => d.Contract).WithMany(p => p.ExcursionsOrders)
                .HasForeignKey(d => d.ContractId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("excursions_order_contract_id_fkey");

            entity.HasOne(d => d.Excursion).WithMany(p => p.ExcursionsOrders)
                .HasForeignKey(d => d.ExcursionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("excursions_order_excursion_id_fkey");
        });

        modelBuilder.Entity<History>(entity =>
        {
            entity.HasKey(e => e.HistoryId).HasName("history_pkey");

            entity.ToTable("history");

            entity.Property(e => e.HistoryId).HasColumnName("history_id");
            entity.Property(e => e.KlientId).HasColumnName("klient_id");
            entity.Property(e => e.СontractId).HasColumnName("Сontract_id");

            entity.HasOne(d => d.Klient).WithMany(p => p.Histories)
                .HasForeignKey(d => d.KlientId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("history_klient_id_fkey");

            entity.HasOne(d => d.Сontract).WithMany(p => p.Histories)
                .HasForeignKey(d => d.СontractId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("history_Сontract_id_fkey");
        });

        modelBuilder.Entity<HistoryRegistration>(entity =>
        {
            entity.HasKey(e => e.HistoryId).HasName("history_registration_pkey");

            entity.ToTable("history_registration");

            entity.Property(e => e.HistoryId).HasColumnName("history_id");
            entity.Property(e => e.Date).HasColumnName("date");
            entity.Property(e => e.RegistrationId).HasColumnName("registration_id");
            entity.Property(e => e.StatusId).HasColumnName("status_id");

            entity.HasOne(d => d.Registration).WithMany(p => p.HistoryRegistrations)
                .HasForeignKey(d => d.RegistrationId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("history_registration_registration_id_fkey");

            entity.HasOne(d => d.Status).WithMany(p => p.HistoryRegistrations)
                .HasForeignKey(d => d.StatusId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("history_registration_status_id_fkey");
        });

        modelBuilder.Entity<Hotel>(entity =>
        {
            entity.HasKey(e => e.HotelId).HasName("hotels_pkey");

            entity.ToTable("hotels");

            entity.Property(e => e.HotelId).HasColumnName("hotel_id");
            entity.Property(e => e.Address)
                .HasMaxLength(250)
                .HasColumnName("address");
            entity.Property(e => e.CityId).HasColumnName("city_id");
            entity.Property(e => e.Cost)
                .HasColumnType("money")
                .HasColumnName("cost");
            entity.Property(e => e.CountryId).HasColumnName("country_id");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name");
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(11)
                .HasColumnName("phone_number");
            entity.Property(e => e.Stars)
                .HasMaxLength(5)
                .HasColumnName("stars");

            entity.HasOne(d => d.City).WithMany(p => p.Hotels)
                .HasForeignKey(d => d.CityId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("city");

            entity.HasOne(d => d.Country).WithMany(p => p.Hotels)
                .HasForeignKey(d => d.CountryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("country");
        });

        modelBuilder.Entity<Klient>(entity =>
        {
            entity.HasKey(e => e.KlientId).HasName("klients_pkey");

            entity.ToTable("klients");

            entity.Property(e => e.KlientId).HasColumnName("klient_id");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .HasColumnName("email");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("name");
            entity.Property(e => e.PassportNumber)
                .HasMaxLength(4)
                .HasColumnName("passport_number");
            entity.Property(e => e.PassportSerias)
                .HasMaxLength(6)
                .HasColumnName("passport_serias");
            entity.Property(e => e.Patronymic)
                .HasMaxLength(50)
                .HasColumnName("patronymic");
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(11)
                .HasColumnName("phone_number");
            entity.Property(e => e.Surname)
                .HasMaxLength(50)
                .HasColumnName("surname");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.User).WithMany(p => p.Klients)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("klients_user_id_fkey");
        });

        modelBuilder.Entity<Payment>(entity =>
        {
            entity.HasKey(e => e.PaymentId).HasName("payments_pkey");

            entity.ToTable("payments");

            entity.Property(e => e.PaymentId).HasColumnName("payment_id");
            entity.Property(e => e.ContractId).HasColumnName("contract_id");
            entity.Property(e => e.Cost)
                .HasColumnType("money")
                .HasColumnName("cost");
            entity.Property(e => e.DateOfPayment).HasColumnName("date_of_payment");
            entity.Property(e => e.PaymentMethodId).HasColumnName("payment_method_id");

            entity.HasOne(d => d.Contract).WithMany(p => p.Payments)
                .HasForeignKey(d => d.ContractId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("payments_contract_id_fkey");

            entity.HasOne(d => d.PaymentMethod).WithMany(p => p.Payments)
                .HasForeignKey(d => d.PaymentMethodId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("payments_payment_method_id_fkey");
        });

        modelBuilder.Entity<PaymentMethod>(entity =>
        {
            entity.HasKey(e => e.PaymentMethodId).HasName("payment_method_pkey");

            entity.ToTable("payment_method");

            entity.Property(e => e.PaymentMethodId).HasColumnName("payment_method_id");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("name");
        });

        modelBuilder.Entity<Position>(entity =>
        {
            entity.HasKey(e => e.PositionId).HasName("positions_pkey");

            entity.ToTable("positions");

            entity.Property(e => e.PositionId).HasColumnName("position_id");
            entity.Property(e => e.NamePositions)
                .HasMaxLength(100)
                .HasColumnName("name_positions");
        });

        modelBuilder.Entity<RegistrationOfDocument>(entity =>
        {
            entity.HasKey(e => e.RegistrationId).HasName("registration_of_documents_pkey");

            entity.ToTable("registration_of_documents");

            entity.Property(e => e.RegistrationId).HasColumnName("registration_id");
            entity.Property(e => e.ContractId).HasColumnName("contract_id");
            entity.Property(e => e.DateOfApplication).HasColumnName("date_of_application");
            entity.Property(e => e.DateOfReceipt).HasColumnName("date_of_receipt");

            entity.HasOne(d => d.Contract).WithMany(p => p.RegistrationOfDocuments)
                .HasForeignKey(d => d.ContractId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("registration_of_documents_contract_id_fkey");
        });

        modelBuilder.Entity<RegistrationOfInsurance>(entity =>
        {
            entity.HasKey(e => e.RegistrationInsuranceId).HasName("registration_of_insurance_pkey");

            entity.ToTable("registration_of_insurance");

            entity.Property(e => e.RegistrationInsuranceId).HasColumnName("registration_insurance_id");
            entity.Property(e => e.CountryId).HasColumnName("country_id");
            entity.Property(e => e.EndDate).HasColumnName("end_date");
            entity.Property(e => e.RegistationId).HasColumnName("registation_id");
            entity.Property(e => e.StartDate).HasColumnName("start_date");
            entity.Property(e => e.TypeInsuranceId).HasColumnName("type_insurance_id");

            entity.HasOne(d => d.Country).WithMany(p => p.RegistrationOfInsurances)
                .HasForeignKey(d => d.CountryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("registration_of_insurance_country_id_fkey");

            entity.HasOne(d => d.Registation).WithMany(p => p.RegistrationOfInsurances)
                .HasForeignKey(d => d.RegistationId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("registration_of_insurance_registation_id_fkey");

            entity.HasOne(d => d.TypeInsurance).WithMany(p => p.RegistrationOfInsurances)
                .HasForeignKey(d => d.TypeInsuranceId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("registration_of_insurance_type_insurance_id_fkey");
        });

        modelBuilder.Entity<Representative>(entity =>
        {
            entity.HasKey(e => e.RepresentativeId).HasName("representatives_pkey");

            entity.ToTable("representatives");

            entity.Property(e => e.RepresentativeId).HasColumnName("representative_id");
            entity.Property(e => e.Address)
                .HasMaxLength(100)
                .HasColumnName("address");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("name");
            entity.Property(e => e.Patronymic)
                .HasMaxLength(50)
                .HasColumnName("patronymic");
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(11)
                .HasColumnName("phone_number");
            entity.Property(e => e.Surname)
                .HasMaxLength(50)
                .HasColumnName("surname");
            entity.Property(e => e.TourId).HasColumnName("tour_id");

            entity.HasOne(d => d.Tour).WithMany(p => p.Representatives)
                .HasForeignKey(d => d.TourId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("representatives_tour_id_fkey");
        });

        modelBuilder.Entity<Reservation>(entity =>
        {
            entity.HasKey(e => e.ReservationId).HasName("reservations_pkey");

            entity.ToTable("reservations");

            entity.Property(e => e.ReservationId).HasColumnName("reservation_id");
            entity.Property(e => e.ContractId).HasColumnName("contract_id");
            entity.Property(e => e.DateReservation).HasColumnName("date_reservation");
            entity.Property(e => e.EndDate).HasColumnName("end_date");
            entity.Property(e => e.HotelId).HasColumnName("hotel_id");
            entity.Property(e => e.StartDate).HasColumnName("start_date");
            entity.Property(e => e.TourId).HasColumnName("tour_id");
            entity.Property(e => e.TransportId).HasColumnName("transport_id");

            entity.HasOne(d => d.Contract).WithMany(p => p.Reservations)
                .HasForeignKey(d => d.ContractId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("reservations_contract_id_fkey");

            entity.HasOne(d => d.Hotel).WithMany(p => p.Reservations)
                .HasForeignKey(d => d.HotelId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("reservations_hotel_id_fkey");

            entity.HasOne(d => d.Tour).WithMany(p => p.Reservations)
                .HasForeignKey(d => d.TourId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("reservations_tour_id_fkey");

            entity.HasOne(d => d.Transport).WithMany(p => p.Reservations)
                .HasForeignKey(d => d.TransportId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("reservations_transport_id_fkey");
        });

        modelBuilder.Entity<ServiceAgreement>(entity =>
        {
            entity.HasKey(e => e.ContractId).HasName("service_agreements_pkey");

            entity.ToTable("service_agreements");

            entity.Property(e => e.ContractId).HasColumnName("contract_id");
            entity.Property(e => e.Conditions).HasColumnName("conditions");
            entity.Property(e => e.DateOfConclusion).HasColumnName("date_of_conclusion");
            entity.Property(e => e.EmployeeId).HasColumnName("employee_id");
            entity.Property(e => e.KlientId).HasColumnName("klient_id");

            entity.HasOne(d => d.Employee).WithMany(p => p.ServiceAgreements)
                .HasForeignKey(d => d.EmployeeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("service_agreements_employee_id_fkey");

            entity.HasOne(d => d.Klient).WithMany(p => p.ServiceAgreements)
                .HasForeignKey(d => d.KlientId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("service_agreements_klient_id_fkey");
        });

        modelBuilder.Entity<StatusRegistration>(entity =>
        {
            entity.HasKey(e => e.StatusId).HasName("status_registration_pkey");

            entity.ToTable("status_registration");

            entity.Property(e => e.StatusId).HasColumnName("status_id");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name");
        });

        modelBuilder.Entity<Supplier>(entity =>
        {
            entity.HasKey(e => e.SupplierId).HasName("suppliers_pkey");

            entity.ToTable("suppliers");

            entity.Property(e => e.SupplierId).HasColumnName("supplier_id");
            entity.Property(e => e.Address)
                .HasMaxLength(100)
                .HasColumnName("address");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name");
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(11)
                .HasColumnName("phone_number");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.User).WithMany(p => p.Suppliers)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("suppliers_user_id_fkey");
        });

        modelBuilder.Entity<Tour>(entity =>
        {
            entity.HasKey(e => e.TourId).HasName("tours_pkey");

            entity.ToTable("tours");

            entity.Property(e => e.TourId).HasColumnName("tour_id");
            entity.Property(e => e.CityId).HasColumnName("city_id");
            entity.Property(e => e.Cost)
                .HasColumnType("money")
                .HasColumnName("cost");
            entity.Property(e => e.CountryId).HasColumnName("country_id");
            entity.Property(e => e.Descriptions).HasColumnName("descriptions");
            entity.Property(e => e.Duration).HasColumnName("duration");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("name");
            entity.Property(e => e.SupplierId).HasColumnName("supplier_id");

            entity.HasOne(d => d.City).WithMany(p => p.Tours)
                .HasForeignKey(d => d.CityId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("tours_city_id_fkey");

            entity.HasOne(d => d.Country).WithMany(p => p.Tours)
                .HasForeignKey(d => d.CountryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("tours_country_id_fkey");

            entity.HasOne(d => d.Supplier).WithMany(p => p.Tours)
                .HasForeignKey(d => d.SupplierId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("tours_supplier_id_fkey");
        });

        modelBuilder.Entity<Transfer>(entity =>
        {
            entity.HasKey(e => e.TransferId).HasName("transfer_pkey");

            entity.ToTable("transfer");

            entity.Property(e => e.TransferId).HasColumnName("transfer_id");
            entity.Property(e => e.Company)
                .HasMaxLength(100)
                .HasColumnName("company");
            entity.Property(e => e.CompanyNumberPhone)
                .HasMaxLength(11)
                .HasColumnName("company_number_phone");
            entity.Property(e => e.Cost)
                .HasColumnType("money")
                .HasColumnName("cost");
            entity.Property(e => e.TypeTransportId).HasColumnName("type_transport_id");

            entity.HasOne(d => d.TypeTransport).WithMany(p => p.Transfers)
                .HasForeignKey(d => d.TypeTransportId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("transfer_type_transport_id_fkey");
        });

        modelBuilder.Entity<TransferOrder>(entity =>
        {
            entity.HasKey(e => e.TransferOrderId).HasName("transfer_order_pkey");

            entity.ToTable("transfer_order");

            entity.Property(e => e.TransferOrderId).HasColumnName("transfer_order_id");
            entity.Property(e => e.ContractId).HasColumnName("contract_id");
            entity.Property(e => e.DateOrder).HasColumnName("date_order");
            entity.Property(e => e.TransferId).HasColumnName("transfer_id");

            entity.HasOne(d => d.Contract).WithMany(p => p.TransferOrders)
                .HasForeignKey(d => d.ContractId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("transfer_order_contract_id_fkey");

            entity.HasOne(d => d.Transfer).WithMany(p => p.TransferOrders)
                .HasForeignKey(d => d.TransferId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("transfer_order_transfer_id_fkey");
        });

        modelBuilder.Entity<Transport>(entity =>
        {
            entity.HasKey(e => e.TransportId).HasName("transport_pkey");

            entity.ToTable("transport");

            entity.Property(e => e.TransportId).HasColumnName("transport_id");
            entity.Property(e => e.Company)
                .HasMaxLength(150)
                .HasColumnName("company");
            entity.Property(e => e.Cost)
                .HasColumnType("money")
                .HasColumnName("cost");
            entity.Property(e => e.FlightNumber)
                .HasMaxLength(10)
                .HasColumnName("flight_number");
            entity.Property(e => e.TypeTransportId).HasColumnName("type_transport_id");

            entity.HasOne(d => d.TypeTransport).WithMany(p => p.Transports)
                .HasForeignKey(d => d.TypeTransportId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("transport_type_transport_id_fkey");
        });

        modelBuilder.Entity<TypeOfInsurance>(entity =>
        {
            entity.HasKey(e => e.TypeInsuranceId).HasName("type_of_insurance_pkey");

            entity.ToTable("type_of_insurance");

            entity.Property(e => e.TypeInsuranceId).HasColumnName("type_insurance_id");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name");
        });

        modelBuilder.Entity<TypeOfTransport>(entity =>
        {
            entity.HasKey(e => e.TypeTransportId).HasName("type_of_transport_pkey");

            entity.ToTable("type_of_transport");

            entity.Property(e => e.TypeTransportId).HasColumnName("type_transport_id");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name");
        });

        modelBuilder.Entity<TypeOfVisa>(entity =>
        {
            entity.HasKey(e => e.TypeVisaId).HasName("type_of_visa_pkey");

            entity.ToTable("type_of_visa");

            entity.Property(e => e.TypeVisaId).HasColumnName("type_visa_id");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("users_pkey");

            entity.ToTable("users");

            entity.Property(e => e.UserId).HasColumnName("user_id");
            entity.Property(e => e.Login)
                .HasMaxLength(20)
                .HasColumnName("login");
            entity.Property(e => e.Password)
                .HasMaxLength(20)
                .HasColumnName("password");
        });

        modelBuilder.Entity<VisaProcessing>(entity =>
        {
            entity.HasKey(e => e.VisaProcessId).HasName("visa_processing_pkey");

            entity.ToTable("visa_processing");

            entity.Property(e => e.VisaProcessId).HasColumnName("visa_process_id");
            entity.Property(e => e.CountryId).HasColumnName("country_id");
            entity.Property(e => e.EndDate).HasColumnName("end_date");
            entity.Property(e => e.RegistationId).HasColumnName("registation_id");
            entity.Property(e => e.StartDate).HasColumnName("start_date");
            entity.Property(e => e.TypeVisa).HasColumnName("type_visa");

            entity.HasOne(d => d.Country).WithMany(p => p.VisaProcessings)
                .HasForeignKey(d => d.CountryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("visa_processing_country_id_fkey");

            entity.HasOne(d => d.Registation).WithMany(p => p.VisaProcessings)
                .HasForeignKey(d => d.RegistationId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("registration");

            entity.HasOne(d => d.TypeVisaNavigation).WithMany(p => p.VisaProcessings)
                .HasForeignKey(d => d.TypeVisa)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("type");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
