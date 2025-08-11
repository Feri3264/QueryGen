using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QueryGen.Domain.User;

namespace QueryGen.Infrastructure.User;

public class UserConfig : IEntityTypeConfiguration<UserModel>
{
    public void Configure(EntityTypeBuilder<UserModel> builder)
    {
        builder.HasKey(u => u.Id);
        builder.Property(u => u.Id).ValueGeneratedNever();

        builder.Property(u => u.Username).HasMaxLength(50).IsRequired();
        builder.Property(u => u.Password).HasMaxLength(150).IsRequired();
        builder.Property(u => u.RefreshToken).IsRequired();
    }
}
