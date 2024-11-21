using hw13.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hw13.Configuration;

public class UserConfig : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(u => u.Id);

        builder.Property(u => u.UserName)
              .IsRequired()
              .HasMaxLength(100);


        builder.Property(u => u.Password)
              .IsRequired()
              .HasMaxLength(200);


        builder.Property(u => u.Role)
              .IsRequired();


        builder.Property(u => u.BorrowLimitEndDate)
              .IsRequired()
              .HasDefaultValueSql("DATEADD(DAY, 30, GETDATE())");


        builder.HasMany(u => u.Books)
              .WithOne(b => b.User)
              .HasForeignKey(b => b.UserID)
              .OnDelete(DeleteBehavior.SetNull);
    }

}
