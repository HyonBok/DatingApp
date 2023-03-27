using API.Data;
using API.DTOs;
using API.Entities;
using API.Extensions;
using API.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    public class ProdutoController : BaseApiController
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        public ProdutoController(DataContext context, IMapper mapper)
        {
            // Recebe o contexto do banco de dados
            _context = context;
            _mapper = mapper;
        }

        [HttpGet("{nome}")] // Get: api/produto/listar/{nome}
        public async Task<ActionResult<ProdutoDto>> Produto(string nome)
        {
            var produtoDto = await _context.Produtos
                .Where(x => x.Nome.ToLower() == nome.ToLower())
                .ProjectTo<ProdutoDto>(_mapper.ConfigurationProvider)
                .SingleOrDefaultAsync();

            return produtoDto;
        }

        [HttpGet("listar")] // Get: api/produto/listar
        public async Task<ActionResult<IEnumerable<ProdutoDto>>> ListarProdutos()
        {
            var produtosDto = await _context.Produtos
                .ProjectTo<ProdutoDto>(_mapper.ConfigurationProvider)
                .ToListAsync();

            return Ok(produtosDto);
        }

        // [HttpGet("listarOfertas")] 
        // public async Task<ActionResult<IEnumerable<ProdutoDto>>> ListarProdutosOferta() {
        //     var produtos = await _context.Produtos.Where().ToListAsync();
        //     return Ok();
        // }

        [Authorize]
        [HttpPost("registrar")] // Post: api/produto/registrar
        public async Task<ActionResult<ProdutoDto>> RegistrarProduto(RegistrarProdutoDto registroProdutoDto)
        {
            var user = await _context.Users
                .Where(x => x.UserName == User.GetUsername())
                .FirstOrDefaultAsync();

            var produto = new Produto
            {
                Nome = registroProdutoDto.Nome,
                Preco = registroProdutoDto.Preco,
                Marca = registroProdutoDto.Marca,
                Sessao = registroProdutoDto.Sessao,
                UnidadeVenda = registroProdutoDto.UnidadeVenda,
                Descricao = registroProdutoDto.Descricao,
                AppUser = user
            };

            _context.Produtos.Add(produto);
            await _context.SaveChangesAsync();

            var produtoDto = new ProdutoDto
            {
                Nome = produto.Nome,
                Preco = produto.Preco,
                Marca = produto.Marca,
                Sessao = produto.Sessao,
                UnidadeVenda = produto.UnidadeVenda,
                Descricao = produto.Descricao
            };

            return produtoDto;
        }

        [Authorize]
        [HttpGet("listself/{nome}")]
        public async Task<ActionResult<IEnumerable<Produto>>> ListarProdutosUsuario(string nome)
        {
            var produtosDto = await _context.Produtos
                .Where(p => p.AppUser.UserName.ToLower() == nome.ToLower())
                .ProjectTo<ProdutoDto>(_mapper.ConfigurationProvider)
                .ToListAsync();

            return Ok(produtosDto);
        }

        [Authorize]
        [HttpDelete("deletar/{id}")]
        public async Task<ActionResult> DeletarProduto(int id)
        {
            var produto = await _context.Produtos.FindAsync(id);

            if (produto == null)
            {
                return NotFound("Produto não encontrado!"); // retorna código 404 caso não encontre o produto
            }

            _context.Produtos.Remove(produto);
            await _context.SaveChangesAsync(); // salva as alterações

            return NoContent();
        }
    }
}