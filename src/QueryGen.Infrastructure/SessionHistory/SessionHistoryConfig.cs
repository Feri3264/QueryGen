using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QueryGen.Domain.Session;
using QueryGen.Domain.SessionHistory;

namespace QueryGen.Infrastructure.SessionHistory;

public class SessionHistoryConfig : IEntityTypeConfiguration<SessionHistoryModel>
{
    public void Configure(EntityTypeBuilder<SessionHistoryModel> builder)
    {
        builder.HasKey(h => h.Id);
        builder.Property(h => h.Id).ValueGeneratedNever();


        builder.Property(h => h.Prompt).IsRequired();
        builder.Property(h => h.Query).IsRequired();
        builder.Property(h => h.Result).IsRequired();

        //navigation
        builder.HasOne<SessionModel>()
            .WithMany()
            .HasForeignKey(h => h.SessionId);
    }
}
