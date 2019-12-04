using Microsoft.EntityFrameworkCore.Migrations;

namespace Repository.Migrations
{
    public partial class AlterandoEntidadesFracas : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Preco",
                table: "ItemPizzas");

            migrationBuilder.DropColumn(
                name: "Preco",
                table: "ItemBebidas");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "Preco",
                table: "ItemPizzas",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Preco",
                table: "ItemBebidas",
                nullable: false,
                defaultValue: 0.0);
        }
    }
}
