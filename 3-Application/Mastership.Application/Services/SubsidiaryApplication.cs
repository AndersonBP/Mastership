using AutoMapper;
using Mastership.Domain;
using Mastership.Domain.DTO;
using Mastership.Domain.Interfaces;
using Mastership.Domain.Interfaces.Application;
using Mastership.Domain.Repository;
using Mastership.Domain.ViewModels;
using Mastership.Domain.ViewModels.RequestResponseViewModels;
using Mastership.Infra.CrossCutting.Extensions;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mastership.Application.Services
{
    public class SubsidiaryApplication : BaseApplication<SubsidiaryViewModel, SubsidiaryDTO, ISubsidiaryRepository>, ISubsidiaryApplication
    {
        public readonly IEmployeeRepository _employeeRepository;
        private readonly IPointTimeRepository _pointTimeRepository;

        public SubsidiaryApplication(
            IMapper mapper,
            ISubsidiaryRepository repository, IUserDataService userDataService,
            IEmployeeRepository employeeRepository, IPointTimeRepository pointTimeRepository
        ) : base(repository, mapper, userDataService)
        {
            this._employeeRepository = employeeRepository;
            this._pointTimeRepository = pointTimeRepository;
        }



        public SubsidiaryDTO GetSubsidiaryByUser(UserDTO user)
        {

            switch (user.UserType)
            {
                case Domain.Enum.UserType.Subsidiary:
                    return this._repository.GetByUser(user.Id);
                case Domain.Enum.UserType.Employee:
                    var employee = this._employeeRepository.GetByUserId(user.Id);
                    return employee.Subsidiary;
                default:
                    throw new System.Exception("Type of user invalid");
            }
        }
        public FileResult CreateAFD(AFDViewModel afdParams)
        {
            IEnumerable<PointTimeDTO> clocks = this._pointTimeRepository.GetByRange(afdParams.Start.AbsoluteStart(), afdParams.End.AbsoluteEnd(), this._userDataService.SubsidiaryId).ToList();
            var employes = this._employeeRepository.Get(clocks.Select(x => x.EmployeeId).ToArray()).ToList();

            var body = clocks.Select(x => $"{x.Sequential.ToString().PadLeft(9, '0')}3{x.DateTime.ToStringNumbers()}{employes.FirstOrDefault(e=>e.Id.Equals(x.EmployeeId)).PIS}");

            FileResult fileResult = new FileContentResult(Encoding.ASCII.GetBytes(string.Join("\n", body)), "application/txt");
            fileResult.FileDownloadName = $"AFD-{afdParams.Start.ToStringNumbers()} a {afdParams.Start.ToStringNumbers()}";
            return fileResult;
        }
    }
}
