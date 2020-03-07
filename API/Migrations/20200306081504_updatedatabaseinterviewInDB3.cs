using Microsoft.EntityFrameworkCore.Migrations;

namespace API.Migrations
{
    public partial class updatedatabaseinterviewInDB3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "ParticipantId",
                table: "TB_T_Interviews",
                nullable: true,
                oldClrType: typeof(int));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "ParticipantId",
                table: "TB_T_Interviews",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
