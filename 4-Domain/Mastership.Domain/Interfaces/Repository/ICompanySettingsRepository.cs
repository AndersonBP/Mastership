using Mastership.Domain.DTO;
using Mastership.Domain.Interfaces.Repository;
using System;
using System.Linq;


namespace Mastership.Domain.Repository
{
    public interface ICompanySettingsRepository : IRepository<CompanySettingsDTO> { }
}
