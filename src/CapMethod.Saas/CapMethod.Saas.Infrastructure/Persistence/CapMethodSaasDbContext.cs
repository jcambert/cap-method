using CapMethod.Saas.Domain.Beneficiaries;
using CapMethod.Saas.Domain.Sessions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace CapMethod.Saas.Infrastructure.Persistence;

public sealed class CapMethodSaasDbContext : DbContext
{
    public CapMethodSaasDbContext(DbContextOptions<CapMethodSaasDbContext> options)
        : base(options)
    {
    }

    public DbSet<Beneficiary> Beneficiaries => Set<Beneficiary>();

    public DbSet<CapSession> CapSessions => Set<CapSession>();

    public DbSet<OperationalSnapshot> OperationalSnapshots => Set<OperationalSnapshot>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        ConfigureBeneficiary(modelBuilder);
        ConfigureCapSession(modelBuilder);
        ConfigureOperationalSnapshot(modelBuilder);
    }

    private static void ConfigureBeneficiary(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Beneficiary>(entity =>
        {
            entity.ToTable("beneficiaries");
            entity.HasKey(beneficiary => beneficiary.Id);
            entity.Property(beneficiary => beneficiary.Id).HasColumnName("id").ValueGeneratedNever();
            entity.Property(beneficiary => beneficiary.TenantId).HasColumnName("tenant_id").IsRequired();
            entity.Property(beneficiary => beneficiary.FirstName).HasColumnName("first_name").HasMaxLength(120).IsRequired();
            entity.Property(beneficiary => beneficiary.LastName).HasColumnName("last_name").HasMaxLength(120).IsRequired();
            entity.Property(beneficiary => beneficiary.Email).HasColumnName("email").HasMaxLength(256);
            entity.Property(beneficiary => beneficiary.CreatedAtUtc).HasColumnName("created_at_utc").IsRequired();
            entity.HasIndex(beneficiary => new { beneficiary.TenantId, beneficiary.Id }).IsUnique();
        });
    }

    private static void ConfigureCapSession(ModelBuilder modelBuilder)
    {
        ValueConverter<CapSessionStatus, string> statusConverter = new(status => status.Code, code => new CapSessionStatus(code));
        modelBuilder.Entity<CapSession>(entity =>
        {
            entity.ToTable("cap_sessions");
            entity.HasKey(session => session.Id);
            entity.Property(session => session.Id).HasColumnName("id").ValueGeneratedNever();
            entity.Property(session => session.TenantId).HasColumnName("tenant_id").IsRequired();
            entity.Property(session => session.BeneficiaryId).HasColumnName("beneficiary_id").IsRequired();
            entity.Property(session => session.ConsultantId).HasColumnName("consultant_id").IsRequired();
            entity.Property(session => session.Status).HasColumnName("status").HasMaxLength(80).HasConversion(statusConverter).IsRequired();
            entity.Property(session => session.IsAiEnabled).HasColumnName("is_ai_enabled").IsRequired();
            entity.Property(session => session.CreatedAtUtc).HasColumnName("created_at_utc").IsRequired();
            entity.HasIndex(session => new { session.TenantId, session.Id }).IsUnique();
            entity.HasIndex(session => new { session.TenantId, session.BeneficiaryId });
        });
    }

    private static void ConfigureOperationalSnapshot(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<OperationalSnapshot>(entity =>
        {
            entity.ToTable("operational_snapshots");
            entity.HasKey(item => new { item.TenantId, item.BeneficiaryId, item.DocumentType, item.DocumentKey });
            entity.Property(item => item.TenantId).HasColumnName("tenant_id");
            entity.Property(item => item.BeneficiaryId).HasColumnName("beneficiary_id");
            entity.Property(item => item.DocumentType).HasColumnName("document_type").HasMaxLength(80);
            entity.Property(item => item.DocumentKey).HasColumnName("document_key").HasMaxLength(160);
            entity.Property(item => item.PayloadJson).HasColumnName("payload_json").HasColumnType("jsonb").IsRequired();
            entity.Property(item => item.UpdatedAtUtc).HasColumnName("updated_at_utc").IsRequired();
            entity.HasIndex(item => new { item.TenantId, item.BeneficiaryId });
        });
    }
}
