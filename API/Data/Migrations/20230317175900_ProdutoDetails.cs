using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Data.Migrations
{
    /// <inheritdoc />
    public partial class ProdutoDetails : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AppUserId",
                table: "Produtos",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Descricao",
                table: "Produtos",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Marca",
                table: "Produtos",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "PrecoPromocao",
                table: "Produtos",
                type: "REAL",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<string>(
                name: "UnidadeVenda",
                table: "Produtos",
                type: "TEXT",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Produtos_AppUserId",
                table: "Produtos",
                column: "AppUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Produtos_Users_AppUserId",
                table: "Produtos",
                column: "AppUserId",
                principalTable: "Users",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Produtos_Users_AppUserId",
                table: "Produtos");

            migrationBuilder.DropIndex(
                name: "IX_Produtos_AppUserId",
                table: "Produtos");

            migrationBuilder.DropColumn(
                name: "AppUserId",
                table: "Produtos");

            migrationBuilder.DropColumn(
                name: "Descricao",
                table: "Produtos");

            migrationBuilder.DropColumn(
                name: "Marca",
                table: "Produtos");

            migrationBuilder.DropColumn(
                name: "PrecoPromocao",
                table: "Produtos");

            migrationBuilder.DropColumn(
                name: "UnidadeVenda",
                table: "Produtos");
        }
    }
}
