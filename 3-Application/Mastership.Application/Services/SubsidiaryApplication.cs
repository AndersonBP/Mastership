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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mastership.Application.Services
{
    public class SubsidiaryApplication : BaseApplication<SubsidiaryViewModel, SubsidiaryDTO, ISubsidiaryRepository>, ISubsidiaryApplication
    {
        public readonly IEmployeeRepository _employeeRepository;
        private readonly IPointTimeRepository _pointTimeRepository;
        private readonly ISubsidiaryRepository _subsidiaryRepository;

        public SubsidiaryApplication(
            IMapper mapper,
            ISubsidiaryRepository repository, IUserDataService userDataService,
            IEmployeeRepository employeeRepository, IPointTimeRepository pointTimeRepository, ISubsidiaryRepository subsidiaryRepository
        ) : base(repository, mapper, userDataService)
        {
            this._employeeRepository = employeeRepository;
            this._pointTimeRepository = pointTimeRepository;
            this._subsidiaryRepository = subsidiaryRepository;
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
            var subsidiary = this._subsidiaryRepository.Get(employes.FirstOrDefault().SubsidiaryId);

            var header = $"00000000011{subsidiary.CNPJ.RemoveSpecialCharacters()}{(string.IsNullOrEmpty(subsidiary.CEI)?"":subsidiary.CEI).PadLeft(12, '0')}{(subsidiary.RazaoSocial.Length>150?subsidiary.RazaoSocial.Substring(0,149):subsidiary.RazaoSocial).RemoveSpecialCharacters().PadRight(150,' ')}{subsidiary.REP}{afdParams.Start.ToString("ddMMyyyy")}{afdParams.End.ToString("ddMMyyyy")}{DateTime.Now.ToString("ddMMyyyy")}{DateTime.Now.ToString("HHmm")}";
           
            var body = clocks.Select(x => $"{x.Sequential.ToString().PadLeft(9, '0')}3{x.DateTime.ToStringNumbers()}{employes.FirstOrDefault(e=>e.Id.Equals(x.EmployeeId)).PIS.Trim()}");
           
            var footer = $"{"".PadLeft(9,'9')}{"".PadLeft(9, '0')}{body.Count().ToString().PadLeft(9,'0')}{"".PadLeft(9, '0')}{"".PadLeft(9, '0')}9";

            var content = new string[] { header }.ToList();
            content.AddRange(body);
            content.Add(footer);

            FileResult fileResult = new FileContentResult(Encoding.ASCII.GetBytes(string.Join("\n", content)), "application/txt");
            fileResult.FileDownloadName = $"AFD-{afdParams.Start.ToStringNumbers()} a {afdParams.Start.ToStringNumbers()}";
            return fileResult;
        }
    }
}
