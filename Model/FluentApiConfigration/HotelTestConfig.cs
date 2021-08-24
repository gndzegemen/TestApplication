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
    public class HotelTestConfig : IEntityTypeConfiguration<HotelTest>
    {
        public void Configure(EntityTypeBuilder<HotelTest> modelBuilder)
        {
            modelBuilder.HasKey(x => x.HotelTestId);

            modelBuilder.Property(x => x.HotelId);
            modelBuilder.Property(x => x.TestId);
            
            
        }
    }
}
