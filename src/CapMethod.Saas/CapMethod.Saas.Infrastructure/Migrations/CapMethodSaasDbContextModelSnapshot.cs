using System;
using CapMethod.Saas.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

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
            b.Property<Guid>("Id")
                .ValueGeneratedNever()
                .HasColumnType("uuid")
                .HasColumnName("id");

            b.Property<DateTimeOffset>("CreatedAtUtc")
                .HasColumnType("timestamp with time zone")
                .HasColumnName("created_at_utc");

            b.Property<string>("Email")
                .HasMaxLength(320)
                .HasColumnType("character varying(320)")
                .HasColumnName("email");

            b.Property<string>("FirstName")
                .IsRequired()
                .HasMaxLength(150)
                .HasColumnType("character varying(150)")
                .HasColumnName("first_name");

            b.Property<string>("LastName")
                .IsRequired()
                .HasMaxLength(150)
                .HasColumnType("character varying(150)")
                .HasColumnName("last_name");

            b.Property<Guid>("TenantId")
                .HasColumnType("uuid")
                .HasColumnName("tenant_id");

            b.HasKey("Id")
                .HasName("pk_beneficiaries");

            b.HasIndex("TenantId")
                .HasDatabaseName("ix_beneficiaries_tenant_id");

            b.ToTable("beneficiaries");
        });

        modelBuilder.Entity("CapMethod.Saas.Domain.Sessions.CapSession", b =>
        {
            b.Property<Guid>("Id")
                .ValueGeneratedNever()
                .HasColumnType("uuid")
                .HasColumnName("id");

            b.Property<Guid>("BeneficiaryId")
                .HasColumnType("uuid")
                .HasColumnName("beneficiary_id");

            b.Property<Guid>("ConsultantId")
                .HasColumnType("uuid")
                .HasColumnName("consultant_id");

            b.Property<DateTimeOffset>("CreatedAtUtc")
                .HasColumnType("timestamp with time zone")
                .HasColumnName("created_at_utc");

            b.Property<bool>("IsAiEnabled")
                .HasColumnType("boolean")
                .HasColumnName("is_ai_enabled");

            b.Property<string>("Status")
                .IsRequired()
                .HasMaxLength(80)
                .HasColumnType("character varying(80)")
                .HasColumnName("status");

            b.Property<Guid>("TenantId")
                .HasColumnType("uuid")
                .HasColumnName("tenant_id");

            b.HasKey("Id")
                .HasName("pk_cap_sessions");

            b.HasIndex("TenantId", "BeneficiaryId")
                .HasDatabaseName("ix_cap_sessions_tenant_id_beneficiary_id");

            b.HasIndex("TenantId", "Id")
                .HasDatabaseName("ix_cap_sessions_tenant_id_id");

            b.ToTable("cap_sessions");
        });
    }
}
