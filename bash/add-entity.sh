#!/bin/bash

path="../"
entity=$1
entityPlural=$2

## Mastership.Domain.Entities


file=$path"4-Domain/Mastership.Domain/Entities/"$entity".cs"

echo $file

tee $file > /dev/null << EOF
using System;

namespace Mastership.Domain.Entities
{
    public class ${entity} : BaseEntity
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

file=$path"/5-Infra/5.1-Data/Mastership.Infra.Data/Repositories/"$entity"Repository.cs"

echo $file

tee $file > /dev/null << EOF
using Mastership.Domain.Entities;
using Mastership.Infra.Data.Interfaces;
using Mastership.Infra.Data.Repositories;

namespace Rv.Database.Repositories
{
    public class ${entity}Repository : BaseRepository<${entity}>, I${entity}Repository
    {
        public ${entity}Repository(IDataUnitOfWork uow) : base(uow) { }

    }
}
EOF

