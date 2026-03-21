using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebAPI_TT1._1.Migrations
{
    /// <inheritdoc />
    public partial class hehehe : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Password",
                table: "Usersss");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Password",
                table: "Usersss",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
