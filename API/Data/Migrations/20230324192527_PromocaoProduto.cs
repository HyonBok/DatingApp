using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Data.Migrations
{
    /// <inheritdoc />
    public partial class PromocaoProduto : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Foto",
                table: "Produtos",
                newName: "FotoUrl");

            migrationBuilder.AddColumn<bool>(
                name: "EmPromocao",
                table: "Produtos",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "ProdutoId",
                table: "Photos",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Photos_ProdutoId",
                table: "Photos",
                column: "ProdutoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Photos_Produtos_ProdutoId",
                table: "Photos",
                column: "ProdutoId",
                principalTable: "Produtos",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Photos_Produtos_ProdutoId",
                table: "Photos");

            migrationBuilder.DropIndex(
                name: "IX_Photos_ProdutoId",
                table: "Photos");

            migrationBuilder.DropColumn(
                name: "EmPromocao",
                table: "Produtos");

            migrationBuilder.DropColumn(
                name: "ProdutoId",
                table: "Photos");

            migrationBuilder.RenameColumn(
                name: "FotoUrl",
                table: "Produtos",
                newName: "Foto");
        }
    }
}
