using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ExaminationProject.Migrations
{
    /// <inheritdoc />
    public partial class UserId_Updated : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserGroups_Users_UserId1",
                table: "UserGroups");

            migrationBuilder.AlterColumn<string>(
                name: "UserId1",
                table: "UserGroups",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_UserGroups_Users_UserId1",
                table: "UserGroups",
                column: "UserId1",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserGroups_Users_UserId1",
                table: "UserGroups");

            migrationBuilder.AlterColumn<string>(
                name: "UserId1",
                table: "UserGroups",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddForeignKey(
                name: "FK_UserGroups_Users_UserId1",
                table: "UserGroups",
                column: "UserId1",
                principalTable: "Users",
                principalColumn: "Id");
        }
    }
}
