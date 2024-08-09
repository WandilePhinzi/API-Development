using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NWUTechTrends.Migrations.ZaazrNwutechTrends
{
    /// <inheritdoc />
    public partial class UpdateCostSavedAndTimeSavedPrecision : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ProjectId",
                schema: "Telemetry",
                table: "JobTelemetry",
                newName: "ProjectID");

            migrationBuilder.RenameColumn(
                name: "ClientId",
                schema: "Telemetry",
                table: "JobTelemetry",
                newName: "ClientID");

            migrationBuilder.AlterColumn<decimal>(
                name: "TimeSaved",
                schema: "Telemetry",
                table: "JobTelemetry",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ProjectID",
                schema: "Telemetry",
                table: "JobTelemetry",
                newName: "ProjectId");

            migrationBuilder.RenameColumn(
                name: "ClientID",
                schema: "Telemetry",
                table: "JobTelemetry",
                newName: "ClientId");

            migrationBuilder.AlterColumn<double>(
                name: "TimeSaved",
                schema: "Telemetry",
                table: "JobTelemetry",
                type: "float",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");
        }
    }
}
