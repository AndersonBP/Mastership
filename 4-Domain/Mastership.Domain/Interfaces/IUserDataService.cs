using System;
using System.Linq;

namespace Mastership.Domain.Interfaces
{
    public interface IUserDataService
    {
        string RawUserName { get; }
        string UserName { get; }
        string FullName { get; }

        Guid RequestIdentity { get; }

        void Load(ILookup<string, string> lookup);
    }
}
