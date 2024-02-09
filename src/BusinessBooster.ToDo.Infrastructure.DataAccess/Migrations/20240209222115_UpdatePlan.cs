using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BusinessBooster.ToDo.Infrastructure.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class UpdatePlan : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tasks_status_record_tasks_PlannedTaskId",
                table: "tasks_status_record");

            migrationBuilder.DropForeignKey(
                name: "FK_tasks_status_record_tasks_TaskId",
                table: "tasks_status_record");

            migrationBuilder.DropPrimaryKey(
                name: "PK_tasks_status_record",
                table: "tasks_status_record");

            migrationBuilder.DropColumn(
                name: "RemovedAt",
                table: "plans");

            migrationBuilder.RenameTable(
                name: "tasks_status_record",
                newName: "tasks_status_records");

            migrationBuilder.RenameIndex(
                name: "IX_tasks_status_record_PlannedTaskId",
                table: "tasks_status_records",
                newName: "IX_tasks_status_records_PlannedTaskId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_tasks_status_records",
                table: "tasks_status_records",
                columns: new[] { "TaskId", "CreatedAt" });

            migrationBuilder.AddForeignKey(
                name: "FK_tasks_status_records_tasks_PlannedTaskId",
                table: "tasks_status_records",
                column: "PlannedTaskId",
                principalTable: "tasks",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_tasks_status_records_tasks_TaskId",
                table: "tasks_status_records",
                column: "TaskId",
                principalTable: "tasks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tasks_status_records_tasks_PlannedTaskId",
                table: "tasks_status_records");

            migrationBuilder.DropForeignKey(
                name: "FK_tasks_status_records_tasks_TaskId",
                table: "tasks_status_records");

            migrationBuilder.DropPrimaryKey(
                name: "PK_tasks_status_records",
                table: "tasks_status_records");

            migrationBuilder.RenameTable(
                name: "tasks_status_records",
                newName: "tasks_status_record");

            migrationBuilder.RenameIndex(
                name: "IX_tasks_status_records_PlannedTaskId",
                table: "tasks_status_record",
                newName: "IX_tasks_status_record_PlannedTaskId");

            migrationBuilder.AddColumn<DateTime>(
                name: "RemovedAt",
                table: "plans",
                type: "timestamp",
                nullable: true,
                comment: "For soft-deletes");

            migrationBuilder.AddPrimaryKey(
                name: "PK_tasks_status_record",
                table: "tasks_status_record",
                columns: new[] { "TaskId", "CreatedAt" });

            migrationBuilder.AddForeignKey(
                name: "FK_tasks_status_record_tasks_PlannedTaskId",
                table: "tasks_status_record",
                column: "PlannedTaskId",
                principalTable: "tasks",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_tasks_status_record_tasks_TaskId",
                table: "tasks_status_record",
                column: "TaskId",
                principalTable: "tasks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
