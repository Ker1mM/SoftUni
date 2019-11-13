using BillsPaymentSystem.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace BillsPaymentSystem.Data.EntityConfigurations
{
    public class UserConfig : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.Property(f => f.FirstName)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(l => l.LastName)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(e => e.Email)
                .IsRequired()
                .HasMaxLength(80)
                .IsUnicode(false);

            builder.Property(p => p.Password)
                .IsRequired()
                .HasMaxLength(25)
                .IsUnicode(false);
        }
    }
}
