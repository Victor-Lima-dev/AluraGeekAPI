using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AluraGeekAPI.Migrations
{
    /// <inheritdoc />
    public partial class carrinhoitemteste : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CarrinhoItens_Produtos_ProdutoId",
                table: "CarrinhoItens");

            migrationBuilder.AlterColumn<int>(
                name: "ProdutoId",
                table: "CarrinhoItens",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_CarrinhoItens_Produtos_ProdutoId",
                table: "CarrinhoItens",
                column: "ProdutoId",
                principalTable: "Produtos",
                principalColumn: "ProdutoId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CarrinhoItens_Produtos_ProdutoId",
                table: "CarrinhoItens");

            migrationBuilder.AlterColumn<int>(
                name: "ProdutoId",
                table: "CarrinhoItens",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_CarrinhoItens_Produtos_ProdutoId",
                table: "CarrinhoItens",
                column: "ProdutoId",
                principalTable: "Produtos",
                principalColumn: "ProdutoId");
        }
    }
}
