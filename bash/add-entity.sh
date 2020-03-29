#!/bin/bash

path="src"
entity=$1
entityPlural=$2

## Rv.Infra

# Rv.Infra => Entities

file=$path"/infra/Rv.Infra/Entities/"$entity".cs"

echo $file

tee $file > /dev/null << EOF
using System;
using Rv.Infra.Abstract;

namespace Rv.Infra.Entities
{
    public class ${entity} : BaseEntity
    {

    }
}
EOF

# Rv.Infra => Mapping

file=$path"/infra/Rv.Infra/Mapping/"$entity"Profile.cs"

echo $file

tee $file > /dev/null << EOF
using AutoMapper;
using Rv.Domain.DTO;
using Rv.Infra.Entities;

namespace Rv.Infra.Mapping
{
    public class ${entity}Profile : Profile
    {
        public ${entity}Profile()
        {
            CreateMap<${entity}, ${entity}DTO>().ReverseMap();
        }
    }
}
EOF

# Rv.Database => Configuration

file=$path"/infra/Rv.Database/Configuration/"$entity"Config.cs"

echo $file

tee $file > /dev/null << EOF
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Rv.Infra.Configuration.Abstract;
using Rv.Infra.Entities;

namespace Rv.Database.Configuration
{
    public class ${entity}Config : BaseConfig<${entity}>
    {
        public ${entity}Config() : base("${entity}") { }

        public override void Configure(EntityTypeBuilder<${entity}> builder)
        {
            base.Configure(builder);

        }
    }
}
EOF

# Rv.Database => Repositories

file=$path"/infra/Rv.Database/Repositories/"$entity"Repository.cs"

echo $file

tee $file > /dev/null << EOF
using Rv.Database.Interface;
using Rv.Domain.Repository;
using Rv.Infra.Entities;
using Rv.Infra.Repositories.Abstract;

namespace Rv.Database.Repositories
{
    public class ${entity}Repository : BaseRepository<${entity}>, I${entity}Repository
    {
        public ${entity}Repository(IDataUnitOfWork uow) : base(uow) { }

    }
}
EOF

## Domain

# Rv.Domain.DTO

file=$path"/domain/Rv.Domain.DTO/"$entity"DTO.cs"

echo $file

tee $file > /dev/null << EOF
using System;
using Rv.Domain.DTO.Abstract;

namespace Rv.Domain.DTO
{
    public class ${entity}DTO : BaseDTO
    {

    }
}
EOF

# Rv.Domain => Repository

file=$path"/domain/Rv.Domain/Repository/I"$entity"Repository.cs"

echo $file

tee $file > /dev/null << EOF
using Rv.Infra.Entities;

namespace Rv.Domain.Repository
{
    public interface I${entity}Repository : IRepository<${entity}> { }
}
EOF


# Rv.Domain => Service

file=$path"/domain/Rv.Domain/Service/I"$entity"Service.cs"

echo $file

tee $file > /dev/null << EOF
using Rv.Domain.DTO;

namespace Rv.Domain.Service
{
    public interface I${entity}Service : IServiceDTO<${entity}DTO> { }
}
EOF

## Application

# Rv.Service => Services

file=$path"/application/Rv.Service/Services/"$entity"Service.cs"

echo $file

tee $file > /dev/null << EOF
using Rv.Domain.DTO;
using Rv.Domain.Repository;
using Rv.Domain.Service;
using Rv.Infra.Entities;
using Rv.Service.Services.Abstract;

namespace Rv.Service.Services
{
    public class ${entity}Service : BaseService<${entity}DTO, ${entity}, I${entity}Repository>, I${entity}Service
    {
        public ${entity}Service(I${entity}Repository repository) : base(repository) { }

    }
}
EOF

## Presentation

if [ -z "$entityPlural" ]
then
    echo "Ignorando os controllers"
    exit
fi

# Rv.Api => Controllers

file=$path"/presentation/Rv.Api/Controllers/"$entityPlural"Controller.cs"

echo $file

tee $file > /dev/null << EOF
using Microsoft.AspNetCore.Mvc;
using Rv.Api.Controllers.Base;
using Rv.Domain.DTO;
using Rv.Domain.Service;

namespace Rv.Api.Controllers
{
    [Route("api/${entityPlural,,}")]
    [ApiController]
    public class ${entityPlural}Controller : BaseController<${entity}DTO, I${entity}Service>
    {
        public ${entityPlural}Controller(I${entity}Service service) : base(service) { }
    }
}

EOF
