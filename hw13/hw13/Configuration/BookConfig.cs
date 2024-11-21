using hw13.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hw13.Configuration
{
    public class BookConfig : IEntityTypeConfiguration<Book>
    {
        public void Configure(EntityTypeBuilder<Book> builder)
        {
            builder.HasKey(b => b.Id); 
            builder.Property(b => b.Name)
                  .IsRequired()
                  .HasMaxLength(200); 

            builder.Property(b => b.Author)
                  .IsRequired()
                  .HasMaxLength(100);

           
            builder.HasOne(b => b.User)
                  .WithMany(u => u.Books)
                  .HasForeignKey(b => b.UserID)
                  .OnDelete(DeleteBehavior.SetNull); 
        }
    }
}
