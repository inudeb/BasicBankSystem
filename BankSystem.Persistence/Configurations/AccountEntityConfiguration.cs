using BankSystem.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankSystem.Persistence.Configurations
{
    public class AccountEntityConfiguration : IEntityTypeConfiguration<AccountEntity>
    {
        public void Configure(EntityTypeBuilder<AccountEntity> builder)
        {
            builder.HasKey(c => c.AccountId);
            builder.Property(c => c.AccountId)
                .ValueGeneratedOnAdd();

            builder.HasMany(c => c.Transactions)
                .WithOne(c => c.Account)
                .HasForeignKey(c => c.AccountId);
        }
    }
}
