using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StudyBuddy.Repositories.Migrations
{
    /// <inheritdoc />
    public partial class AddIsArchivedColumnToPosts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "ArchivedOn",
                table: "Posts",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "IsArchived",
                table: "Posts",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ArchivedOn",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "IsArchived",
                table: "Posts");
        }
    }
}
