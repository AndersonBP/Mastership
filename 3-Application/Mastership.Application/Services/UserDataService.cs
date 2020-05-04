using Mastership.Domain.Interfaces;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mastership.Application.Services
{
    public class UserDataService : IUserDataService
    {

        public UserDataService(
        )
        {
            RequestIdentity = Guid.NewGuid();
        }

        private ILookup<string, string> _lookup;

        public void Load(ILookup<string, string> lookup)
        {
                _lookup = lookup;
        }
        
        public Guid RequestIdentity { get; private set; }
        public Guid SubsidiaryId
        {
            get
            {
                return Guid.Parse(_lookup?["subsidiary"].FirstOrDefault());
            }
        }

        //private Guid _subsidiaryId = Guid.Empty;
        //public Guid SubsidiaryId
        //{
        //    get
        //    {
        //        var sub = _lookup?["subsidiary"].FirstOrDefault();
        //        return sub != null ? Guid.Parse(sub) : _subsidiaryId;
        //    }
        //    set { _subsidiaryId = value; }
        //}

    }
}