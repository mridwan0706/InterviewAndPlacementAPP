using Microsoft.EntityFrameworkCore.Migrations;

namespace API.Migrations
{
    public partial class updatedatabaseinterviewInDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Participant",
                table: "TB_T_Interviews",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Participant",
                table: "TB_T_Interviews");
        }
    }
}
