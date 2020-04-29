using Microsoft.EntityFrameworkCore.Migrations;

namespace WRM.App.Migrations
{
    public partial class checkConstraints : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ZscoreChecks_MeasurementId",
                table: "ZscoreChecks");

            migrationBuilder.DropIndex(
                name: "IX_ReasonabilityChecks_MeasurementId",
                table: "ReasonabilityChecks");

            migrationBuilder.DropIndex(
                name: "IX_NonNullChecks_MeasurementId",
                table: "NonNullChecks");

            migrationBuilder.CreateIndex(
                name: "IX_ZscoreChecks_MeasurementId_Threshold_NumDays_Influence",
                table: "ZscoreChecks",
                columns: new[] { "MeasurementId", "Threshold", "NumDays", "Influence" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ReasonabilityChecks_MeasurementId_MaxValue_MinValue",
                table: "ReasonabilityChecks",
                columns: new[] { "MeasurementId", "MaxValue", "MinValue" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_NonNullChecks_MeasurementId",
                table: "NonNullChecks",
                column: "MeasurementId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ZscoreChecks_MeasurementId_Threshold_NumDays_Influence",
                table: "ZscoreChecks");

            migrationBuilder.DropIndex(
                name: "IX_ReasonabilityChecks_MeasurementId_MaxValue_MinValue",
                table: "ReasonabilityChecks");

            migrationBuilder.DropIndex(
                name: "IX_NonNullChecks_MeasurementId",
                table: "NonNullChecks");

            migrationBuilder.CreateIndex(
                name: "IX_ZscoreChecks_MeasurementId",
                table: "ZscoreChecks",
                column: "MeasurementId");

            migrationBuilder.CreateIndex(
                name: "IX_ReasonabilityChecks_MeasurementId",
                table: "ReasonabilityChecks",
                column: "MeasurementId");

            migrationBuilder.CreateIndex(
                name: "IX_NonNullChecks_MeasurementId",
                table: "NonNullChecks",
                column: "MeasurementId");
        }
    }
}
