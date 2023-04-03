using API.Commands;
using API.Interfaces;
using MediatR;

namespace API.Handlers
{
    public class ProdutoDeleteHandler : IRequestHandler<ProdutoDeleteRequest, ProdutoResponse>
    {
        private readonly IProdutoRepository _repository;

        public ProdutoDeleteHandler(IProdutoRepository repository)
        {
            _repository = repository;
        }

        public async Task<ProdutoResponse> Handle(ProdutoDeleteRequest request, CancellationToken token)
        {
            var produto = await _repository.GetProdutoByIdAsync(request.Id);

            if (produto == null) return null;

            _repository.DeleteProduto(produto);
            await _repository.SaveAllAsync(); // salva as alterações

            return null;
        }
    }
}