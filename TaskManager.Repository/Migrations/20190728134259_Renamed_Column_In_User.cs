using Microsoft.EntityFrameworkCore.Migrations;

namespace TaskManager.Repository.Migrations
{
    public partial class Renamed_Column_In_User : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_TaskDetails_TaskDetailsId",
                table: "Users");

            migrationBuilder.RenameColumn(
                name: "TaskDetailsId",
                table: "Users",
                newName: "TaskId");

            migrationBuilder.RenameIndex(
                name: "IX_Users_TaskDetailsId",
                table: "Users",
                newName: "IX_Users_TaskId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_TaskDetails_TaskId",
                table: "Users",
                column: "TaskId",
                principalTable: "TaskDetails",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_TaskDetails_TaskId",
                table: "Users");

            migrationBuilder.RenameColumn(
                name: "TaskId",
                table: "Users",
                newName: "TaskDetailsId");

            migrationBuilder.RenameIndex(
                name: "IX_Users_TaskId",
                table: "Users",
                newName: "IX_Users_TaskDetailsId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_TaskDetails_TaskDetailsId",
                table: "Users",
                column: "TaskDetailsId",
                principalTable: "TaskDetails",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
