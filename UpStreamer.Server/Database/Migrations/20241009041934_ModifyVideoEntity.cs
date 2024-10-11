using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UpStreamer.Server.Migrations
{
    /// <inheritdoc />
    public partial class ModifyVideoEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Video",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(160)",
                oldMaxLength: 160);

            migrationBuilder.AddColumn<string>(
                name: "DisplayFileName",
                table: "Video",
                type: "character varying(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FilePath",
                table: "Video",
                type: "character varying(255)",
                maxLength: 255,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DisplayFileName",
                table: "Video");

            migrationBuilder.DropColumn(
                name: "FilePath",
                table: "Video");

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Video",
                type: "character varying(160)",
                maxLength: 160,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(100)",
                oldMaxLength: 100);
        }
    }
}
