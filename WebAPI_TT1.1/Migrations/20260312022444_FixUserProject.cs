using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebAPI_TT1._1.Migrations
{
    /// <inheritdoc />
    public partial class FixUserProject : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Usersss_Projects_ProjectId",
                table: "Usersss");

            migrationBuilder.DropIndex(
                name: "IX_Usersss_ProjectId",
                table: "Usersss");

            migrationBuilder.DropColumn(
                name: "ProjectId",
                table: "Usersss");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ProjectId",
                table: "Usersss",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Usersss_ProjectId",
                table: "Usersss",
                column: "ProjectId");

            migrationBuilder.AddForeignKey(
                name: "FK_Usersss_Projects_ProjectId",
                table: "Usersss",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
