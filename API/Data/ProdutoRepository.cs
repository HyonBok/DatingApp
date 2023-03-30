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
        private readonly IMapper _mapper;
        public ProdutoRepository(DataContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
        }

        public void AddProduto(Produto produto)
        {
            _context.Produtos.Add(produto);
        }

        public async Task<Produto> GetProdutoByIdAsync(int id)
        {
            return await _context.Produtos
                .SingleOrDefaultAsync(x => x.Id == id);
        }

        public async Task<ProdutoDto> GetProdutoDtoByIdAsync(int id)
        {
            return await _context.Produtos
                .ProjectTo<ProdutoDto>(_mapper.ConfigurationProvider)
                .SingleOrDefaultAsync(x => x.Id == id);
        }

        public async Task<ProdutoDto> GetProdutoByUserAsync(string nome)
        {
            return await _context.Produtos
                .Include(p => p.Fotos)
                .ProjectTo<ProdutoDto>(_mapper.ConfigurationProvider)
                .SingleOrDefaultAsync(p => p.Usuario == nome);
        }

        public async Task<IEnumerable<ProdutoDto>> GetProdutosAsync()
        {
            return await _context.Produtos
                .Include(p => p.Fotos)
                .ProjectTo<ProdutoDto>(_mapper.ConfigurationProvider)
                .ToListAsync();
        }

        public async Task<IEnumerable<ProdutoDto>> GetProdutosOfertaAsync()
        {
            return await _context.Produtos
                .Where(p => p.Desconto != 0)
                .ProjectTo<ProdutoDto>(_mapper.ConfigurationProvider)
                .ToListAsync();
        }

        public void DeleteProduto(Produto produto)
        {
            _context.Produtos.Remove(produto);
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