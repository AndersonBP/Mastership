using Mastership.Domain.Entities;
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
            builder.Entity<CompanyEntity>().HasData(
            new CompanyEntity { Id=Guid.NewGuid(), Adress= " R. Pedro Borges, 30 - Centro, Fortaleza - CE", ZipCode= "60030-200", CNPJ= "10.347.407/0001-43", DomainName="alldesk", Name= "MEIRELES, FREITAS E ALMEIDA SERVICOS DE TELEATENDIMENTO LTDA", RazaoSocial= "CALLDESK SOLUCOES EM CONTACT CENTER", Latitude= -3.7280656, Longitude= -38.5282496 }
          );
        }
    }
}
