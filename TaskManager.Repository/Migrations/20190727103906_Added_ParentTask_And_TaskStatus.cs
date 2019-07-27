using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TaskManager.Repository.Migrations
{
    public partial class Added_ParentTask_And_TaskStatus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ParentId",
                table: "TaskDetails");

            migrationBuilder.AddColumn<int>(
                name: "ParentTaskId",
                table: "TaskDetails",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "TaskDetails",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "ParentTask",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    TaskName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ParentTask", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TaskDetails_ParentTaskId",
                table: "TaskDetails",
                column: "ParentTaskId");

            migrationBuilder.AddForeignKey(
                name: "FK_TaskDetails_ParentTask_ParentTaskId",
                table: "TaskDetails",
                column: "ParentTaskId",
                principalTable: "ParentTask",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TaskDetails_ParentTask_ParentTaskId",
                table: "TaskDetails");

            migrationBuilder.DropTable(
                name: "ParentTask");

            migrationBuilder.DropIndex(
                name: "IX_TaskDetails_ParentTaskId",
                table: "TaskDetails");

            migrationBuilder.DropColumn(
                name: "ParentTaskId",
                table: "TaskDetails");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "TaskDetails");

            migrationBuilder.AddColumn<int>(
                name: "ParentId",
                table: "TaskDetails",
                nullable: false,
                defaultValue: 0);
        }
    }
}
