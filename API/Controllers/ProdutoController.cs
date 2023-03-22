using API.Data;
using API.DTOs;
using API.Entities;
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
        public async Task<ActionResult<ProdutoDto>> RegistrarProduto(RegistrarProdutoDto registroProdutoDto){
            var produto = new Produto
            {
                Nome = registroProdutoDto.Nome,
                Preco = registroProdutoDto.Preco,
                Foto = registroProdutoDto.Foto,
                Marca = registroProdutoDto.Marca,
                UnidadeVenda = registroProdutoDto.UnidadeVenda,
            };

            var user = await _context.Users
                .SingleOrDefaultAsync(users => users.UserName == registroProdutoDto.UserName);
            user.Produtos.Add(produto);
            
            _context.Produtos.Add(produto);
            await _context.SaveChangesAsync();

            var produtoDto = new ProdutoDto
            {
                Nome = produto.Nome,
                Preco = produto.Preco,
                Foto = produto.Foto,
                Marca = produto.Marca,
                UnidadeVenda = produto.UnidadeVenda,
            };
            
            return produtoDto;
        }

        [Authorize]
        [HttpGet("listself/{nome}")]
        public async Task<ActionResult<IEnumerable<Produto>>> ListarProdutosUsuario(string nome){
            var user = await _context.Users
                .Include(p => p.Produtos)
                .SingleOrDefaultAsync(x => x.UserName.ToLower() == nome.ToLower());

            List<ProdutoDto> produtosDto = new List<ProdutoDto>();
            for(int i = 0; i < user.Produtos.Count; i++){
                var prodDto = new ProdutoDto{
                    Nome = user.Produtos[i].Nome,
                    Preco = user.Produtos[i].Preco,
                    Foto = user.Produtos[i].Foto,
                    Marca = user.Produtos[i].Marca,
                    UnidadeVenda = user.Produtos[i].UnidadeVenda,
                };
                produtosDto.Add(prodDto);
            }

            return Ok(produtosDto);
        }
    }
}