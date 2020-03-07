using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace API.Migrations
{
    public partial class addModelParticipant : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BirthDate",
                table: "TB_M_Employees");

            migrationBuilder.RenameColumn(
                name: "LastName",
                table: "TB_M_Employees",
                newName: "PasswordHash");

            migrationBuilder.RenameColumn(
                name: "Gender",
                table: "TB_M_Employees",
                newName: "ParticipantId");

            migrationBuilder.RenameColumn(
                name: "FirstName",
                table: "TB_M_Employees",
                newName: "Participant");

            migrationBuilder.RenameColumn(
                name: "Address",
                table: "TB_M_Employees",
                newName: "NIK");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PasswordHash",
                table: "TB_M_Employees",
                newName: "LastName");

            migrationBuilder.RenameColumn(
                name: "ParticipantId",
                table: "TB_M_Employees",
                newName: "Gender");

            migrationBuilder.RenameColumn(
                name: "Participant",
                table: "TB_M_Employees",
                newName: "FirstName");

            migrationBuilder.RenameColumn(
                name: "NIK",
                table: "TB_M_Employees",
                newName: "Address");

            migrationBuilder.AddColumn<DateTime>(
                name: "BirthDate",
                table: "TB_M_Employees",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
