using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace eHospitalServer.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class mg6 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_doctor_details_users_user_id",
                table: "doctor_details");

            migrationBuilder.RenameColumn(
                name: "user_id",
                table: "doctor_details",
                newName: "id");

            migrationBuilder.CreateIndex(
                name: "ix_users_doctor_detail_id",
                table: "users",
                column: "doctor_detail_id");

            migrationBuilder.AddForeignKey(
                name: "fk_users_doctor_details_doctor_detail_id",
                table: "users",
                column: "doctor_detail_id",
                principalTable: "doctor_details",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_users_doctor_details_doctor_detail_id",
                table: "users");

            migrationBuilder.DropIndex(
                name: "ix_users_doctor_detail_id",
                table: "users");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "doctor_details",
                newName: "user_id");

            migrationBuilder.AddForeignKey(
                name: "fk_doctor_details_users_user_id",
                table: "doctor_details",
                column: "user_id",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
