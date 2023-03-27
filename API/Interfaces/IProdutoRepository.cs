using API.DTOs;
using API.Entities;
using Microsoft.AspNetCore.Mvc;

namespace API.Interfaces
{
    public interface IProdutoRepository
    {
        void Update(Produto produto);
        Task<bool> SaveAllAsync();
        Task<IEnumerable<Produto>> GetProdutosAsync();
        Task<Produto> GetProdutoByIdAsync(int id);
        Task<Produto> GetProdutoByNomeAsync(string nome);
    }
}