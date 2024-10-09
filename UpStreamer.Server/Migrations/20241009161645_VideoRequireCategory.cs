using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UpStreamer.Server.Migrations
{
    /// <inheritdoc />
    public partial class VideoRequireCategory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Video_Category_CategoryId",
                table: "Video");

            migrationBuilder.AlterColumn<int>(
                name: "CategoryId",
                table: "Video",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Video_Category_CategoryId",
                table: "Video",
                column: "CategoryId",
                principalTable: "Category",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Video_Category_CategoryId",
                table: "Video");

            migrationBuilder.AlterColumn<int>(
                name: "CategoryId",
                table: "Video",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddForeignKey(
                name: "FK_Video_Category_CategoryId",
                table: "Video",
                column: "CategoryId",
                principalTable: "Category",
                principalColumn: "Id");
        }
    }
}
