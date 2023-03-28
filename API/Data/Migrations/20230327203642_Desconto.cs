using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Data.Migrations
{
    /// <inheritdoc />
    public partial class Desconto : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EmPromocao",
                table: "Produtos");

            migrationBuilder.RenameColumn(
                name: "PrecoPromocao",
                table: "Produtos",
                newName: "Desconto");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Desconto",
                table: "Produtos",
                newName: "PrecoPromocao");

            migrationBuilder.AddColumn<bool>(
                name: "EmPromocao",
                table: "Produtos",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);
        }
    }
}
