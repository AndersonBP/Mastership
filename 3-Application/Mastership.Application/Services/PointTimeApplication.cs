using AutoMapper;
using Mastership.Domain;
using Mastership.Domain.DTO;
using Mastership.Domain.DTO.Enums;
using Mastership.Domain.Enum;
using Mastership.Domain.Exceptions;
using Mastership.Domain.Interfaces;
using Mastership.Domain.Interfaces.Application;
using Mastership.Domain.Repository;
using Mastership.Domain.ViewModels;
using Mastership.Infra.CrossCutting.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OpenHtmlToPdf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;

namespace Mastership.Application.Services
{
    public class PointTimeApplication : BaseApplication<PointTimeViewModel, PointTimeDTO, IPointTimeRepository>, IPointTimeApplication
    {
        private readonly Lazy<IEmployeeApplication> employeeApplication;
        private readonly IEmailApplication _emailApplication;
        private readonly ITemplateService _templateService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ICompanyApplication _companyApplication;

        public PointTimeApplication(
            Lazy<IEmployeeApplication> employeeApplication,
            IPointTimeRepository repository,
            IMapper mapper, IUserDataService userDataService,
            IEmailApplication emailApplication,
            IHttpContextAccessor httpContextAccessor,
            ITemplateService templateService,
            ICompanyApplication _companyApplication
        ) : base(repository, mapper, userDataService)
        {
            this._emailApplication = emailApplication;
            this.employeeApplication = employeeApplication;
            this._httpContextAccessor = httpContextAccessor;
            this._templateService = templateService;
            this._companyApplication = _companyApplication;
        }

        public ICollection<PointTimeViewModel> GetByDay(DateTime day, Guid employeId)
        {
            var registrations = this._repository.GetByDay(day, employeId)
                .OrderBy(x => x.DateTime)
                .ToList();
            return this.MapToViewModel(registrations);
        }

        public IEnumerable<PointTimeDTO> GetByRange(DateTime start, DateTime end, Guid subsidiary)
        {
            return this._repository.GetByRange(start, end, subsidiary);
        }

        public FileResult ReceiptPDF(Guid id, string domainName)
        {
            var clock = this._repository.Search(id, domainName);
            if (clock == null)
                throw new NotFoundException("Clock not found!");

            var employe = this.employeeApplication.Value.Search(clock.EmployeeId);
            var check = this.employeeApplication.Value.CheckRegistration(new CheckRegistrationViewModel() { Registration= employe.Registration}, domainName);
            var pdf = Pdf.From( this.WriteEmailToClocking(clock, check))
              .WithGlobalSetting("orientation", "Portrait")
              .WithObjectSetting("web.defaultEncoding", "utf-8")
              .OfSize(PaperSize.A4)
              .Content();

            FileResult fileResult = new FileContentResult(pdf, "application/pdf");
            fileResult.FileDownloadName = $"Comprovante";
            return fileResult;
        }

        public CheckRegistrationViewModel Register(CheckRegistrationViewModel vm, string domainName)
        {
            var now = DateTime.Now;
            var employeeClock = this.employeeApplication.Value.CheckRegistration(vm, domainName);
            employeeClock.TrueAnswer = false;
            if (this.employeeApplication.Value.CheckAnswerQuestion(vm.QuestionType, employeeClock.Id, vm.Answer))
            {
                this._companyApplication.CheckDomainName(domainName);
                var registration = this._repository.Save(
                    new PointTimeDTO
                    {
                        DateTime = now,
                        EmployeeId = employeeClock.Id,
                        Latitude = vm.Latitude,
                        Longitude = vm.Longitude,
                        IP = this._httpContextAccessor.HttpContext.Connection.RemoteIpAddress.ToString(),
                        Sequential = this.GetSequential(employeeClock.Subsidiary.Id)
                    });

                employeeClock.PointsTime.Add(this.MapToViewModel(registration));
                employeeClock.PointsTime.OrderBy(x => x.DateTime);
                employeeClock.TrueAnswer = true;
                employeeClock.NSR = registration.Sequential.ToString().PadLeft(9, '0');
                if (!string.IsNullOrEmpty(employeeClock.Email))
                    this._emailApplication.SendEmailAsync("Registro de ponto", this.WriteEmailToClocking(registration, employeeClock), employeeClock.Email);
            }
            else
            {
                employeeClock.QuestionType = this.employeeApplication.Value.GetQuestionKey(vm.QuestionType);
            }

            return employeeClock;
        }

        private string WriteEmailToClocking(PointTimeDTO pointTimeDTO, CheckRegistrationViewModel checkRegistration)
        {
            var dictionary = new Dictionary<string, string> {
                { "{{RAZAO_SOCIAL}}", checkRegistration.Subsidiary.RazaoSocial },
                {"{{LOCAL_TRABALHO}}", checkRegistration.Subsidiary.Adress },
                {"{{CNPJ}}", checkRegistration.Subsidiary.CNPJ },
                {"{{CEI}}", checkRegistration.Subsidiary.CEI},
                { "{{DATA_HORA}}", pointTimeDTO.DateTime.ToString(@"dd / MM / yyyy HH: mm")},
                { "{{NOME_EMPREGADO}}", checkRegistration.FullName},
                { "{{PIS}}", checkRegistration.PIS},
                { "{{NSR}}", checkRegistration.NSR}
            };
            return this._templateService.GetReady(TemplateType.ClockingReceipt, dictionary);
        }

        private long GetSequential(Guid subsidiaryId)
        {
            var currentSequential = this._repository.GetLastSequentialOf(subsidiaryId);
            return currentSequential + 1;
        }
    }
}
