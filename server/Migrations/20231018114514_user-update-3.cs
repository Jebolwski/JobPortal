using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JobPortal.Migrations
{
    public partial class userupdate3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<byte[]>(
                name: "passwordSalt",
                schema: "JobPortal",
                table: "User",
                type: "bytea",
                nullable: true,
                oldClrType: typeof(byte[]),
                oldType: "bytea");

            migrationBuilder.AlterColumn<byte[]>(
                name: "passwordHash",
                schema: "JobPortal",
                table: "User",
                type: "bytea",
                nullable: true,
                oldClrType: typeof(byte[]),
                oldType: "bytea");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<byte[]>(
                name: "passwordSalt",
                schema: "JobPortal",
                table: "User",
                type: "bytea",
                nullable: false,
                defaultValue: new byte[0],
                oldClrType: typeof(byte[]),
                oldType: "bytea",
                oldNullable: true);

            migrationBuilder.AlterColumn<byte[]>(
                name: "passwordHash",
                schema: "JobPortal",
                table: "User",
                type: "bytea",
                nullable: false,
                defaultValue: new byte[0],
                oldClrType: typeof(byte[]),
                oldType: "bytea",
                oldNullable: true);
        }
    }
}
