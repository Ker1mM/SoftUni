using BillsPaymentSystem.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace BillsPaymentSystem.Data.EntityConfigurations
{
    public class BankAccountConfig : IEntityTypeConfiguration<BankAccount>
    {
        public void Configure(EntityTypeBuilder<BankAccount> builder)
        {
            builder.Property(bn => bn.BankName)
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode();

            builder.Property(s => s.SWIFT)
                .HasMaxLength(20)
                .IsRequired()
                .IsUnicode(false);
        }
    }
}
