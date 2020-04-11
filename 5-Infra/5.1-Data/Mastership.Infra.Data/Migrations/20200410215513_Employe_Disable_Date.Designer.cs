﻿// <auto-generated />
using System;
using Mastership.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Mastership.Infra.Data.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20200410215513_Employe_Disable_Date")]
    partial class Employe_Disable_Date
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                .HasAnnotation("ProductVersion", "3.1.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            modelBuilder.Entity("Mastership.Infra.Data.Entities.BillingCustomerEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("ChangeDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<bool>("Deleted")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("boolean")
                        .HasDefaultValue(false);

                    b.Property<bool>("Enable")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("boolean")
                        .HasDefaultValue(true);

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<Guid?>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("BillingCustomer");

                    b.HasData(
                        new
                        {
                            Id = new Guid("8bd7a794-7dc8-41a2-be9a-e09ce16f7181"),
                            ChangeDate = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            CreationDate = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Deleted = false,
                            Enable = false,
                            Name = "Alldesk"
                        });
                });

            modelBuilder.Entity("Mastership.Infra.Data.Entities.CompanyEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Adress")
                        .HasColumnType("text");

                    b.Property<Guid>("BillingCustomerId")
                        .HasColumnType("uuid");

                    b.Property<string>("CNPJ")
                        .HasColumnType("character varying(18)")
                        .HasMaxLength(18);

                    b.Property<DateTime>("ChangeDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<bool>("Deleted")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("boolean")
                        .HasDefaultValue(false);

                    b.Property<string>("DomainName")
                        .HasColumnType("text");

                    b.Property<bool>("Enable")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("boolean")
                        .HasDefaultValue(true);

                    b.Property<string>("ForeignId")
                        .HasColumnType("text");

                    b.Property<double>("Latitude")
                        .HasColumnType("double precision");

                    b.Property<double>("Longitude")
                        .HasColumnType("double precision");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<string>("RazaoSocial")
                        .HasColumnType("text");

                    b.Property<string>("ZipCode")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("BillingCustomerId");

                    b.HasIndex("CNPJ")
                        .IsUnique();

                    b.HasIndex("DomainName")
                        .IsUnique();

                    b.ToTable("Company");

                    b.HasData(
                        new
                        {
                            Id = new Guid("90286f77-5cc9-4140-8cc5-e4e24510879e"),
                            Adress = "RUA PEDRO BORGES , 30, SALAS 101 A 110 1 ANDAR",
                            BillingCustomerId = new Guid("8bd7a794-7dc8-41a2-be9a-e09ce16f7181"),
                            CNPJ = "10.347.407/0001-43",
                            ChangeDate = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            CreationDate = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Deleted = false,
                            DomainName = "alldesk",
                            Enable = false,
                            Latitude = -3.7357805000000002,
                            Longitude = -38.490112000000003,
                            Name = "AllDesk",
                            RazaoSocial = "MEIRELES, FREITAS E ALMEIDA SERVICOS DE TELEATENDIMENTO LTDA",
                            ZipCode = "60055-110"
                        });
                });

            modelBuilder.Entity("Mastership.Infra.Data.Entities.EmployeeEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("AdmissionDate")
                        .HasColumnType("date");

                    b.Property<DateTime>("Birthday")
                        .HasColumnType("date");

                    b.Property<string>("CPF")
                        .HasColumnType("character varying(15)")
                        .HasMaxLength(15);

                    b.Property<DateTime>("ChangeDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<bool>("Deleted")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("boolean")
                        .HasDefaultValue(false);

                    b.Property<DateTime?>("DisabledDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Email")
                        .HasColumnType("text");

                    b.Property<bool>("Enable")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("boolean")
                        .HasDefaultValue(true);

                    b.Property<string>("ForeignId")
                        .HasColumnType("text");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("PIS")
                        .HasColumnType("text");

                    b.Property<string>("RG")
                        .HasColumnType("text");

                    b.Property<string>("Registration")
                        .HasColumnType("text");

                    b.Property<Guid>("SubsidiaryId")
                        .HasColumnType("uuid");

                    b.Property<Guid?>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.HasIndex("SubsidiaryId", "CPF")
                        .IsUnique();

                    b.HasIndex("SubsidiaryId", "Registration")
                        .IsUnique();

                    b.ToTable("Employee");

                    b.HasData(
                        new
                        {
                            Id = new Guid("546d31b0-f719-4789-b5f2-7ff94afa72e8"),
                            AdmissionDate = new DateTime(2020, 4, 10, 0, 0, 0, 0, DateTimeKind.Local),
                            Birthday = new DateTime(1995, 1, 25, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            CPF = "062.898.123-60",
                            ChangeDate = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            CreationDate = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Deleted = false,
                            Enable = false,
                            FullName = "Anderson Benevides Peres",
                            Name = "Anderson",
                            Registration = "87654321",
                            SubsidiaryId = new Guid("a88c24f4-d6c9-4eba-8c86-67d515c3979f")
                        });
                });

            modelBuilder.Entity("Mastership.Infra.Data.Entities.PointTimeEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("ChangeDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<DateTime>("DateTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<bool>("Deleted")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("boolean")
                        .HasDefaultValue(false);

                    b.Property<Guid>("EmployeeId")
                        .HasColumnType("uuid");

                    b.Property<bool>("Enable")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("boolean")
                        .HasDefaultValue(true);

                    b.Property<string>("IP")
                        .HasColumnType("text");

                    b.Property<decimal>("Latitude")
                        .HasColumnType("numeric");

                    b.Property<decimal>("Longitude")
                        .HasColumnType("numeric");

                    b.Property<long>("Sequential")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("UserHostName")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("EmployeeId");

                    b.ToTable("PointTime");
                });

            modelBuilder.Entity("Mastership.Infra.Data.Entities.SubsidiaryEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Adress")
                        .HasColumnType("text");

                    b.Property<string>("CEI")
                        .HasColumnType("text");

                    b.Property<string>("CNPJ")
                        .HasColumnType("text");

                    b.Property<DateTime>("ChangeDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<Guid>("CompanyId")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<bool>("Deleted")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("boolean")
                        .HasDefaultValue(false);

                    b.Property<bool>("Enable")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("boolean")
                        .HasDefaultValue(true);

                    b.Property<string>("ForeignId")
                        .HasColumnType("text");

                    b.Property<double>("Latitude")
                        .HasColumnType("double precision");

                    b.Property<double>("Longitude")
                        .HasColumnType("double precision");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<string>("REP")
                        .HasColumnType("character varying(17)")
                        .HasMaxLength(17);

                    b.Property<string>("RazaoSocial")
                        .HasColumnType("text");

                    b.Property<Guid?>("UserId")
                        .HasColumnType("uuid");

                    b.Property<string>("ZipCode")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("CompanyId");

                    b.HasIndex("REP")
                        .IsUnique();

                    b.HasIndex("UserId");

                    b.ToTable("Subsidiary");

                    b.HasData(
                        new
                        {
                            Id = new Guid("a88c24f4-d6c9-4eba-8c86-67d515c3979f"),
                            Adress = "RUA PEDRO BORGES , 30, SALAS 101 A 110 1 ANDAR",
                            CNPJ = "10.347.407/0001-43",
                            ChangeDate = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            CompanyId = new Guid("90286f77-5cc9-4140-8cc5-e4e24510879e"),
                            CreationDate = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Deleted = false,
                            Enable = false,
                            Latitude = -3.7357805000000002,
                            Longitude = -38.490112000000003,
                            Name = "Alldesk",
                            RazaoSocial = "MEIRELES, FREITAS E ALMEIDA SERVICOS DE TELEATENDIMENTO LTDA",
                            UserId = new Guid("fe01e0a6-c73b-41b4-a963-0481b2476cb3"),
                            ZipCode = "60055-110"
                        });
                });

            modelBuilder.Entity("Mastership.Infra.Data.Entities.UserEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("ChangeDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<bool>("Deleted")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("boolean")
                        .HasDefaultValue(false);

                    b.Property<string>("Email")
                        .HasColumnType("text");

                    b.Property<bool>("Enable")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("boolean")
                        .HasDefaultValue(true);

                    b.Property<string>("Password")
                        .HasColumnType("text");

                    b.Property<int>("UserType")
                        .HasColumnType("integer");

                    b.Property<string>("Username")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("User");

                    b.HasData(
                        new
                        {
                            Id = new Guid("fe01e0a6-c73b-41b4-a963-0481b2476cb3"),
                            ChangeDate = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            CreationDate = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Deleted = false,
                            Enable = false,
                            Password = "5af3d7ad0ff3e10cd38d298e6efec318",
                            UserType = 1,
                            Username = "alldesk"
                        });
                });

            modelBuilder.Entity("Mastership.Infra.Data.Entities.BillingCustomerEntity", b =>
                {
                    b.HasOne("Mastership.Infra.Data.Entities.UserEntity", "User")
                        .WithMany()
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("Mastership.Infra.Data.Entities.CompanyEntity", b =>
                {
                    b.HasOne("Mastership.Infra.Data.Entities.BillingCustomerEntity", "BillingCustomer")
                        .WithMany("Companies")
                        .HasForeignKey("BillingCustomerId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });

            modelBuilder.Entity("Mastership.Infra.Data.Entities.EmployeeEntity", b =>
                {
                    b.HasOne("Mastership.Infra.Data.Entities.SubsidiaryEntity", "Subsidiary")
                        .WithMany("Employees")
                        .HasForeignKey("SubsidiaryId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Mastership.Infra.Data.Entities.UserEntity", "User")
                        .WithMany("Employees")
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("Mastership.Infra.Data.Entities.PointTimeEntity", b =>
                {
                    b.HasOne("Mastership.Infra.Data.Entities.EmployeeEntity", "Employee")
                        .WithMany("PointsTime")
                        .HasForeignKey("EmployeeId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });

            modelBuilder.Entity("Mastership.Infra.Data.Entities.SubsidiaryEntity", b =>
                {
                    b.HasOne("Mastership.Infra.Data.Entities.CompanyEntity", "Company")
                        .WithMany("Subsidiaries")
                        .HasForeignKey("CompanyId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Mastership.Infra.Data.Entities.UserEntity", "User")
                        .WithMany("Subsidiaries")
                        .HasForeignKey("UserId");
                });
#pragma warning restore 612, 618
        }
    }
}
