using ApiCatalogo.Repository;

namespace ApiCatalogo.Services.Interfaces
{
    public interface IRepositoryServices
    {
        IProdutoRepository ProdutoRepository { get; }
        ICategoriaRepository CategoriaRepository { get; }
        void Commit();
    }
}
