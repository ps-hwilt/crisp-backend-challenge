using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CRISP.BackendChallenge.Migrations
{
    public partial class inital : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Department = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Logins",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    PersonId = table.Column<int>(type: "INTEGER", nullable: false),
                    LoginDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    EmployeeId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Logins", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Logins_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "Id", "Department", "Name" },
                values: new object[] { 1, 1, "John Doe" });

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "Id", "Department", "Name" },
                values: new object[] { 2, 2, "Jane Doe" });

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "Id", "Department", "Name" },
                values: new object[] { 3, 1, "Joe Doe" });

            migrationBuilder.InsertData(
                table: "Logins",
                columns: new[] { "Id", "EmployeeId", "LoginDate", "PersonId" },
                values: new object[] { 1, null, new DateTime(2022, 6, 22, 14, 35, 58, 191, DateTimeKind.Local).AddTicks(8767), 1 });

            migrationBuilder.InsertData(
                table: "Logins",
                columns: new[] { "Id", "EmployeeId", "LoginDate", "PersonId" },
                values: new object[] { 2, null, new DateTime(2022, 5, 22, 14, 35, 58, 191, DateTimeKind.Local).AddTicks(8810), 1 });

            migrationBuilder.InsertData(
                table: "Logins",
                columns: new[] { "Id", "EmployeeId", "LoginDate", "PersonId" },
                values: new object[] { 3, null, new DateTime(2022, 4, 22, 14, 35, 58, 191, DateTimeKind.Local).AddTicks(8813), 1 });

            migrationBuilder.InsertData(
                table: "Logins",
                columns: new[] { "Id", "EmployeeId", "LoginDate", "PersonId" },
                values: new object[] { 4, null, new DateTime(2022, 6, 22, 14, 35, 58, 191, DateTimeKind.Local).AddTicks(8816), 2 });

            migrationBuilder.InsertData(
                table: "Logins",
                columns: new[] { "Id", "EmployeeId", "LoginDate", "PersonId" },
                values: new object[] { 5, null, new DateTime(2022, 5, 22, 14, 35, 58, 191, DateTimeKind.Local).AddTicks(8819), 2 });

            migrationBuilder.InsertData(
                table: "Logins",
                columns: new[] { "Id", "EmployeeId", "LoginDate", "PersonId" },
                values: new object[] { 6, null, new DateTime(2022, 6, 22, 14, 35, 58, 191, DateTimeKind.Local).AddTicks(8822), 3 });

            migrationBuilder.CreateIndex(
                name: "IX_Logins_EmployeeId",
                table: "Logins",
                column: "EmployeeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Logins");

            migrationBuilder.DropTable(
                name: "Employees");
        }
    }
}
