using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace APICatalogo.Migrations
{
    /// <inheritdoc />
    public partial class PopulaCategorias : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder mb)
        {
            mb.Sql("INSERT INTO Categorias (Nome, ImgUrl) VALUES ('Bebidas', 'bebidas.jpg')");
            mb.Sql("INSERT INTO Categorias (Nome, ImgUrl) VALUES ('Lanches', 'lanches.jpg')");
            mb.Sql("INSERT INTO Categorias (Nome, ImgUrl) VALUES ('Sobremesas', 'sobremesas.jpg')");

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder mb)
        {
            mb.Sql("DELETE FROM Categorias");
        }
    }
}
