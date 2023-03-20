using API.Data;
using API.DTOs;
using API.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    public class ProdutoController : BaseApiController
    {
        private readonly DataContext _context;
        public ProdutoController(DataContext context)
        {
            // Recebe o contexto do banco de dados
            _context = context;
        }

        [HttpGet("{nome}")] // Get: api/produto/listar/{nome}
        public async Task<ActionResult<ProdutoDto>> Produto(string nome){
            var produto = await _context.Produtos
                .Where(x => x.Nome.ToLower() == nome.ToLower())
                .SingleOrDefaultAsync();

            var produtoDto = new ProdutoDto
            {
                Nome = produto.Nome,
                Preco = produto.Preco,
                Foto = produto.Foto,
                Marca = produto.Marca,
                Descricao = produto.Descricao,
                UnidadeVenda = produto.UnidadeVenda,
            };

            return produtoDto;
        }
             

        [HttpGet("listar")] // Get: api/produto/listar
        public async Task<ActionResult<IEnumerable<ProdutoDto>>> ListarProdutos(){
            var users = await _context.Produtos.ToListAsync();
            
            return Ok(users);
        }

        [Authorize]
        [HttpPost("registrar")] // Post: api/produto/registrar
        public async Task<ActionResult<ProdutoDto>> RegistrarProduto(RegistrarProdutoDto registroDto){
            var produto = new Produto
            {
                Nome = registroDto.Nome,
                Preco = registroDto.Preco,
                Foto = registroDto.Foto,
                Marca = registroDto.Marca,
                Descricao = registroDto.Descricao,
                UnidadeVenda = registroDto.UnidadeVenda,
            };

            _context.Produtos.Add(produto);
            await _context.SaveChangesAsync();

            var produtoDto = new ProdutoDto
            {
                Nome = produto.Nome,
                Preco = produto.Preco,
                Foto = produto.Foto,
                Marca = produto.Marca,
                Descricao = produto.Descricao,
                UnidadeVenda = produto.UnidadeVenda,
            };
            
            return produtoDto;
        }
    }
}