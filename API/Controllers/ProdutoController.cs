using API.Commands;
using API.Data;
using API.DTOs;
using API.Entities;
using API.Extensions;
using API.Handlers;
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
        private readonly IProdutoRepository _produtoRepository;
        private readonly IMapper _mapper;
        private readonly IPhotoService _photoService;
        private readonly IUserRepository _userRepository;
        public ProdutoController(IProdutoRepository produtoRepository, IMapper mapper,
            IPhotoService photoService, IUserRepository userRepository)
        {
            _userRepository = userRepository;
            _photoService = photoService;
            // Recebe o contexto do banco de dados
            _produtoRepository = produtoRepository;
            _mapper = mapper;
        }

        [HttpGet("{id}")] // Get: api/produto/{id}
        public async Task<ActionResult<ProdutoDto>> Produto(int id)
        {
            var produtoDto = await _produtoRepository.GetProdutoDtoByIdAsync(id);

            return produtoDto;
        }

        [HttpGet("")]
        public IActionResult ProdutoM(
            [FromServices]IProdutoFindHandler handler,
            [FromQuery]ProdutoFindByIdRequest command 
        )
        {
            var response = handler.Handle(command);
            return Ok(response);
        }

        [HttpGet("listar")] // Get: api/produto/listar
        public async Task<ActionResult<IEnumerable<ProdutoDto>>> ListarProdutos()
        {
            var produtosDto = await _produtoRepository.GetProdutosAsync();

            return Ok(produtosDto);
        }

        [HttpGet("listar-ofertas")] 
        public async Task<ActionResult<IEnumerable<ProdutoDto>>> ListarProdutosOferta() {
            var produtosDto = await _produtoRepository.GetProdutosOfertaAsync();

            return Ok(produtosDto);
        }

        [Authorize]
        [HttpPost("registrar")] // Post: api/produto/registrar
        public async Task<ActionResult<ProdutoDto>> RegistrarProduto(RegistrarProdutoDto rPDto)
        {
            var user = await _userRepository.GetUserByUsernameAsync(User.GetUsername());

            var produto = new Produto(rPDto.Nome, rPDto.Marca, rPDto.Preco, rPDto.UnidadeVenda, user);

            

            var produtoDto = new ProdutoDto
            {
                Id = produto.Id,
                Nome = produto.Nome,
                Preco = produto.Preco,
                PrecoDesconto = produto.Preco,
                Marca = produto.Marca,
                UnidadeVenda = produto.UnidadeVenda,
                Sessao = produto.Sessao
            };

            return produtoDto;
        }

        // Mediator
        [HttpPost("registrarM")]
        public IActionResult RegistrarProdutoM(
            [FromServices]IProdutoRegistrarHandler handler,
            [FromBody]ProdutoRequest command
            )
        {
            var response = handler.Handle(command);
            return Ok(response);
        }

        [Authorize]
        [HttpGet("listself/{nome}")]
        public async Task<ActionResult<IEnumerable<Produto>>> ListarProdutosUsuario(string nome)
        {
            var produtosDto = await _produtoRepository.GetProdutoByUserAsync(nome);

            return Ok(produtosDto);
        }

        [Authorize]
        [HttpDelete("deletar/{id}")]
        public async Task<ActionResult> DeletarProduto(int id)
        {
            var produto = await _produtoRepository.GetProdutoByIdAsync(id);

            if (produto == null)
            {
                return NotFound("Produto não encontrado!"); // retorna código 404 caso não encontre o produto
            }

            _produtoRepository.DeleteProduto(produto);
            await _produtoRepository.SaveAllAsync(); // salva as alterações

            return NoContent();
        }

        [Authorize]
        [HttpPut("atualizar")]
        public async Task<ActionResult> AtualizarProduto(ProdutoDto produtoDto) {
            var produto = await _produtoRepository.GetProdutoByIdAsync(produtoDto.Id);

            if(produto == null) return NotFound();

            _mapper.Map(produtoDto, produto);
            await _produtoRepository.SaveAllAsync();

            return NoContent();
        }

        [Authorize]
        [HttpPost("add-foto/{id}")]
        public async Task<ActionResult<FotoDto>> AdicionarFoto(IFormFile file, int id)
        {
            var produto = await _produtoRepository.GetProdutoByIdAsync(id);

            if(produto == null) return NotFound();

            var result = await _photoService.AddPhotoAsync(file);

            if(result.Error != null) return BadRequest(result.Error.Message);

            var foto = new Foto
            {
                Url = result.SecureUrl.AbsoluteUri,
                PublicId = result.PublicId,
            };

            if(produto.Fotos.Count == 0) foto.IsMain = true;

            produto.Fotos.Add(foto);
            await _produtoRepository.SaveAllAsync();

            return Ok();
        }

        [Authorize]
        [HttpDelete("deletar-foto/{id}/{fotoId}")]
        public async Task<ActionResult> DeletarFoto(int id, int fotoId) 
        {
            var produto = await _produtoRepository.GetProdutoByIdAsync(id);

            var foto = produto.Fotos.FirstOrDefault(x => x.Id == fotoId);

            if(foto == null) return NotFound();

            if(foto.PublicId != null)
            {
                var result = await _photoService.DeletePhotoAsync(foto.PublicId);
                if (result.Error != null) return BadRequest(result.Error.Message);
            }

            produto.Fotos.Remove(foto);

            await _produtoRepository.SaveAllAsync();

            return Ok();
        }

        [HttpPut("set-foto/{id}/{fotoId}")]
        public async Task<ActionResult> SetMainPhoto(int id, int fotoId)
        {
            var produto = await _produtoRepository.GetProdutoByIdAsync(id);

            if(produto == null) return NotFound();

            var foto = produto.Fotos.FirstOrDefault(x => x.Id == fotoId);

            if(foto == null) return NotFound();

            if(foto.IsMain) return BadRequest("Esta já é sua foto principal!");

            var currentMain = produto.Fotos.FirstOrDefault(x => x.IsMain);
            if(currentMain != null) currentMain.IsMain = false;
            foto.IsMain = true;

            await _produtoRepository.SaveAllAsync();

            return NoContent();
        }
    }
}