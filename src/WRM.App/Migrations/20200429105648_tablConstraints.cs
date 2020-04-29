using Microsoft.EntityFrameworkCore.Migrations;

namespace WRM.App.Migrations
{
    public partial class tablConstraints : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ZscoreCheckResults_ZscoreCheckId",
                table: "ZscoreCheckResults");

            migrationBuilder.DropIndex(
                name: "IX_ReasonabilityCheckResults_ReasonabilityCheckId",
                table: "ReasonabilityCheckResults");

            migrationBuilder.DropIndex(
                name: "IX_NonNullCheckResults_NonNullCheckId",
                table: "NonNullCheckResults");

            migrationBuilder.CreateIndex(
                name: "IX_ZscoreCheckResults_ZscoreCheckId_DateOfCheck",
                table: "ZscoreCheckResults",
                columns: new[] { "ZscoreCheckId", "DateOfCheck" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ReasonabilityCheckResults_ReasonabilityCheckId_DateOfCheck",
                table: "ReasonabilityCheckResults",
                columns: new[] { "ReasonabilityCheckId", "DateOfCheck" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_NonNullCheckResults_NonNullCheckId_DateOfCheck",
                table: "NonNullCheckResults",
                columns: new[] { "NonNullCheckId", "DateOfCheck" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ZscoreCheckResults_ZscoreCheckId_DateOfCheck",
                table: "ZscoreCheckResults");

            migrationBuilder.DropIndex(
                name: "IX_ReasonabilityCheckResults_ReasonabilityCheckId_DateOfCheck",
                table: "ReasonabilityCheckResults");

            migrationBuilder.DropIndex(
                name: "IX_NonNullCheckResults_NonNullCheckId_DateOfCheck",
                table: "NonNullCheckResults");

            migrationBuilder.CreateIndex(
                name: "IX_ZscoreCheckResults_ZscoreCheckId",
                table: "ZscoreCheckResults",
                column: "ZscoreCheckId");

            migrationBuilder.CreateIndex(
                name: "IX_ReasonabilityCheckResults_ReasonabilityCheckId",
                table: "ReasonabilityCheckResults",
                column: "ReasonabilityCheckId");

            migrationBuilder.CreateIndex(
                name: "IX_NonNullCheckResults_NonNullCheckId",
                table: "NonNullCheckResults",
                column: "NonNullCheckId");
        }
    }
}
