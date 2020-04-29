using Microsoft.EntityFrameworkCore.Migrations;

namespace WRM.App.Migrations
{
    public partial class removeInfluence : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ZscoreChecks_MeasurementId_Threshold_NumDays_Influence",
                table: "ZscoreChecks");

            migrationBuilder.DropColumn(
                name: "Influence",
                table: "ZscoreChecks");

            migrationBuilder.CreateIndex(
                name: "IX_ZscoreChecks_MeasurementId_Threshold_NumDays",
                table: "ZscoreChecks",
                columns: new[] { "MeasurementId", "Threshold", "NumDays" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ZscoreChecks_MeasurementId_Threshold_NumDays",
                table: "ZscoreChecks");

            migrationBuilder.AddColumn<double>(
                name: "Influence",
                table: "ZscoreChecks",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.CreateIndex(
                name: "IX_ZscoreChecks_MeasurementId_Threshold_NumDays_Influence",
                table: "ZscoreChecks",
                columns: new[] { "MeasurementId", "Threshold", "NumDays", "Influence" },
                unique: true);
        }
    }
}
