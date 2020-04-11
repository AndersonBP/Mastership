using Mastership.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mastership.Domain.Interfaces.Application
{
    public interface ITemplateService
    {
        string Get(TemplateType type);
        string GetReady(TemplateType type, Dictionary<string, string> keyValuePairs);
    }
}
