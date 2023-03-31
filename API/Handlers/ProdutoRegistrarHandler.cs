using API.Commands;
using API.Data;
using API.Entities;
using API.Interfaces;
using MediatR;

namespace API.Handlers
{
    public class ProdutoRegistrarHandler : IRequestHandler<ProdutoRequest, ProdutoResponse>
    {
        private readonly IProdutoRepository _repository;
        public ProdutoRegistrarHandler(IProdutoRepository repository)
        {
            _repository = repository;
        }

        public async Task<ProdutoResponse> Handle(ProdutoRequest request, CancellationToken token)
        {
            var produto = new Produto(request.Nome, request.Marca, request.Preco, request.UnidadeVenda, request.AppUser);

            _repository.AddProduto(produto);
            await _repository.SaveAllAsync();

            var result = new ProdutoResponse
            {
                Id = request.Id,
                Nome = request.Nome,
                Preco = request.Preco,
                PrecoDesconto = request.Preco,
                Marca = request.Marca,
                UnidadeVenda = request.UnidadeVenda
            };
            return result;
        }
    }
}