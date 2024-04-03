using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace homework3_26.Data.Migrations
{
    /// <inheritdoc />
    public partial class updatename : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_QuestionTags_Questions_QuestionId",
                table: "QuestionTags");

            migrationBuilder.DropForeignKey(
                name: "FK_QuestionTags_Tags_TagId",
                table: "QuestionTags");

            migrationBuilder.DropPrimaryKey(
                name: "PK_QuestionTags",
                table: "QuestionTags");

            migrationBuilder.RenameTable(
                name: "QuestionTags",
                newName: "QuestionsTags");

            migrationBuilder.RenameIndex(
                name: "IX_QuestionTags_TagId",
                table: "QuestionsTags",
                newName: "IX_QuestionsTags_TagId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_QuestionsTags",
                table: "QuestionsTags",
                columns: new[] { "QuestionId", "TagId" });

            migrationBuilder.AddForeignKey(
                name: "FK_QuestionsTags_Questions_QuestionId",
                table: "QuestionsTags",
                column: "QuestionId",
                principalTable: "Questions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_QuestionsTags_Tags_TagId",
                table: "QuestionsTags",
                column: "TagId",
                principalTable: "Tags",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_QuestionsTags_Questions_QuestionId",
                table: "QuestionsTags");

            migrationBuilder.DropForeignKey(
                name: "FK_QuestionsTags_Tags_TagId",
                table: "QuestionsTags");

            migrationBuilder.DropPrimaryKey(
                name: "PK_QuestionsTags",
                table: "QuestionsTags");

            migrationBuilder.RenameTable(
                name: "QuestionsTags",
                newName: "QuestionTags");

            migrationBuilder.RenameIndex(
                name: "IX_QuestionsTags_TagId",
                table: "QuestionTags",
                newName: "IX_QuestionTags_TagId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_QuestionTags",
                table: "QuestionTags",
                columns: new[] { "QuestionId", "TagId" });

            migrationBuilder.AddForeignKey(
                name: "FK_QuestionTags_Questions_QuestionId",
                table: "QuestionTags",
                column: "QuestionId",
                principalTable: "Questions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_QuestionTags_Tags_TagId",
                table: "QuestionTags",
                column: "TagId",
                principalTable: "Tags",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
