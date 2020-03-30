#!/bin/bash

path="../"
entity=$1
entityPlural=$2

## Mastership.Domain.Entities


file=$path"4-Domain/Mastership.Domain/Entities/"$entity"Entity.cs"

echo $file

tee $file > /dev/null << EOF
using System;

namespace Mastership.Domain.Entities
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
using Mastership.Domain.Entities;
using Mastership.Infra.Data.Configuration;

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
using Mastership.Domain.Entities;
using Mastership.Domain.Repository;
using Mastership.Infra.Data.Interfaces;
using Mastership.Infra.Data.Repositories;

namespace Mastership.Database.Repositories
{
    public class ${entity}Repository : BaseRepository<${entity}Entity>, I${entity}Repository
    {
        public ${entity}Repository(IDataUnitOfWork uow) : base(uow) { }

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

# Mastership.Domain => Repository

file=$path"/4-Domain/Mastership.Domain/Interfaces/Repository/I"$entity"Repository.cs"

echo $file

tee $file > /dev/null << EOF
using Mastership.Domain.Entities;
using Mastership.Domain.Interfaces.Repository;

namespace Mastership.Domain.Repository
{
    public interface I${entity}Repository : IRepository<${entity}Entity> { }
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
using Mastership.Domain.Entities;
using Mastership.Domain.Interfaces.Application;
using Mastership.Domain.Repository;
using Mastership.Domain.ViewModels;

namespace Mastership.Application.Services
{
    public class ${entity}Application : BaseApplication<${entity}ViewModel, ${entity}Entity, I${entity}Repository>, I${entity}Application
    {
        public ${entity}Application(I${entity}Repository repository, IMapper mapper) : base(repository, mapper) { }

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
using Microsoft.AspNetCore.Mvc;

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