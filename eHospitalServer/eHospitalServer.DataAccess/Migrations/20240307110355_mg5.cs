using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace eHospitalServer.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class mg5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "epicrisisreport",
                table: "appointments",
                newName: "epicrisis_report");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "epicrisis_report",
                table: "appointments",
                newName: "epicrisisreport");
        }
    }
}
