using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CapMethod.Saas.Infrastructure.Migrations;

public partial class InitialSaasPersistence : Migration
{
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "beneficiaries",
            columns: table => new
            {
                id = table.Column<Guid>(type: "uuid", nullable: false),
                tenant_id = table.Column<Guid>(type: "uuid", nullable: false),
                first_name = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                last_name = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                email = table.Column<string>(type: "character varying(320)", maxLength: 320, nullable: true),
                created_at_utc = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("pk_beneficiaries", x => x.id);
            });

        migrationBuilder.CreateTable(
            name: "cap_sessions",
            columns: table => new
            {
                id = table.Column<Guid>(type: "uuid", nullable: false),
                tenant_id = table.Column<Guid>(type: "uuid", nullable: false),
                beneficiary_id = table.Column<Guid>(type: "uuid", nullable: false),
                consultant_id = table.Column<Guid>(type: "uuid", nullable: false),
                status = table.Column<string>(type: "character varying(80)", maxLength: 80, nullable: false),
                is_ai_enabled = table.Column<bool>(type: "boolean", nullable: false),
                created_at_utc = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("pk_cap_sessions", x => x.id);
            });

        migrationBuilder.CreateIndex(
            name: "ix_beneficiaries_tenant_id",
            table: "beneficiaries",
            column: "tenant_id");

        migrationBuilder.CreateIndex(
            name: "ix_cap_sessions_tenant_id_beneficiary_id",
            table: "cap_sessions",
            columns: new[] { "tenant_id", "beneficiary_id" });

        migrationBuilder.CreateIndex(
            name: "ix_cap_sessions_tenant_id_id",
            table: "cap_sessions",
            columns: new[] { "tenant_id", "id" });
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(name: "cap_sessions");
        migrationBuilder.DropTable(name: "beneficiaries");
    }
}
