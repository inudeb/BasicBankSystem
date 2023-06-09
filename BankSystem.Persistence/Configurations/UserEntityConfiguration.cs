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
    public class UserEntityConfiguration : IEntityTypeConfiguration<UserEntity>
    {
        public void Configure(EntityTypeBuilder<UserEntity> builder)
        {
            builder.HasKey(c => c.UserId);
            builder.Property(c=> c.UserId)
                .ValueGeneratedOnAdd() ;
            builder.HasMany(c => c.UserAccounts)
                .WithOne(c => c.User)
                .HasForeignKey(c => c.UserId);
        }
    }
}
