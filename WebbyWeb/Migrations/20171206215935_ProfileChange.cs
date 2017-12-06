using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace WebbyWeb.Migrations
{
    public partial class ProfileChange : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Habit_Profile_ProfileID",
                table: "Habit");

            migrationBuilder.DropIndex(
                name: "IX_Habit_ProfileID",
                table: "Habit");

            migrationBuilder.DropColumn(
                name: "ProfileID",
                table: "Habit");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ProfileID",
                table: "Habit",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Habit_ProfileID",
                table: "Habit",
                column: "ProfileID");

            migrationBuilder.AddForeignKey(
                name: "FK_Habit_Profile_ProfileID",
                table: "Habit",
                column: "ProfileID",
                principalTable: "Profile",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
