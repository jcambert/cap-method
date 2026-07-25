using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CapMethod.Saas.Infrastructure.Migrations;

public partial class AddOperationalSnapshots : Migration
{
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "operational_snapshots",
            columns: table => new
            {
                tenant_id = table.Column<Guid>(type: "uuid", nullable: false),
                beneficiary_id = table.Column<Guid>(type: "uuid", nullable: false),
                document_type = table.Column<string>(type: "character varying(80)", maxLength: 80, nullable: false),
                document_key = table.Column<string>(type: "character varying(160)", maxLength: 160, nullable: false),
                payload_json = table.Column<string>(type: "jsonb", nullable: false),
                updated_at_utc = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_operational_snapshots", item => new { item.tenant_id, item.beneficiary_id, item.document_type, item.document_key });
            });

        migrationBuilder.CreateIndex(
            name: "IX_operational_snapshots_tenant_id_beneficiary_id",
            table: "operational_snapshots",
            columns: new[] { "tenant_id", "beneficiary_id" });
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(name: "operational_snapshots");
    }
}
