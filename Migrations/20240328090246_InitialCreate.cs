using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Travel_agency_Lyapynova.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "countries",
                columns: table => new
                {
                    country_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("countries_pkey", x => x.country_id);
                });

            migrationBuilder.CreateTable(
                name: "payment_method",
                columns: table => new
                {
                    payment_method_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("payment_method_pkey", x => x.payment_method_id);
                });

            migrationBuilder.CreateTable(
                name: "positions",
                columns: table => new
                {
                    position_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name_positions = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("positions_pkey", x => x.position_id);
                });

            migrationBuilder.CreateTable(
                name: "status_registration",
                columns: table => new
                {
                    status_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("status_registration_pkey", x => x.status_id);
                });

            migrationBuilder.CreateTable(
                name: "type_of_insurance",
                columns: table => new
                {
                    type_insurance_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("type_of_insurance_pkey", x => x.type_insurance_id);
                });

            migrationBuilder.CreateTable(
                name: "type_of_transport",
                columns: table => new
                {
                    type_transport_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("type_of_transport_pkey", x => x.type_transport_id);
                });

            migrationBuilder.CreateTable(
                name: "type_of_visa",
                columns: table => new
                {
                    type_visa_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("type_of_visa_pkey", x => x.type_visa_id);
                });

            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    user_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    login = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    password = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("users_pkey", x => x.user_id);
                });

            migrationBuilder.CreateTable(
                name: "cities",
                columns: table => new
                {
                    city_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    country_id = table.Column<int>(type: "integer", nullable: false),
                    name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("cities_pkey", x => x.city_id);
                    table.ForeignKey(
                        name: "cities_country_id_fkey",
                        column: x => x.country_id,
                        principalTable: "countries",
                        principalColumn: "country_id");
                });

            migrationBuilder.CreateTable(
                name: "transfer",
                columns: table => new
                {
                    transfer_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    type_transport_id = table.Column<int>(type: "integer", nullable: false),
                    cost = table.Column<decimal>(type: "money", nullable: false),
                    company = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    company_number_phone = table.Column<string>(type: "character varying(11)", maxLength: 11, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("transfer_pkey", x => x.transfer_id);
                    table.ForeignKey(
                        name: "transfer_type_transport_id_fkey",
                        column: x => x.type_transport_id,
                        principalTable: "type_of_transport",
                        principalColumn: "type_transport_id");
                });

            migrationBuilder.CreateTable(
                name: "transport",
                columns: table => new
                {
                    transport_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    type_transport_id = table.Column<int>(type: "integer", nullable: false),
                    flight_number = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false),
                    company = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    cost = table.Column<decimal>(type: "money", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("transport_pkey", x => x.transport_id);
                    table.ForeignKey(
                        name: "transport_type_transport_id_fkey",
                        column: x => x.type_transport_id,
                        principalTable: "type_of_transport",
                        principalColumn: "type_transport_id");
                });

            migrationBuilder.CreateTable(
                name: "employees",
                columns: table => new
                {
                    employee_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    surname = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    patronymic = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    position_id = table.Column<int>(type: "integer", nullable: false),
                    phone_number = table.Column<string>(type: "character varying(11)", maxLength: 11, nullable: false),
                    user_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("employees_pkey", x => x.employee_id);
                    table.ForeignKey(
                        name: "employees_position_id_fkey",
                        column: x => x.position_id,
                        principalTable: "positions",
                        principalColumn: "position_id");
                    table.ForeignKey(
                        name: "employees_user_id_fkey",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "user_id");
                });

            migrationBuilder.CreateTable(
                name: "klients",
                columns: table => new
                {
                    klient_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    surname = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    patronymic = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    email = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    phone_number = table.Column<string>(type: "character varying(11)", maxLength: 11, nullable: false),
                    passport_number = table.Column<string>(type: "character varying(4)", maxLength: 4, nullable: false),
                    passport_serias = table.Column<string>(type: "character varying(6)", maxLength: 6, nullable: false),
                    user_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("klients_pkey", x => x.klient_id);
                    table.ForeignKey(
                        name: "klients_user_id_fkey",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "user_id");
                });

            migrationBuilder.CreateTable(
                name: "suppliers",
                columns: table => new
                {
                    supplier_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    phone_number = table.Column<string>(type: "character varying(11)", maxLength: 11, nullable: false),
                    address = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    user_id = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("suppliers_pkey", x => x.supplier_id);
                    table.ForeignKey(
                        name: "suppliers_user_id_fkey",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "user_id");
                });

            migrationBuilder.CreateTable(
                name: "excursions",
                columns: table => new
                {
                    excursion_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    description = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    cost = table.Column<decimal>(type: "money", nullable: false),
                    company_number_phone = table.Column<string>(type: "character varying(11)", maxLength: 11, nullable: false),
                    country_id = table.Column<int>(type: "integer", nullable: false),
                    city_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("excursions_pkey", x => x.excursion_id);
                    table.ForeignKey(
                        name: "city",
                        column: x => x.city_id,
                        principalTable: "cities",
                        principalColumn: "city_id");
                    table.ForeignKey(
                        name: "country",
                        column: x => x.country_id,
                        principalTable: "countries",
                        principalColumn: "country_id");
                });

            migrationBuilder.CreateTable(
                name: "hotels",
                columns: table => new
                {
                    hotel_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    address = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: false),
                    stars = table.Column<string>(type: "character varying(5)", maxLength: 5, nullable: false),
                    phone_number = table.Column<string>(type: "character varying(11)", maxLength: 11, nullable: false),
                    cost = table.Column<decimal>(type: "money", nullable: false),
                    country_id = table.Column<int>(type: "integer", nullable: false),
                    city_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("hotels_pkey", x => x.hotel_id);
                    table.ForeignKey(
                        name: "city",
                        column: x => x.city_id,
                        principalTable: "cities",
                        principalColumn: "city_id");
                    table.ForeignKey(
                        name: "country",
                        column: x => x.country_id,
                        principalTable: "countries",
                        principalColumn: "country_id");
                });

            migrationBuilder.CreateTable(
                name: "service_agreements",
                columns: table => new
                {
                    contract_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    klient_id = table.Column<int>(type: "integer", nullable: false),
                    employee_id = table.Column<int>(type: "integer", nullable: false),
                    date_of_conclusion = table.Column<DateOnly>(type: "date", nullable: false),
                    conditions = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("service_agreements_pkey", x => x.contract_id);
                    table.ForeignKey(
                        name: "service_agreements_employee_id_fkey",
                        column: x => x.employee_id,
                        principalTable: "employees",
                        principalColumn: "employee_id");
                    table.ForeignKey(
                        name: "service_agreements_klient_id_fkey",
                        column: x => x.klient_id,
                        principalTable: "klients",
                        principalColumn: "klient_id");
                });

            migrationBuilder.CreateTable(
                name: "cooperation_agreement",
                columns: table => new
                {
                    contract_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    supplier_id = table.Column<int>(type: "integer", nullable: false),
                    start_date = table.Column<DateOnly>(type: "date", nullable: false),
                    end_date = table.Column<DateOnly>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("cooperation_agreement_pkey", x => x.contract_id);
                    table.ForeignKey(
                        name: "cooperation_agreement_supplier_id_fkey",
                        column: x => x.supplier_id,
                        principalTable: "suppliers",
                        principalColumn: "supplier_id");
                });

            migrationBuilder.CreateTable(
                name: "tours",
                columns: table => new
                {
                    tour_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    descriptions = table.Column<string>(type: "text", nullable: false),
                    duration = table.Column<int>(type: "integer", nullable: false),
                    country_id = table.Column<int>(type: "integer", nullable: false),
                    city_id = table.Column<int>(type: "integer", nullable: false),
                    cost = table.Column<decimal>(type: "money", nullable: false),
                    supplier_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("tours_pkey", x => x.tour_id);
                    table.ForeignKey(
                        name: "tours_city_id_fkey",
                        column: x => x.city_id,
                        principalTable: "cities",
                        principalColumn: "city_id");
                    table.ForeignKey(
                        name: "tours_country_id_fkey",
                        column: x => x.country_id,
                        principalTable: "countries",
                        principalColumn: "country_id");
                    table.ForeignKey(
                        name: "tours_supplier_id_fkey",
                        column: x => x.supplier_id,
                        principalTable: "suppliers",
                        principalColumn: "supplier_id");
                });

            migrationBuilder.CreateTable(
                name: "excursions_order",
                columns: table => new
                {
                    excursion_order_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    excursion_id = table.Column<int>(type: "integer", nullable: false),
                    contract_id = table.Column<int>(type: "integer", nullable: false),
                    date_order = table.Column<DateOnly>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("excursions_order_pkey", x => x.excursion_order_id);
                    table.ForeignKey(
                        name: "excursions_order_contract_id_fkey",
                        column: x => x.contract_id,
                        principalTable: "service_agreements",
                        principalColumn: "contract_id");
                    table.ForeignKey(
                        name: "excursions_order_excursion_id_fkey",
                        column: x => x.excursion_id,
                        principalTable: "excursions",
                        principalColumn: "excursion_id");
                });

            migrationBuilder.CreateTable(
                name: "history",
                columns: table => new
                {
                    history_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    klient_id = table.Column<int>(type: "integer", nullable: false),
                    Сontract_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("history_pkey", x => x.history_id);
                    table.ForeignKey(
                        name: "history_klient_id_fkey",
                        column: x => x.klient_id,
                        principalTable: "klients",
                        principalColumn: "klient_id");
                    table.ForeignKey(
                        name: "history_Сontract_id_fkey",
                        column: x => x.Сontract_id,
                        principalTable: "service_agreements",
                        principalColumn: "contract_id");
                });

            migrationBuilder.CreateTable(
                name: "payments",
                columns: table => new
                {
                    payment_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    contract_id = table.Column<int>(type: "integer", nullable: false),
                    cost = table.Column<decimal>(type: "money", nullable: false),
                    payment_method_id = table.Column<int>(type: "integer", nullable: false),
                    date_of_payment = table.Column<DateOnly>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("payments_pkey", x => x.payment_id);
                    table.ForeignKey(
                        name: "payments_contract_id_fkey",
                        column: x => x.contract_id,
                        principalTable: "service_agreements",
                        principalColumn: "contract_id");
                    table.ForeignKey(
                        name: "payments_payment_method_id_fkey",
                        column: x => x.payment_method_id,
                        principalTable: "payment_method",
                        principalColumn: "payment_method_id");
                });

            migrationBuilder.CreateTable(
                name: "registration_of_documents",
                columns: table => new
                {
                    registration_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    contract_id = table.Column<int>(type: "integer", nullable: false),
                    date_of_application = table.Column<DateOnly>(type: "date", nullable: false),
                    date_of_receipt = table.Column<DateOnly>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("registration_of_documents_pkey", x => x.registration_id);
                    table.ForeignKey(
                        name: "registration_of_documents_contract_id_fkey",
                        column: x => x.contract_id,
                        principalTable: "service_agreements",
                        principalColumn: "contract_id");
                });

            migrationBuilder.CreateTable(
                name: "transfer_order",
                columns: table => new
                {
                    transfer_order_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    transfer_id = table.Column<int>(type: "integer", nullable: false),
                    contract_id = table.Column<int>(type: "integer", nullable: false),
                    date_order = table.Column<DateOnly>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("transfer_order_pkey", x => x.transfer_order_id);
                    table.ForeignKey(
                        name: "transfer_order_contract_id_fkey",
                        column: x => x.contract_id,
                        principalTable: "service_agreements",
                        principalColumn: "contract_id");
                    table.ForeignKey(
                        name: "transfer_order_transfer_id_fkey",
                        column: x => x.transfer_id,
                        principalTable: "transfer",
                        principalColumn: "transfer_id");
                });

            migrationBuilder.CreateTable(
                name: "representatives",
                columns: table => new
                {
                    representative_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    tour_id = table.Column<int>(type: "integer", nullable: false),
                    surname = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    patronymic = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    phone_number = table.Column<string>(type: "character varying(11)", maxLength: 11, nullable: false),
                    address = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("representatives_pkey", x => x.representative_id);
                    table.ForeignKey(
                        name: "representatives_tour_id_fkey",
                        column: x => x.tour_id,
                        principalTable: "tours",
                        principalColumn: "tour_id");
                });

            migrationBuilder.CreateTable(
                name: "reservations",
                columns: table => new
                {
                    reservation_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    start_date = table.Column<DateOnly>(type: "date", nullable: false),
                    end_date = table.Column<DateOnly>(type: "date", nullable: false),
                    contract_id = table.Column<int>(type: "integer", nullable: false),
                    tour_id = table.Column<int>(type: "integer", nullable: false),
                    hotel_id = table.Column<int>(type: "integer", nullable: false),
                    transport_id = table.Column<int>(type: "integer", nullable: false),
                    date_reservation = table.Column<DateOnly>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("reservations_pkey", x => x.reservation_id);
                    table.ForeignKey(
                        name: "reservations_contract_id_fkey",
                        column: x => x.contract_id,
                        principalTable: "service_agreements",
                        principalColumn: "contract_id");
                    table.ForeignKey(
                        name: "reservations_hotel_id_fkey",
                        column: x => x.hotel_id,
                        principalTable: "hotels",
                        principalColumn: "hotel_id");
                    table.ForeignKey(
                        name: "reservations_tour_id_fkey",
                        column: x => x.tour_id,
                        principalTable: "tours",
                        principalColumn: "tour_id");
                    table.ForeignKey(
                        name: "reservations_transport_id_fkey",
                        column: x => x.transport_id,
                        principalTable: "transport",
                        principalColumn: "transport_id");
                });

            migrationBuilder.CreateTable(
                name: "history_registration",
                columns: table => new
                {
                    history_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    registration_id = table.Column<int>(type: "integer", nullable: false),
                    status_id = table.Column<int>(type: "integer", nullable: false),
                    date = table.Column<DateOnly>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("history_registration_pkey", x => x.history_id);
                    table.ForeignKey(
                        name: "history_registration_registration_id_fkey",
                        column: x => x.registration_id,
                        principalTable: "registration_of_documents",
                        principalColumn: "registration_id");
                    table.ForeignKey(
                        name: "history_registration_status_id_fkey",
                        column: x => x.status_id,
                        principalTable: "status_registration",
                        principalColumn: "status_id");
                });

            migrationBuilder.CreateTable(
                name: "registration_of_insurance",
                columns: table => new
                {
                    registration_insurance_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    registation_id = table.Column<int>(type: "integer", nullable: false),
                    type_insurance_id = table.Column<int>(type: "integer", nullable: false),
                    country_id = table.Column<int>(type: "integer", nullable: false),
                    start_date = table.Column<DateOnly>(type: "date", nullable: false),
                    end_date = table.Column<DateOnly>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("registration_of_insurance_pkey", x => x.registration_insurance_id);
                    table.ForeignKey(
                        name: "registration_of_insurance_country_id_fkey",
                        column: x => x.country_id,
                        principalTable: "countries",
                        principalColumn: "country_id");
                    table.ForeignKey(
                        name: "registration_of_insurance_registation_id_fkey",
                        column: x => x.registation_id,
                        principalTable: "registration_of_documents",
                        principalColumn: "registration_id");
                    table.ForeignKey(
                        name: "registration_of_insurance_type_insurance_id_fkey",
                        column: x => x.type_insurance_id,
                        principalTable: "type_of_insurance",
                        principalColumn: "type_insurance_id");
                });

            migrationBuilder.CreateTable(
                name: "visa_processing",
                columns: table => new
                {
                    visa_process_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    registation_id = table.Column<int>(type: "integer", nullable: false),
                    country_id = table.Column<int>(type: "integer", nullable: false),
                    start_date = table.Column<DateOnly>(type: "date", nullable: false),
                    end_date = table.Column<DateOnly>(type: "date", nullable: false),
                    type_visa = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("visa_processing_pkey", x => x.visa_process_id);
                    table.ForeignKey(
                        name: "registration",
                        column: x => x.registation_id,
                        principalTable: "registration_of_documents",
                        principalColumn: "registration_id");
                    table.ForeignKey(
                        name: "type",
                        column: x => x.type_visa,
                        principalTable: "type_of_visa",
                        principalColumn: "type_visa_id");
                    table.ForeignKey(
                        name: "visa_processing_country_id_fkey",
                        column: x => x.country_id,
                        principalTable: "countries",
                        principalColumn: "country_id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_cities_country_id",
                table: "cities",
                column: "country_id");

            migrationBuilder.CreateIndex(
                name: "IX_cooperation_agreement_supplier_id",
                table: "cooperation_agreement",
                column: "supplier_id");

            migrationBuilder.CreateIndex(
                name: "IX_employees_position_id",
                table: "employees",
                column: "position_id");

            migrationBuilder.CreateIndex(
                name: "IX_employees_user_id",
                table: "employees",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_excursions_city_id",
                table: "excursions",
                column: "city_id");

            migrationBuilder.CreateIndex(
                name: "IX_excursions_country_id",
                table: "excursions",
                column: "country_id");

            migrationBuilder.CreateIndex(
                name: "IX_excursions_order_contract_id",
                table: "excursions_order",
                column: "contract_id");

            migrationBuilder.CreateIndex(
                name: "IX_excursions_order_excursion_id",
                table: "excursions_order",
                column: "excursion_id");

            migrationBuilder.CreateIndex(
                name: "IX_history_Сontract_id",
                table: "history",
                column: "Сontract_id");

            migrationBuilder.CreateIndex(
                name: "IX_history_klient_id",
                table: "history",
                column: "klient_id");

            migrationBuilder.CreateIndex(
                name: "IX_history_registration_registration_id",
                table: "history_registration",
                column: "registration_id");

            migrationBuilder.CreateIndex(
                name: "IX_history_registration_status_id",
                table: "history_registration",
                column: "status_id");

            migrationBuilder.CreateIndex(
                name: "IX_hotels_city_id",
                table: "hotels",
                column: "city_id");

            migrationBuilder.CreateIndex(
                name: "IX_hotels_country_id",
                table: "hotels",
                column: "country_id");

            migrationBuilder.CreateIndex(
                name: "IX_klients_user_id",
                table: "klients",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_payments_contract_id",
                table: "payments",
                column: "contract_id");

            migrationBuilder.CreateIndex(
                name: "IX_payments_payment_method_id",
                table: "payments",
                column: "payment_method_id");

            migrationBuilder.CreateIndex(
                name: "IX_registration_of_documents_contract_id",
                table: "registration_of_documents",
                column: "contract_id");

            migrationBuilder.CreateIndex(
                name: "IX_registration_of_insurance_country_id",
                table: "registration_of_insurance",
                column: "country_id");

            migrationBuilder.CreateIndex(
                name: "IX_registration_of_insurance_registation_id",
                table: "registration_of_insurance",
                column: "registation_id");

            migrationBuilder.CreateIndex(
                name: "IX_registration_of_insurance_type_insurance_id",
                table: "registration_of_insurance",
                column: "type_insurance_id");

            migrationBuilder.CreateIndex(
                name: "IX_representatives_tour_id",
                table: "representatives",
                column: "tour_id");

            migrationBuilder.CreateIndex(
                name: "IX_reservations_contract_id",
                table: "reservations",
                column: "contract_id");

            migrationBuilder.CreateIndex(
                name: "IX_reservations_hotel_id",
                table: "reservations",
                column: "hotel_id");

            migrationBuilder.CreateIndex(
                name: "IX_reservations_tour_id",
                table: "reservations",
                column: "tour_id");

            migrationBuilder.CreateIndex(
                name: "IX_reservations_transport_id",
                table: "reservations",
                column: "transport_id");

            migrationBuilder.CreateIndex(
                name: "IX_service_agreements_employee_id",
                table: "service_agreements",
                column: "employee_id");

            migrationBuilder.CreateIndex(
                name: "IX_service_agreements_klient_id",
                table: "service_agreements",
                column: "klient_id");

            migrationBuilder.CreateIndex(
                name: "IX_suppliers_user_id",
                table: "suppliers",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_tours_city_id",
                table: "tours",
                column: "city_id");

            migrationBuilder.CreateIndex(
                name: "IX_tours_country_id",
                table: "tours",
                column: "country_id");

            migrationBuilder.CreateIndex(
                name: "IX_tours_supplier_id",
                table: "tours",
                column: "supplier_id");

            migrationBuilder.CreateIndex(
                name: "IX_transfer_type_transport_id",
                table: "transfer",
                column: "type_transport_id");

            migrationBuilder.CreateIndex(
                name: "IX_transfer_order_contract_id",
                table: "transfer_order",
                column: "contract_id");

            migrationBuilder.CreateIndex(
                name: "IX_transfer_order_transfer_id",
                table: "transfer_order",
                column: "transfer_id");

            migrationBuilder.CreateIndex(
                name: "IX_transport_type_transport_id",
                table: "transport",
                column: "type_transport_id");

            migrationBuilder.CreateIndex(
                name: "IX_visa_processing_country_id",
                table: "visa_processing",
                column: "country_id");

            migrationBuilder.CreateIndex(
                name: "IX_visa_processing_registation_id",
                table: "visa_processing",
                column: "registation_id");

            migrationBuilder.CreateIndex(
                name: "IX_visa_processing_type_visa",
                table: "visa_processing",
                column: "type_visa");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "cooperation_agreement");

            migrationBuilder.DropTable(
                name: "excursions_order");

            migrationBuilder.DropTable(
                name: "history");

            migrationBuilder.DropTable(
                name: "history_registration");

            migrationBuilder.DropTable(
                name: "payments");

            migrationBuilder.DropTable(
                name: "registration_of_insurance");

            migrationBuilder.DropTable(
                name: "representatives");

            migrationBuilder.DropTable(
                name: "reservations");

            migrationBuilder.DropTable(
                name: "transfer_order");

            migrationBuilder.DropTable(
                name: "visa_processing");

            migrationBuilder.DropTable(
                name: "excursions");

            migrationBuilder.DropTable(
                name: "status_registration");

            migrationBuilder.DropTable(
                name: "payment_method");

            migrationBuilder.DropTable(
                name: "type_of_insurance");

            migrationBuilder.DropTable(
                name: "hotels");

            migrationBuilder.DropTable(
                name: "tours");

            migrationBuilder.DropTable(
                name: "transport");

            migrationBuilder.DropTable(
                name: "transfer");

            migrationBuilder.DropTable(
                name: "registration_of_documents");

            migrationBuilder.DropTable(
                name: "type_of_visa");

            migrationBuilder.DropTable(
                name: "cities");

            migrationBuilder.DropTable(
                name: "suppliers");

            migrationBuilder.DropTable(
                name: "type_of_transport");

            migrationBuilder.DropTable(
                name: "service_agreements");

            migrationBuilder.DropTable(
                name: "countries");

            migrationBuilder.DropTable(
                name: "employees");

            migrationBuilder.DropTable(
                name: "klients");

            migrationBuilder.DropTable(
                name: "positions");

            migrationBuilder.DropTable(
                name: "users");
        }
    }
}
