using API.DTOs;
using API.Entities;
using API.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class ProdutoRepository : IProdutoRepository
    {
        private readonly DataContext _context;
        public ProdutoRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<Produto> GetProdutoByIdAsync(int id)
        {
            return await _context.Produtos
                .SingleOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Produto> GetProdutoByNomeAsync(string nome)
        {
            return await _context.Produtos
                .Include(p => p.Fotos)
                .SingleOrDefaultAsync(p => p.Nome == nome);
        }

        public async Task<IEnumerable<Produto>> GetProdutosAsync()
        {
            return await _context.Produtos
                .Include(p => p.Fotos)
                .ToListAsync();
        }

        public async Task<bool> SaveAllAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public void Update(Produto produto)
        {
            _context.Entry(produto).State = EntityState.Modified;
        }

    }
}