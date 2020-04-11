using Mastership.Domain.DTO;
using Mastership.Domain.DTO.Enums;
using Mastership.Domain.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Mastership.Domain.Interfaces.Application
{
    public interface IPointTimeApplication : IApplication<PointTimeViewModel> {
        ICollection<PointTimeViewModel> GetByDay(DateTime day, Guid employeId);
        IEnumerable<PointTimeDTO> GetByRange(DateTime start, DateTime end, Guid subsidiary);
        CheckRegistrationViewModel Register(CheckRegistrationViewModel vm, string domainName);
        FileResult ReceiptPDF(Guid id, string domainName);
    }
}
