using LibraryMgtApp.Context.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryMgtApp.Context.Maps
{
    public class BookMap : BaseEntityTypeConfiguration<Book>
    {
        public override void Configure(EntityTypeBuilder<Book> builder)
        {
            base.Configure(builder);
            builder.Property(t => t.Title).HasMaxLength(150);
            builder.Property(t => t.ISBN).HasMaxLength(150).IsUnicode(false);
        }
    }
}
