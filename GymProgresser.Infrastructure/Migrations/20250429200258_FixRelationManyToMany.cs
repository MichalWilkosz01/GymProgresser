using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GymProgresser.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class FixRelationManyToMany : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_ExercisesWorkouts_ExerciseId",
                table: "ExercisesWorkouts",
                column: "ExerciseId");

            migrationBuilder.CreateIndex(
                name: "IX_ExercisesWorkouts_WorkoutId",
                table: "ExercisesWorkouts",
                column: "WorkoutId");

            migrationBuilder.AddForeignKey(
                name: "FK_ExercisesWorkouts_Exercises_ExerciseId",
                table: "ExercisesWorkouts",
                column: "ExerciseId",
                principalTable: "Exercises",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ExercisesWorkouts_Workouts_WorkoutId",
                table: "ExercisesWorkouts",
                column: "WorkoutId",
                principalTable: "Workouts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ExercisesWorkouts_Exercises_ExerciseId",
                table: "ExercisesWorkouts");

            migrationBuilder.DropForeignKey(
                name: "FK_ExercisesWorkouts_Workouts_WorkoutId",
                table: "ExercisesWorkouts");

            migrationBuilder.DropIndex(
                name: "IX_ExercisesWorkouts_ExerciseId",
                table: "ExercisesWorkouts");

            migrationBuilder.DropIndex(
                name: "IX_ExercisesWorkouts_WorkoutId",
                table: "ExercisesWorkouts");
        }
    }
}
