using System;
using CapMethod.Saas.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

#nullable disable

namespace CapMethod.Saas.Infrastructure.Migrations;

[DbContext(typeof(CapMethodSaasDbContext))]
public partial class CapMethodSaasDbContextModelSnapshot : ModelSnapshot
{
    protected override void BuildModel(ModelBuilder modelBuilder)
    {
        modelBuilder.HasAnnotation("ProductVersion", "10.0.0");

        modelBuilder.Entity("CapMethod.Saas.Domain.Beneficiaries.Beneficiary", b =>
        {
            b.Property<Guid>("Id").ValueGeneratedNever().HasColumnType("uuid").HasColumnName("id");
            b.Property<DateTimeOffset>("CreatedAtUtc").HasColumnType("timestamp with time zone").HasColumnName("created_at_utc");
            b.Property<string>("Email").HasMaxLength(256).HasColumnType("character varying(256)").HasColumnName("email");
            b.Property<string>("FirstName").IsRequired().HasMaxLength(120).HasColumnType("character varying(120)").HasColumnName("first_name");
            b.Property<string>("LastName").IsRequired().HasMaxLength(120).HasColumnType("character varying(120)").HasColumnName("last_name");
            b.Property<Guid>("TenantId").HasColumnType("uuid").HasColumnName("tenant_id");
            b.HasKey("Id");
            b.HasIndex("TenantId", "Id").IsUnique();
            b.ToTable("beneficiaries");
        });

        modelBuilder.Entity("CapMethod.Saas.Domain.Sessions.CapSession", b =>
        {
            b.Property<Guid>("Id").ValueGeneratedNever().HasColumnType("uuid").HasColumnName("id");
            b.Property<Guid>("BeneficiaryId").HasColumnType("uuid").HasColumnName("beneficiary_id");
            b.Property<Guid>("ConsultantId").HasColumnType("uuid").HasColumnName("consultant_id");
            b.Property<DateTimeOffset>("CreatedAtUtc").HasColumnType("timestamp with time zone").HasColumnName("created_at_utc");
            b.Property<bool>("IsAiEnabled").HasColumnType("boolean").HasColumnName("is_ai_enabled");
            b.Property<string>("Status").IsRequired().HasMaxLength(80).HasColumnType("character varying(80)").HasColumnName("status");
            b.Property<Guid>("TenantId").HasColumnType("uuid").HasColumnName("tenant_id");
            b.HasKey("Id");
            b.HasIndex("TenantId", "BeneficiaryId");
            b.HasIndex("TenantId", "Id").IsUnique();
            b.ToTable("cap_sessions");
        });

        modelBuilder.Entity("CapMethod.Saas.Infrastructure.Persistence.OperationalSnapshot", b =>
        {
            b.Property<Guid>("TenantId").HasColumnType("uuid").HasColumnName("tenant_id");
            b.Property<Guid>("BeneficiaryId").HasColumnType("uuid").HasColumnName("beneficiary_id");
            b.Property<string>("DocumentType").IsRequired().HasMaxLength(80).HasColumnType("character varying(80)").HasColumnName("document_type");
            b.Property<string>("DocumentKey").IsRequired().HasMaxLength(160).HasColumnType("character varying(160)").HasColumnName("document_key");
            b.Property<string>("PayloadJson").IsRequired().HasColumnType("jsonb").HasColumnName("payload_json");
            b.Property<DateTimeOffset>("UpdatedAtUtc").HasColumnType("timestamp with time zone").HasColumnName("updated_at_utc");
            b.HasKey("TenantId", "BeneficiaryId", "DocumentType", "DocumentKey");
            b.HasIndex("TenantId", "BeneficiaryId");
            b.ToTable("operational_snapshots");
        });
    }
}
