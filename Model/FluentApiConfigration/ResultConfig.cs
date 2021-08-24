using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.FluentApiConfigration
{
    public class ResultConfig : IEntityTypeConfiguration<Result>
    {
        public void Configure(EntityTypeBuilder<Result> modelBuilder)
        {
            modelBuilder.HasKey(x => x.ResultId);
            modelBuilder.Property(x => x.HotelTestId);
            modelBuilder.Property(x => x.Output);
        }
    }
}
