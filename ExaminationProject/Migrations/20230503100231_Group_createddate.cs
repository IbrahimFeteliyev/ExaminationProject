using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ExaminationProject.Migrations
{
    /// <inheritdoc />
    public partial class Group_createddate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "GroupCreatedDate",
                table: "Groups",
                newName: "CreatedDate");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CreatedDate",
                table: "Groups",
                newName: "GroupCreatedDate");
        }
    }
}
