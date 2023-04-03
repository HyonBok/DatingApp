using API.Commands;
using API.DTOs;
using API.Entities;
using API.Interfaces;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class ProdutoController : BaseApiController
    {
        private readonly IProdutoRepository _produtoRepository;
        private readonly IPhotoService _photoService;
        private readonly IMediator _mediator;
        
        public ProdutoController(IProdutoRepository produtoRepository, IMediator mediator,
            IPhotoService photoService)
        {
            _mediator = mediator;
            _photoService = photoService;
            _produtoRepository = produtoRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Produto([FromQuery]ProdutoFindRequest command)
        {
            var response = await _mediator.Send(command);

            if(response == null) return NoContent();

            return Ok(response);
        }

        [HttpGet("listar")] // Get: api/produto/listar
        public async Task<IActionResult> ListarProdutos()
        {
            var response = await _mediator.Send(new ProdutoListRequest());

            return Ok(response);
        }

        [HttpGet("listar-ofertas")] 
        public async Task<IActionResult> ListarOfertas() {
            var response = await _mediator.Send(new ProdutoListOfertasRequest());

            return Ok(response);
        }

        [Authorize]
        [HttpPost("registrar")]
        public async Task<IActionResult> RegistrarProduto([FromBody]ProdutoRegistrarRequest command)
        {
            var response = await _mediator.Send(command);

            return Ok(response);
        }

        [Authorize]
        [HttpGet("listself/")]
        public async Task<IActionResult> ListarProdutosUsuario([FromQuery]ProdutoListSelfRequest command)
        {
            var response = await _mediator.Send(command);

            return Ok(response);
        }

        [Authorize]
        [HttpDelete("deletar/")]
        public IActionResult DeletarProduto([FromQuery]ProdutoDeleteRequest command)
        {
            _mediator.Send(command);

            return NoContent();
        }

        [Authorize]
        [HttpPut("atualizar")]
        public async Task<IActionResult> AtualizarProduto([FromBody]ProdutoAtualizarRequest command) {
            var response = await _mediator.Send(command);

            return Ok(response);
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

        [HttpPost("add-foto/{id}")]
        public async Task<IActionResult> AdicionarFotoM(IFormFile f, int id)
        {
            var response = await _mediator.Send(new ProdutoAddFotoRequest{
                file = f,
                IdProduto = id
            });

            return Ok(response);
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