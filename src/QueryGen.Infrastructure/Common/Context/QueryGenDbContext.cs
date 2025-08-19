using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using System.Reflection;
using QueryGen.Domain.User;
using QueryGen.Domain.Session;
using QueryGen.Domain.SessionHistory;

namespace QueryGen.Infrastructure.Common.Context;

public class QueryGenDbContext : DbContext
{

    public QueryGenDbContext(DbContextOptions<QueryGenDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        base.OnModelCreating(modelBuilder);
    }



    public DbSet<UserModel> Users { get; set; }

    public DbSet<SessionModel> Sessions { get; set; }

    public DbSet<SessionHistoryModel> SessionsHistories { get; set; }
}
