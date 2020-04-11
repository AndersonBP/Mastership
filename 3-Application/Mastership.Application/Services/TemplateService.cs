using Mastership.Domain.Enum;
using Mastership.Domain.Interfaces.Application;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mastership.Application.Services
{
    public class TemplateService : ITemplateService
    {
        private readonly IHostingEnvironment _hostingEnvironment;
        public TemplateService(
            IHostingEnvironment hostingEnvironment)
        {
            this._hostingEnvironment = hostingEnvironment;
        }
        public string Get(TemplateType emailType)
        {
            var fileName = string.Empty;
            switch (emailType)
            {
                case TemplateType.ClockingReceipt:
                    fileName = "clocking_template.html";
                    break;
                default:
                    throw new Exception();
            }

            var directoryPath = this._hostingEnvironment.ContentRootPath + @"/wwwroot/EmailTemplates/";
            return System.IO.File.ReadAllText(directoryPath + fileName);
        }

        public string GetReady(TemplateType type, Dictionary<string, string> keyValuePairs)
        {
            var template = this.Get(type);
            foreach (var item in keyValuePairs)
            {
                template = template.Replace(item.Key, item.Value);
            }
            return template;
        }

       
    }
}
