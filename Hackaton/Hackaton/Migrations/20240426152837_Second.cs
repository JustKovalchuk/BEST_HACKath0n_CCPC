using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hackaton.Migrations
{
    /// <inheritdoc />
    public partial class Second : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "surname",
                table: "UserData",
                newName: "Surname");

            migrationBuilder.RenameColumn(
                name: "password",
                table: "UserData",
                newName: "Password");

            migrationBuilder.RenameColumn(
                name: "name",
                table: "UserData",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "email",
                table: "UserData",
                newName: "Email");

            migrationBuilder.RenameColumn(
                name: "copyPassword",
                table: "UserData",
                newName: "CopyPassword");

            migrationBuilder.RenameColumn(
                name: "age",
                table: "UserData",
                newName: "Age");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "UserData",
                newName: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Surname",
                table: "UserData",
                newName: "surname");

            migrationBuilder.RenameColumn(
                name: "Password",
                table: "UserData",
                newName: "password");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "UserData",
                newName: "name");

            migrationBuilder.RenameColumn(
                name: "Email",
                table: "UserData",
                newName: "email");

            migrationBuilder.RenameColumn(
                name: "CopyPassword",
                table: "UserData",
                newName: "copyPassword");

            migrationBuilder.RenameColumn(
                name: "Age",
                table: "UserData",
                newName: "age");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "UserData",
                newName: "id");
        }
    }
}
