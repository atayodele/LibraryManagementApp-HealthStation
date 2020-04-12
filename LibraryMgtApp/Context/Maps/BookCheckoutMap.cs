using LibraryMgtApp.Context.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryMgtApp.Context.Maps
{
    public class BookCheckoutMap : BaseEntityTypeConfiguration<BookCheckout>
    {
        public override void Configure(EntityTypeBuilder<BookCheckout> builder)
        {
            base.Configure(builder);
            builder.HasKey(bc => new { bc.BookId, bc.CheckoutId });

            builder.HasOne(bc => bc.Book)
                .WithMany(b => b.BookCheckouts)
                .HasForeignKey(bc => bc.BookId);

            builder.HasOne(bc => bc.Checkout)
                .WithMany(c => c.BookCheckouts)
                .HasForeignKey(bc => bc.CheckoutId);
        }
    }
}
