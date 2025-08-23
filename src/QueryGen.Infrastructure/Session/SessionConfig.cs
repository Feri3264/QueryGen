using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QueryGen.Domain.Session;
using QueryGen.Domain.User;

namespace QueryGen.Infrastructure.Session;

public class SessionConfig : IEntityTypeConfiguration<SessionModel>
{
    public void Configure(EntityTypeBuilder<SessionModel> builder)
    {
        builder.HasKey(s => s.Id);
        builder.Property(s => s.Id).ValueGeneratedNever();

        builder.Property(s => s.Name).HasMaxLength(50).IsRequired();
        builder.Property(s => s.ConnectionString).IsRequired();
        builder.Property(s => s.Metadata).IsRequired();
        builder.Property(s => s.DbType).HasConversion<string>();
        builder.Property(s => s.LlmType).HasConversion<string>();


        //navigation
        builder.HasOne<UserModel>()
            .WithMany()
            .HasForeignKey(s => s.UserId);
    }
}
