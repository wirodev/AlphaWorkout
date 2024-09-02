using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AlphaWorkout.Data.Migrations
{
    /// <inheritdoc />
    public partial class SaveWorkoutplan : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "WorkoutPlan",
                table: "Onboardings",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "WorkoutPlan",
                table: "Onboardings");
        }
    }
}
