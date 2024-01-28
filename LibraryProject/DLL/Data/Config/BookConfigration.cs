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
    internal class BookConfigration : IEntityTypeConfiguration<Book>
    {
        public void Configure(EntityTypeBuilder<Book> builder)
        {
            builder.Property(B => B.Title).IsRequired();
            builder.Property(B=>B.Description).IsRequired().HasMaxLength(150);
            builder.Property(B=>B.GenerId).IsRequired();
            builder.Property(B=>B.AuthorId).IsRequired(false);
            builder.Property(B=>B.Price).HasColumnType("decimal(18,2)");
            builder.HasOne(B=>B.Author).WithMany(B=>B.Books).HasForeignKey(B=>B.AuthorId).OnDelete(DeleteBehavior.SetNull);
        }
    }
}
