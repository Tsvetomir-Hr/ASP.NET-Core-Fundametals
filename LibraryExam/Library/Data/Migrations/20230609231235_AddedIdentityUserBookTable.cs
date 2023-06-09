using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Library.Data.Migrations
{
    public partial class AddedIdentityUserBookTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_IdentityUserBook_AspNetUsers_CollectorId",
                table: "IdentityUserBook");

            migrationBuilder.DropForeignKey(
                name: "FK_IdentityUserBook_Books_BookId",
                table: "IdentityUserBook");

            migrationBuilder.DropPrimaryKey(
                name: "PK_IdentityUserBook",
                table: "IdentityUserBook");

            migrationBuilder.RenameTable(
                name: "IdentityUserBook",
                newName: "IdentityUsersBooks");

            migrationBuilder.RenameIndex(
                name: "IX_IdentityUserBook_BookId",
                table: "IdentityUsersBooks",
                newName: "IX_IdentityUsersBooks_BookId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_IdentityUsersBooks",
                table: "IdentityUsersBooks",
                columns: new[] { "CollectorId", "BookId" });

            migrationBuilder.AddForeignKey(
                name: "FK_IdentityUsersBooks_AspNetUsers_CollectorId",
                table: "IdentityUsersBooks",
                column: "CollectorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_IdentityUsersBooks_Books_BookId",
                table: "IdentityUsersBooks",
                column: "BookId",
                principalTable: "Books",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_IdentityUsersBooks_AspNetUsers_CollectorId",
                table: "IdentityUsersBooks");

            migrationBuilder.DropForeignKey(
                name: "FK_IdentityUsersBooks_Books_BookId",
                table: "IdentityUsersBooks");

            migrationBuilder.DropPrimaryKey(
                name: "PK_IdentityUsersBooks",
                table: "IdentityUsersBooks");

            migrationBuilder.RenameTable(
                name: "IdentityUsersBooks",
                newName: "IdentityUserBook");

            migrationBuilder.RenameIndex(
                name: "IX_IdentityUsersBooks_BookId",
                table: "IdentityUserBook",
                newName: "IX_IdentityUserBook_BookId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_IdentityUserBook",
                table: "IdentityUserBook",
                columns: new[] { "CollectorId", "BookId" });

            migrationBuilder.AddForeignKey(
                name: "FK_IdentityUserBook_AspNetUsers_CollectorId",
                table: "IdentityUserBook",
                column: "CollectorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_IdentityUserBook_Books_BookId",
                table: "IdentityUserBook",
                column: "BookId",
                principalTable: "Books",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
