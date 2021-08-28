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
    public class HotelConfig : IEntityTypeConfiguration<Hotel>
    {
        public void Configure(EntityTypeBuilder<Hotel> modelBuilder)
        {
            modelBuilder.HasKey(x => x.HotelId);
            modelBuilder.Property(x => x.HotelName).IsRequired();
            modelBuilder.Property(x => x.HotelUrl).IsRequired();
        }
    }
}
