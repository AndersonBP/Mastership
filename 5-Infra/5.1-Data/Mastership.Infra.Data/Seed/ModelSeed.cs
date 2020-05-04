using Mastership.Infra.CrossCutting.Extensions;
using Mastership.Infra.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Security.Cryptography;
using System.Text;

namespace Mastership.Infra.Data
{
    public static class ModelSeed
    {

        public static void SeedModel(this ModelBuilder builder)
        {
            builder.Entity<BillingCustomerEntity>().HasData(
                new BillingCustomerEntity() { Id = Guid.Parse("8bd7a794-7dc8-41a2-be9a-e09ce16f7181"), Name = "Alldesk" }
            );

            builder.Entity<CompanyEntity>().HasData(
            new CompanyEntity { BillingCustomerId = Guid.Parse("8bd7a794-7dc8-41a2-be9a-e09ce16f7181"), DomainName = "alldesk", Id = Guid.Parse("90286f77-5cc9-4140-8cc5-e4e24510879e"), Adress = "RUA PEDRO BORGES , 30, SALAS 101 A 110 1 ANDAR", ZipCode = "60055-110", CNPJ = "10.347.407/0001-43", Name = "AllDesk", RazaoSocial = "MEIRELES, FREITAS E ALMEIDA SERVICOS DE TELEATENDIMENTO LTDA", Latitude = -3.7357805, Longitude = -38.490112, Enable=true, CreationDate= new DateTime(2020, 4, 11, 17, 33, 31, 922, DateTimeKind.Local).AddTicks(9731), Image=$"11042020173331923logo-alldesk.png" }
             );

            builder.Entity<SubsidiaryEntity>().HasData(
              new SubsidiaryEntity() { UserId = Guid.Parse("fe01e0a6-c73b-41b4-a963-0481b2476cb3"), CompanyId = Guid.Parse("90286f77-5cc9-4140-8cc5-e4e24510879e"), Id = Guid.Parse("a88c24f4-d6c9-4eba-8c86-67d515c3979f"), Name = "Alldesk",  Adress = "RUA PEDRO BORGES , 30, SALAS 101 A 110 1 ANDAR", ZipCode = "60055-110", CNPJ = "10.347.407/0001-43", RazaoSocial = "MEIRELES, FREITAS E ALMEIDA SERVICOS DE TELEATENDIMENTO LTDA", Latitude = -3.7357805, Longitude = -38.490112, REP= "00000000000000000" }
            );

            builder.Entity<UserEntity>().HasData(
              new UserEntity() { Id = Guid.Parse("fe01e0a6-c73b-41b4-a963-0481b2476cb3"), Username = "alldesk", Password = MD5Extension.GetMd5Hash(MD5.Create(), "1234desk"), UserType = Domain.Enum.UserType.Subsidiary }
            );
        }
    }
}
