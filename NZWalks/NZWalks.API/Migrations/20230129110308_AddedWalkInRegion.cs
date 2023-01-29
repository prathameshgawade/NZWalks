using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NZWalks.API.Migrations
{
    public partial class AddedWalkInRegion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Regions_Regions_RegionId",
                table: "Regions");

            migrationBuilder.DropIndex(
                name: "IX_Regions_RegionId",
                table: "Regions");

            migrationBuilder.DropColumn(
                name: "RegionId",
                table: "Regions");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "RegionId",
                table: "Regions",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Regions_RegionId",
                table: "Regions",
                column: "RegionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Regions_Regions_RegionId",
                table: "Regions",
                column: "RegionId",
                principalTable: "Regions",
                principalColumn: "Id");
        }
    }
}
