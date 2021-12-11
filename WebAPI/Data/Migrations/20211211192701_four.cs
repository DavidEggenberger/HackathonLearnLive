using Microsoft.EntityFrameworkCore.Migrations;

namespace WebAPI.Data.Migrations
{
    public partial class four : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Learning",
                table: "LearningNote",
                newName: "LearningText");

            migrationBuilder.RenameColumn(
                name: "Text",
                table: "GroupMessage",
                newName: "TextMessage");

            migrationBuilder.AddColumn<bool>(
                name: "MobilePushNotifications",
                table: "AspNetUsers",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MobilePushNotifications",
                table: "AspNetUsers");

            migrationBuilder.RenameColumn(
                name: "LearningText",
                table: "LearningNote",
                newName: "Learning");

            migrationBuilder.RenameColumn(
                name: "TextMessage",
                table: "GroupMessage",
                newName: "Text");
        }
    }
}
