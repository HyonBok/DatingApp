using API.Commands;
using API.Interfaces;
using MediatR;

namespace API.Handlers
{
    public class ProdutoFindHandler : IRequestHandler<ProdutoFindRequest, ProdutoResponse>
    {
        private readonly IProdutoRepository _repository;

        public ProdutoFindHandler(IProdutoRepository repository)
        {
            _repository = repository;
        }

        public async Task<ProdutoResponse> Handle(ProdutoFindRequest request, CancellationToken token)
        {
            var p = await _repository.GetProdutoByIdAsync(request.Id);

            if (p == null) return null;

            var result = new ProdutoResponse
            {
                Id = p.Id,
                Nome = p.Nome,
                Marca = p.Marca,
                Preco = p.Preco,
                Desconto = p.Desconto,
                Sessao = p.Sessao,
                Descricao = p.Descricao,
                UnidadeVenda = p.UnidadeVenda,
                FotoUrl = p.FotoUrl,
                Usuario = p.AppUser.UserName
            };
            return result;
        }
        
    }
}