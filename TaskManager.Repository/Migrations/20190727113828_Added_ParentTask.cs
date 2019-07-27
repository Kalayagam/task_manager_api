using Microsoft.EntityFrameworkCore.Migrations;

namespace TaskManager.Repository.Migrations
{
    public partial class Added_ParentTask : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TaskDetails_ParentTask_ParentTaskId",
                table: "TaskDetails");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ParentTask",
                table: "ParentTask");

            migrationBuilder.RenameTable(
                name: "ParentTask",
                newName: "ParentTasks");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ParentTasks",
                table: "ParentTasks",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TaskDetails_ParentTasks_ParentTaskId",
                table: "TaskDetails",
                column: "ParentTaskId",
                principalTable: "ParentTasks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TaskDetails_ParentTasks_ParentTaskId",
                table: "TaskDetails");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ParentTasks",
                table: "ParentTasks");

            migrationBuilder.RenameTable(
                name: "ParentTasks",
                newName: "ParentTask");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ParentTask",
                table: "ParentTask",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TaskDetails_ParentTask_ParentTaskId",
                table: "TaskDetails",
                column: "ParentTaskId",
                principalTable: "ParentTask",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
