using Microsoft.EntityFrameworkCore.Migrations;

namespace API.Migrations
{
    public partial class Updatedatabase2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_TB_T_Interviews_EmployeeId",
                table: "TB_T_Interviews",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_TB_T_Interviews_SiteId",
                table: "TB_T_Interviews",
                column: "SiteId");

            migrationBuilder.AddForeignKey(
                name: "FK_TB_T_Interviews_TB_M_Employees_EmployeeId",
                table: "TB_T_Interviews",
                column: "EmployeeId",
                principalTable: "TB_M_Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TB_T_Interviews_TB_M_Sites_SiteId",
                table: "TB_T_Interviews",
                column: "SiteId",
                principalTable: "TB_M_Sites",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TB_T_Interviews_TB_M_Employees_EmployeeId",
                table: "TB_T_Interviews");

            migrationBuilder.DropForeignKey(
                name: "FK_TB_T_Interviews_TB_M_Sites_SiteId",
                table: "TB_T_Interviews");

            migrationBuilder.DropIndex(
                name: "IX_TB_T_Interviews_EmployeeId",
                table: "TB_T_Interviews");

            migrationBuilder.DropIndex(
                name: "IX_TB_T_Interviews_SiteId",
                table: "TB_T_Interviews");
        }
    }
}
