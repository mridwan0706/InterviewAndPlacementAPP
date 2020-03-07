using Microsoft.EntityFrameworkCore.Migrations;

namespace API.Migrations
{
    public partial class updatedatabaseinterviewInDB2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TB_T_Interviews_TB_M_Employees_EmployeeId",
                table: "TB_T_Interviews");

            migrationBuilder.DropIndex(
                name: "IX_TB_T_Interviews_EmployeeId",
                table: "TB_T_Interviews");

            migrationBuilder.RenameColumn(
                name: "EmployeeId",
                table: "TB_T_Interviews",
                newName: "ParticipantId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ParticipantId",
                table: "TB_T_Interviews",
                newName: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_TB_T_Interviews_EmployeeId",
                table: "TB_T_Interviews",
                column: "EmployeeId");

            migrationBuilder.AddForeignKey(
                name: "FK_TB_T_Interviews_TB_M_Employees_EmployeeId",
                table: "TB_T_Interviews",
                column: "EmployeeId",
                principalTable: "TB_M_Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
