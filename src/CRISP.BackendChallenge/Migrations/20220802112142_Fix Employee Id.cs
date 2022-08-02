using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CRISP.BackendChallenge.Migrations
{
    public partial class FixEmployeeId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Logins_Employees_EmployeeId",
                table: "Logins");

            migrationBuilder.DropColumn(
                name: "PersonId",
                table: "Logins");

            migrationBuilder.AlterColumn<int>(
                name: "EmployeeId",
                table: "Logins",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "Logins",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "EmployeeId", "LoginDate" },
                values: new object[] { 1, new DateTime(2022, 7, 2, 7, 21, 42, 195, DateTimeKind.Local).AddTicks(6415) });

            migrationBuilder.UpdateData(
                table: "Logins",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "EmployeeId", "LoginDate" },
                values: new object[] { 1, new DateTime(2022, 6, 2, 7, 21, 42, 195, DateTimeKind.Local).AddTicks(6442) });

            migrationBuilder.UpdateData(
                table: "Logins",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "EmployeeId", "LoginDate" },
                values: new object[] { 1, new DateTime(2022, 5, 2, 7, 21, 42, 195, DateTimeKind.Local).AddTicks(6443) });

            migrationBuilder.UpdateData(
                table: "Logins",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "EmployeeId", "LoginDate" },
                values: new object[] { 2, new DateTime(2022, 7, 2, 7, 21, 42, 195, DateTimeKind.Local).AddTicks(6445) });

            migrationBuilder.UpdateData(
                table: "Logins",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "EmployeeId", "LoginDate" },
                values: new object[] { 2, new DateTime(2022, 6, 2, 7, 21, 42, 195, DateTimeKind.Local).AddTicks(6447) });

            migrationBuilder.UpdateData(
                table: "Logins",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "EmployeeId", "LoginDate" },
                values: new object[] { 3, new DateTime(2022, 7, 2, 7, 21, 42, 195, DateTimeKind.Local).AddTicks(6449) });

            migrationBuilder.AddForeignKey(
                name: "FK_Logins_Employees_EmployeeId",
                table: "Logins",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Logins_Employees_EmployeeId",
                table: "Logins");

            migrationBuilder.AlterColumn<int>(
                name: "EmployeeId",
                table: "Logins",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AddColumn<int>(
                name: "PersonId",
                table: "Logins",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "Logins",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "EmployeeId", "LoginDate", "PersonId" },
                values: new object[] { null, new DateTime(2022, 6, 22, 14, 35, 58, 191, DateTimeKind.Local).AddTicks(8767), 1 });

            migrationBuilder.UpdateData(
                table: "Logins",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "EmployeeId", "LoginDate", "PersonId" },
                values: new object[] { null, new DateTime(2022, 5, 22, 14, 35, 58, 191, DateTimeKind.Local).AddTicks(8810), 1 });

            migrationBuilder.UpdateData(
                table: "Logins",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "EmployeeId", "LoginDate", "PersonId" },
                values: new object[] { null, new DateTime(2022, 4, 22, 14, 35, 58, 191, DateTimeKind.Local).AddTicks(8813), 1 });

            migrationBuilder.UpdateData(
                table: "Logins",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "EmployeeId", "LoginDate", "PersonId" },
                values: new object[] { null, new DateTime(2022, 6, 22, 14, 35, 58, 191, DateTimeKind.Local).AddTicks(8816), 2 });

            migrationBuilder.UpdateData(
                table: "Logins",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "EmployeeId", "LoginDate", "PersonId" },
                values: new object[] { null, new DateTime(2022, 5, 22, 14, 35, 58, 191, DateTimeKind.Local).AddTicks(8819), 2 });

            migrationBuilder.UpdateData(
                table: "Logins",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "EmployeeId", "LoginDate", "PersonId" },
                values: new object[] { null, new DateTime(2022, 6, 22, 14, 35, 58, 191, DateTimeKind.Local).AddTicks(8822), 3 });

            migrationBuilder.AddForeignKey(
                name: "FK_Logins_Employees_EmployeeId",
                table: "Logins",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "Id");
        }
    }
}
