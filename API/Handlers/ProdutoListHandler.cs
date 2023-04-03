using API.Commands;
using API.Interfaces;
using MediatR;

namespace API.Handlers
{
    public class ProdutoListHandler : IRequestHandler<ProdutoListRequest, List<ProdutoResponse>>
    {
        private readonly IProdutoRepository _repository;
        public ProdutoListHandler(IProdutoRepository repository)
        {
            _repository = repository;
        }
        public async Task<List<ProdutoResponse>> Handle(ProdutoListRequest request, CancellationToken cancellationToken)
        {
            List<ProdutoResponse> list = new List<ProdutoResponse>();

            foreach(var p in await _repository.GetProdutosAsync()){
                list.Add(new ProdutoResponse
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
                    Usuario = p.Usuario
                });
            }

            return list;
        }
    }
}