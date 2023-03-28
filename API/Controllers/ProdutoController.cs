using API.Data;
using API.DTOs;
using API.Entities;
using API.Extensions;
using API.Interfaces;
using API.Services;
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
        private readonly IPhotoService _photoService;
        public ProdutoController(DataContext context, IMapper mapper,
            IPhotoService photoService)
        {
            _photoService = photoService;
            // Recebe o contexto do banco de dados
            _context = context;
            _mapper = mapper;
        }

        [HttpGet("{id}")] // Get: api/produto/{id}
        public async Task<ActionResult<ProdutoDto>> Produto(int id)
        {
            var produtoDto = await _context.Produtos
                .Where(x => x.Id == id)
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
                UnidadeVenda = registroProdutoDto.UnidadeVenda,
                AppUser = user
            };

            _context.Produtos.Add(produto);
            await _context.SaveChangesAsync();

            var produtoDto = new ProdutoDto
            {
                Id = produto.Id,
                Nome = produto.Nome,
                Preco = produto.Preco,
                Marca = produto.Marca,
                UnidadeVenda = produto.UnidadeVenda,
                Sessao = produto.Sessao
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

        [Authorize]
        [HttpPut("atualizar")]
        public async Task<ActionResult> AtualizarProduto(ProdutoDto produtoDto) {
            var produto = await _context.Produtos
                .Where(x => x.Id == produtoDto.Id)
                .SingleOrDefaultAsync();

            if(produto == null) return NotFound();

            _mapper.Map(produtoDto, produto);

            await _context.SaveChangesAsync();

            return NoContent();
        }

        [Authorize]
        [HttpPost("add-foto/{id}")]
        public async Task<ActionResult<PhotoDto>> AdicionarFoto(IFormFile file, int id)
        {
            var produto = await _context.Produtos
                .Where(x => x.Id == id)
                .SingleOrDefaultAsync();

            if(produto == null) return NotFound();

            var result = await _photoService.AddPhotoAsync(file);

            if(result.Error != null) return BadRequest(result.Error.Message);

            var foto = new Photo
            {
                Url = result.SecureUrl.AbsoluteUri,
                PublicId = result.PublicId,
            };

            produto.Fotos.Add(foto);

            await _context.SaveChangesAsync();

            return Ok();
        }

        // [Authorize]
        // [HttpDelete("delete-photo/{photoId}")]
        // public async Task<ActionResult> DeletePhoto(int photoId) 
        // {
        //     var user = await _userRepository .GetUserByUsernameAsync(User.GetUsername());

        //     var photo = user.Photos.FirstOrDefault(x => x.Id == photoId);

        //     if(photo == null) return NotFound();

        //     if(photo.IsMain) return BadRequest("Você não pode deletar sua foto principal!");

        //     if(photo.PublicId != null)
        //     {
        //         var result = await _photoService.DeletePhotoAsync(photo.PublicId);
        //         if (result.Error != null) return BadRequest(result.Error.Message);
        //     }

        //     user.Photos.Remove(photo);

        //     if(await _userRepository.SaveAllAsync()) return Ok();

        //     return BadRequest("Problema ao tentar deletar imagem!");
        // }
    }
}