using DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLL.Data.Config
{
    internal class GenerConfigration : IEntityTypeConfiguration<Gener>
    {
        public void Configure(EntityTypeBuilder<Gener> builder)
        {
            builder.Property(G => G.Name).IsRequired();
       
        }
    }
}
