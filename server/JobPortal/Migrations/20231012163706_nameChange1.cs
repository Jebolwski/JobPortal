using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JobPortal.Migrations
{
    public partial class nameChange1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_JubAdPhoto_JobAd_jobAdId",
                table: "JubAdPhoto");

            migrationBuilder.DropPrimaryKey(
                name: "PK_JubAdPhoto",
                table: "JubAdPhoto");

            migrationBuilder.RenameTable(
                name: "JubAdPhoto",
                newName: "JobAdPhoto");

            migrationBuilder.RenameIndex(
                name: "IX_JubAdPhoto_jobAdId",
                table: "JobAdPhoto",
                newName: "IX_JobAdPhoto_jobAdId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_JobAdPhoto",
                table: "JobAdPhoto",
                column: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_JobAdPhoto_JobAd_jobAdId",
                table: "JobAdPhoto",
                column: "jobAdId",
                principalTable: "JobAd",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_JobAdPhoto_JobAd_jobAdId",
                table: "JobAdPhoto");

            migrationBuilder.DropPrimaryKey(
                name: "PK_JobAdPhoto",
                table: "JobAdPhoto");

            migrationBuilder.RenameTable(
                name: "JobAdPhoto",
                newName: "JubAdPhoto");

            migrationBuilder.RenameIndex(
                name: "IX_JobAdPhoto_jobAdId",
                table: "JubAdPhoto",
                newName: "IX_JubAdPhoto_jobAdId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_JubAdPhoto",
                table: "JubAdPhoto",
                column: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_JubAdPhoto_JobAd_jobAdId",
                table: "JubAdPhoto",
                column: "jobAdId",
                principalTable: "JobAd",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
