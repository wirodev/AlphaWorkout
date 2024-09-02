﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AlphaWorkout.Data.Migrations
{
    /// <inheritdoc />
    public partial class RemovePreferredSplit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PreferredSplit",
                table: "Onboardings");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PreferredSplit",
                table: "Onboardings",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
