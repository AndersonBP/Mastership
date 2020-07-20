using Hangfire;
using Mastership.Domain.Interfaces.Application;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mastership.Infra.CrossCutting.IoC
{
    public static class HangFireSchedule
    {
        public static void ScheduleTasks(IServiceProvider serviceProvider)
        {
            using (var scope = serviceProvider.CreateScope())
            {
                var companyApp = scope.ServiceProvider.GetRequiredService<ICompanyApplication>();
                //RecurringJob.AddOrUpdate(() => companyApp.AFDScheduled(), "0 1 * * *", TimeZoneInfo.FindSystemTimeZoneById("E. South America Standard Time"));
            }
        }
    }
}
