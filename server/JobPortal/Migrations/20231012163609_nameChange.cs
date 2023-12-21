using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JobPortal.Migrations
{
    public partial class nameChange : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductCategory_Address_jobAdId",
                table: "ProductCategory");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductCategory",
                table: "ProductCategory");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Address",
                table: "Address");

            migrationBuilder.RenameTable(
                name: "ProductCategory",
                newName: "JubAdPhoto");

            migrationBuilder.RenameTable(
                name: "Address",
                newName: "JobAd");

            migrationBuilder.RenameIndex(
                name: "IX_ProductCategory_jobAdId",
                table: "JubAdPhoto",
                newName: "IX_JubAdPhoto_jobAdId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_JubAdPhoto",
                table: "JubAdPhoto",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_JobAd",
                table: "JobAd",
                column: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_JubAdPhoto_JobAd_jobAdId",
                table: "JubAdPhoto",
                column: "jobAdId",
                principalTable: "JobAd",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_JubAdPhoto_JobAd_jobAdId",
                table: "JubAdPhoto");

            migrationBuilder.DropPrimaryKey(
                name: "PK_JubAdPhoto",
                table: "JubAdPhoto");

            migrationBuilder.DropPrimaryKey(
                name: "PK_JobAd",
                table: "JobAd");

            migrationBuilder.RenameTable(
                name: "JubAdPhoto",
                newName: "ProductCategory");

            migrationBuilder.RenameTable(
                name: "JobAd",
                newName: "Address");

            migrationBuilder.RenameIndex(
                name: "IX_JubAdPhoto_jobAdId",
                table: "ProductCategory",
                newName: "IX_ProductCategory_jobAdId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductCategory",
                table: "ProductCategory",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Address",
                table: "Address",
                column: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductCategory_Address_jobAdId",
                table: "ProductCategory",
                column: "jobAdId",
                principalTable: "Address",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
