using API.Commands;
using API.Interfaces;
using MediatR;

namespace API.Handlers
{
    public class ProdutoListOfertasHandler : IRequestHandler<ProdutoListOfertasRequest, List<ProdutoResponse>>
    {
        private readonly IProdutoRepository _repository;
        public ProdutoListOfertasHandler(IProdutoRepository repository)
        {
            _repository = repository;
        }
        public async Task<List<ProdutoResponse>> Handle(ProdutoListOfertasRequest request, CancellationToken cancellationToken)
        {
            List<ProdutoResponse> list = new List<ProdutoResponse>();

            foreach(var p in await _repository.GetProdutosOfertaAsync()){
                list.Add(new ProdutoResponse
                {
                    Id = p.Id,
                    Nome = p.Nome,
                    Marca = p.Marca,
                    Preco = p.Preco,
                    Desconto = p.Desconto,
                    PrecoDesconto = (p.Preco * ((100 - p.Desconto) / 100)).ToString("N2"),
                    Sessao = p.Sessao,
                    Descricao = p.Descricao,
                    UnidadeVenda = p.UnidadeVenda,
                    FotoUrl = p.FotoUrl,
                    Usuario = p.Usuario
                });
            }

            return list;
        }
    }
}