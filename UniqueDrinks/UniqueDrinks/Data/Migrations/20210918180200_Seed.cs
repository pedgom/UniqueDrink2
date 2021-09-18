using Microsoft.EntityFrameworkCore.Migrations;

namespace UniqueDrinks.Data.Migrations
{
    public partial class Seed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c",
                column: "ConcurrencyStamp",
                value: "44d18b1f-54af-453c-b9dd-0a8976d470b4");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "g",
                column: "ConcurrencyStamp",
                value: "ab991d4d-0e6c-4c78-9419-a5a405876546");

            migrationBuilder.InsertData(
                table: "Bebidas",
                columns: new[] { "Id", "Categoria", "Descricao", "Imagem", "Nome", "Preco", "Stock" },
                values: new object[,]
                {
                    { 9, "Vinho", "MATEUS ROSÉ é um vinho leve, fresco, jovem e ligeiramente «pétillant»", "Vinho-Mateus-Rose.jpg", "Vinho Rose Mateus", 12.5f, "Sim" },
                    { 10, "Vinho do Porto", "É vinificado pelo método tradicional do vinho do Porto.", "ferreira_Porto.jpg", "Vinho do Porto Ferreira", 17.25f, "Sim" },
                    { 11, "Whiskey", "Grant’s é um whisky extraordinário e um dos mais complexos produzidos na Escócia.", "grants_whisky.jpg", "Grants Whisky", 27.99f, "Sim" },
                    { 12, "Cerveja", "O sabor autêntico.Super Bock Original é a única cerveja portuguesa com 37 medalhas de ouro consecutivas", "superBock.jpg", "Super Bock Pack15", 12.5f, "Sim" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Bebidas",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Bebidas",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Bebidas",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Bebidas",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c",
                column: "ConcurrencyStamp",
                value: "cb5d8dcd-9db2-4880-878d-cc047b860b53");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "g",
                column: "ConcurrencyStamp",
                value: "ba193b00-a729-442a-b0bd-e4672e83b262");
        }
    }
}
