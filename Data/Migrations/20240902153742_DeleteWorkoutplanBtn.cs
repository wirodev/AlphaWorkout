using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AlphaWorkout.Data.Migrations
{
    /// <inheritdoc />
    public partial class DeleteWorkoutplanBtn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PastInjuryDetails",
                table: "Onboardings");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PastInjuryDetails",
                table: "Onboardings",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
