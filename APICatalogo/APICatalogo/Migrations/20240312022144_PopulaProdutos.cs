using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace APICatalogo.Migrations
{
    /// <inheritdoc />
    public partial class PopulaProdutos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder mb)
        {
            mb.Sql("INSERT INTO Produtos (Nome, Descricao, Preco, ImgUrl, Estoque, DataCadastro, CategoriaId) VALUES ('Hot Dog', 'Pão com salsicha e purê de batata', 12.00, 'hotdog.jpg', 15, now(), 2)");
            mb.Sql("INSERT INTO Produtos (Nome, Descricao, Preco, ImgUrl, Estoque, DataCadastro, CategoriaId) VALUES ('X-Salada', 'Pão com Hamburger 56g, alface, tomate, cebola e picles', 14.00, 'xsalada.jpg', 10, now(), 2)");
            mb.Sql("INSERT INTO Produtos (Nome, Descricao, Preco, ImgUrl, Estoque, DataCadastro, CategoriaId) VALUES ('X-Bacon', 'Pão com Hamburger 56g, tiras de bacon e alface', 15.00, 'xbacon.jpg', 7, now(), 2)");

            mb.Sql("INSERT INTO Produtos (Nome, Descricao, Preco, ImgUrl, Estoque, DataCadastro, CategoriaId) VALUES ('Coca-Cola zero', 'Lata de coca-cola zero acucar 350ml', 5.50, 'cocacola.jpg', 20, now(), 1)");
            mb.Sql("INSERT INTO Produtos (Nome, Descricao, Preco, ImgUrl, Estoque, DataCadastro, CategoriaId) VALUES ('Suco de laranja natural', 'Suco natural de laranja 300ml', 7.00, 'sucolaranja.jpg', 10, now(), 1)");

            mb.Sql("INSERT INTO Produtos (Nome, Descricao, Preco, ImgUrl, Estoque, DataCadastro, CategoriaId) VALUES ('Bolo de pote chocolate', 'Bolo de pote sabor chocolate 200g', 12.00, 'bolopote.jpg', 15, now(), 3)");
            mb.Sql("INSERT INTO Produtos (Nome, Descricao, Preco, ImgUrl, Estoque, DataCadastro, CategoriaId) VALUES ('Sundae de morango', 'Sundae sabor morango 200ml', 5.50, 'sundaemorango.jpg', 12, now(), 3)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder mb)
        {
            mb.Sql("DELETE FROM Categorias");
        }
    }
}
