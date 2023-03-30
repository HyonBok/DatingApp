using API.DTOs;
using API.Entities;
using Microsoft.AspNetCore.Mvc;

namespace API.Interfaces
{
    public interface IProdutoRepository
    {
        void Update(Produto produto);
        Task<bool> SaveAllAsync();
        void AddProduto(Produto produto);
        void DeleteProduto(Produto produto);
        Task<IEnumerable<ProdutoDto>> GetProdutosAsync();
        Task<IEnumerable<ProdutoDto>> GetProdutosOfertaAsync();
        Task<Produto> GetProdutoByIdAsync(int id);
        Task<ProdutoDto> GetProdutoDtoByIdAsync(int id);
        Task<ProdutoDto> GetProdutoByUserAsync(string nome);
    }
}