using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UpStreamer.Server.Migrations
{
    /// <inheritdoc />
    public partial class ModifyVideoDescLengthAndField : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DisplayFileName",
                table: "Video");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Video",
                type: "character varying(255)",
                maxLength: 255,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(160)",
                oldMaxLength: 160,
                oldNullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Video",
                type: "character varying(160)",
                maxLength: 160,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(255)",
                oldMaxLength: 255,
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DisplayFileName",
                table: "Video",
                type: "character varying(100)",
                maxLength: 100,
                nullable: true);
        }
    }
}
