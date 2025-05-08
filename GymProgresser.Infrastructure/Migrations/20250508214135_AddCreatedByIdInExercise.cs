using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GymProgresser.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddCreatedByIdInExercise : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CreatedById",
                table: "Exercises",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedById",
                table: "Exercises");
        }
    }
}
