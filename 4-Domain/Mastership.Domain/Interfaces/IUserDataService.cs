using System;
using System.Linq;

namespace Mastership.Domain.Interfaces
{
    public interface IUserDataService
    {
        Guid SubsidiaryId { get; }

        Guid RequestIdentity { get; }

        void Load(ILookup<string, string> lookup);
    }
}
