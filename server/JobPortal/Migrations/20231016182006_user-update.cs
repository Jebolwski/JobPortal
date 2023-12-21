using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JobPortal.Migrations
{
    public partial class userupdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "JobPortal");

            migrationBuilder.RenameTable(
                name: "User",
                newName: "User",
                newSchema: "JobPortal");

            migrationBuilder.RenameTable(
                name: "Role",
                newName: "Role",
                newSchema: "JobPortal");

            migrationBuilder.RenameTable(
                name: "JobAdPhoto",
                newName: "JobAdPhoto",
                newSchema: "JobPortal");

            migrationBuilder.RenameTable(
                name: "JobAd",
                newName: "JobAd",
                newSchema: "JobPortal");

            migrationBuilder.RenameTable(
                name: "AspNetUserTokens",
                newName: "AspNetUserTokens",
                newSchema: "JobPortal");

            migrationBuilder.RenameTable(
                name: "AspNetUsers",
                newName: "AspNetUsers",
                newSchema: "JobPortal");

            migrationBuilder.RenameTable(
                name: "AspNetUserRoles",
                newName: "AspNetUserRoles",
                newSchema: "JobPortal");

            migrationBuilder.RenameTable(
                name: "AspNetUserLogins",
                newName: "AspNetUserLogins",
                newSchema: "JobPortal");

            migrationBuilder.RenameTable(
                name: "AspNetUserClaims",
                newName: "AspNetUserClaims",
                newSchema: "JobPortal");

            migrationBuilder.RenameTable(
                name: "AspNetRoles",
                newName: "AspNetRoles",
                newSchema: "JobPortal");

            migrationBuilder.RenameTable(
                name: "AspNetRoleClaims",
                newName: "AspNetRoleClaims",
                newSchema: "JobPortal");

            migrationBuilder.RenameColumn(
                name: "username",
                schema: "JobPortal",
                table: "User",
                newName: "photoUrl");

            migrationBuilder.AddColumn<string>(
                name: "firstName",
                schema: "JobPortal",
                table: "User",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "lastName",
                schema: "JobPortal",
                table: "User",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "name",
                schema: "JobPortal",
                table: "User",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "firstName",
                schema: "JobPortal",
                table: "User");

            migrationBuilder.DropColumn(
                name: "lastName",
                schema: "JobPortal",
                table: "User");

            migrationBuilder.DropColumn(
                name: "name",
                schema: "JobPortal",
                table: "User");

            migrationBuilder.RenameTable(
                name: "User",
                schema: "JobPortal",
                newName: "User");

            migrationBuilder.RenameTable(
                name: "Role",
                schema: "JobPortal",
                newName: "Role");

            migrationBuilder.RenameTable(
                name: "JobAdPhoto",
                schema: "JobPortal",
                newName: "JobAdPhoto");

            migrationBuilder.RenameTable(
                name: "JobAd",
                schema: "JobPortal",
                newName: "JobAd");

            migrationBuilder.RenameTable(
                name: "AspNetUserTokens",
                schema: "JobPortal",
                newName: "AspNetUserTokens");

            migrationBuilder.RenameTable(
                name: "AspNetUsers",
                schema: "JobPortal",
                newName: "AspNetUsers");

            migrationBuilder.RenameTable(
                name: "AspNetUserRoles",
                schema: "JobPortal",
                newName: "AspNetUserRoles");

            migrationBuilder.RenameTable(
                name: "AspNetUserLogins",
                schema: "JobPortal",
                newName: "AspNetUserLogins");

            migrationBuilder.RenameTable(
                name: "AspNetUserClaims",
                schema: "JobPortal",
                newName: "AspNetUserClaims");

            migrationBuilder.RenameTable(
                name: "AspNetRoles",
                schema: "JobPortal",
                newName: "AspNetRoles");

            migrationBuilder.RenameTable(
                name: "AspNetRoleClaims",
                schema: "JobPortal",
                newName: "AspNetRoleClaims");

            migrationBuilder.RenameColumn(
                name: "photoUrl",
                table: "User",
                newName: "username");
        }
    }
}
