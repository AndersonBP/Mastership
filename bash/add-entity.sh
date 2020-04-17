#!/bin/bash

path="../"
entity=$1
entityPlural=$2

## Mastership.Domain.Entities


file=$path"5-Infra/5.1-Data/Mastership.Infra.Data/Entities/"$entity"Entity.cs"

echo $file

tee $file > /dev/null << EOF
using System;

namespace Mastership.Infra.Data.Entities
{
    public class ${entity}Entity : BaseEntity
    {

    }
}
EOF


# Mastership.Database => Configuration

file=$path"5-Infra/5.1-Data/Mastership.Infra.Data/Configuration/"$entity"Config.cs"

echo $file

tee $file > /dev/null << EOF
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Mastership.Infra.Data.Configuration;
using Mastership.Infra.Data.Entities;

namespace Mastership.Database.Configuration
{
    public class ${entity}Config : BaseConfig<${entity}Entity>
    {
        public ${entity}Config() : base("${entity}") { }

        public override void Configure(EntityTypeBuilder<${entity}Entity> builder)
        {
            base.Configure(builder);

        }
    }
}
EOF

# Mastership.Database => Repositories

file=$path"/5-Infra/5.1-Data/Mastership.Infra.Data/Repositories/"$entity"Repository.cs"

echo $file

tee $file > /dev/null << EOF
using Mastership.Infra.Data.Entities;
using Mastership.Domain.Repository;
using Mastership.Infra.Data.Interfaces;
using Mastership.Infra.Data.Repositories;
using Mastership.Domain.DTO;
using AutoMapper;
using System.Linq;

namespace Mastership.Database.Repositories
{
    public class ${entity}Repository : BaseRepository<${entity}DTO, ${entity}Entity>, I${entity}Repository
    {
        public ${entity}Repository(IDataUnitOfWork uow, IMapper mapper) : base(uow, mapper) { }

    }
}
EOF

## Domain
# Mastership.Domain.ViewModel

file=$path"/4-Domain/Mastership.Domain/ViewModels/"$entity"ViewModel.cs"

echo $file

tee $file > /dev/null << EOF
using System;

namespace Mastership.Domain.ViewModels
{
    public class ${entity}ViewModel : BaseViewModel
    {

    }
}
EOF

# Rv.Domain => DTO

file=$path"/4-Domain/Mastership.Domain/DTO/"$entity"DTO.cs"

echo $file

tee $file > /dev/null << EOF
using System;
using System.Collections.Generic;

namespace Mastership.Domain.DTO
{
    public class ${entity}DTO : BaseDTO
    {

    }
}
EOF


# Mastership.Domain => Repository

file=$path"/4-Domain/Mastership.Domain/Interfaces/Repository/I"$entity"Repository.cs"

echo $file

tee $file > /dev/null << EOF
using Mastership.Domain.DTO;
using Mastership.Domain.Interfaces.Repository;
using System;
using System.Linq;


namespace Mastership.Domain.Repository
{
    public interface I${entity}Repository : IRepository<${entity}DTO> { }
}
EOF


# Rv.Domain => Application

file=$path"/4-Domain/Mastership.Domain/Interfaces/Application/I"$entity"Application.cs"

echo $file

tee $file > /dev/null << EOF
using Mastership.Domain.ViewModels;

namespace Mastership.Domain.Interfaces.Application
{
    public interface I${entity}Application : IApplication<${entity}ViewModel> { }
}
EOF

## Application
# Mastership.Application => Applications

file=$path"/3-Application/Mastership.Application/Services/"$entity"Application.cs"
echo $file

tee $file > /dev/null << EOF
using AutoMapper;
using Mastership.Domain.Interfaces.Application;
using Mastership.Domain.Repository;
using Mastership.Domain.ViewModels;
using Mastership.Domain.Interfaces;
using Mastership.Domain.DTO;

namespace Mastership.Application.Services
{
    public class ${entity}Application : BaseApplication<${entity}ViewModel, ${entity}DTO, I${entity}Repository>, I${entity}Application
    {
        public ${entity}Application(I${entity}Repository repository, IMapper mapper, IUserDataService userDataService) : base(repository, mapper, userDataService) { }

    }
}
EOF

## Presentation

if [ -z "$entityPlural" ]
then
    echo "Ignorando os controllers"
    exit
fi

# Mastership.Services.Api => Controllers

file=$path"/2-Service/Mastership.Services.Api/Controllers/"$entityPlural"Controller.cs"

echo $file

tee $file > /dev/null << EOF
using Mastership.Domain.Interfaces.Application;
using Mastership.Domain.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.ComponentModel.DataAnnotations;
using System.Net;

namespace Mastership.Services.Api.Controllers
{
    [Route("api/${entityPlural,,}")]
    [ApiController]
    public class ${entityPlural}Controller : BaseController<${entity}ViewModel, I${entity}Application>
    {
        public ${entityPlural}Controller(I${entity}Application service) : base(service) { }
    }
}

EOF