using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Models.Data;
using Models.Mpdels;


namespace Models
{
    public class DepartmentConf : IEntityTypeConfiguration<Department>
    {
        public void Configure(EntityTypeBuilder<Department> builder)
        {
            builder.HasKey(e => e.Id); //PK
            builder.Property(e => e.Name).IsRequired().HasColumnType("VARCHAR").HasMaxLength(30);
            builder.Property(x => x.ManagerName).IsRequired();

        }

    }
}
