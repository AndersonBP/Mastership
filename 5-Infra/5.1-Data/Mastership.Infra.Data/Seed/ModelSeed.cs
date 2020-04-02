using Mastership.Infra.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mastership.Infra.Data
{
    public static class ModelSeed
    {

        public static void SeedModel(this ModelBuilder builder)
        {
            builder.Entity<BillingCustomerEntity>().HasData(
                new BillingCustomerEntity() { Id = Guid.Parse("8bd7a794-7dc8-41a2-be9a-e09ce16f7181"), Name = "MConsult" }
            );

            builder.Entity<CompanyEntity>().HasData(
            new CompanyEntity { BillingCustomerId = Guid.Parse("8bd7a794-7dc8-41a2-be9a-e09ce16f7181"), Id = Guid.Parse("90286f77-5cc9-4140-8cc5-e4e24510879e"), Adress = "V. DOM LUIS, 1200, TORRE 1, 21 ANDAR, SALA 2104 - Meireles, Fortaleza - CE", ZipCode = "60160-830", CNPJ = "14.921.000/0001-39", Name = "Mconsult", RazaoSocial = "M C Serviços de Tecnologia e Gestão LTDA", Latitude = -3.7357805, Longitude = -38.490112 }
             );

            builder.Entity<SubsidiaryEntity>().HasData(
              new SubsidiaryEntity() { CompanyId = Guid.Parse("90286f77-5cc9-4140-8cc5-e4e24510879e"), Id = Guid.Parse("a88c24f4-d6c9-4eba-8c86-67d515c3979f"), Name = "MConsult", DomainName = "mconsult" }
            );


            builder.Entity<EmployeeEntity>().HasData(
              new EmployeeEntity() { SubsidiaryId = Guid.Parse("a88c24f4-d6c9-4eba-8c86-67d515c3979f"), Id = Guid.Parse("546d31b0-f719-4789-b5f2-7ff94afa72e8"), Registration="87654321", CPF="062.898.123-60", AdmissionDate=DateTime.Now.Date, Birthday= DateTime.Now }
            );

            //FIXME: Change seed password, put encryption
            builder.Entity<UserEntity>().HasData(
              new UserEntity() { BillingCustomerId = Guid.Parse("8bd7a794-7dc8-41a2-be9a-e09ce16f7181"), Id = Guid.Parse("fe01e0a6-c73b-41b4-a963-0481b2476cb3"), Username="mconsult", Password="mc123321" }
            );
        }
    }
}
