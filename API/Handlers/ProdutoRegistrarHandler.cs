using API.Commands;
using API.Data;
using API.Entities;
using API.Interfaces;
using MediatR;

namespace API.Handlers
{
    public class ProdutoRegistrarHandler : IRequestHandler<ProdutoRegistrarRequest, ProdutoResponse>
    {
        private readonly IProdutoRepository _repository;
        private readonly IUserRepository _userRepository;
        public ProdutoRegistrarHandler(IProdutoRepository repository, IUserRepository userRepository)
        {
            _userRepository = userRepository;
            _repository = repository;
        }

        public async Task<ProdutoResponse> Handle(ProdutoRegistrarRequest request, CancellationToken token)
        {
            var appUser = await _userRepository.GetUserByUsernameAsync(request.Usuario);

            var produto = new Produto{
                Nome = request.Nome,
                Marca = request.Marca, 
                Preco = request.Preco, 
                UnidadeVenda = request.UnidadeVenda, 
                AppUser = appUser
            };

            _repository.AddProduto(produto);
            await _repository.SaveAllAsync();

            var result = new ProdutoResponse
            {
                Id = produto.Id,
                Nome = produto.Nome,
                Preco = produto.Preco,
                PrecoDesconto = produto.Preco.ToString(),
                Marca = produto.Marca,
                UnidadeVenda = produto.UnidadeVenda
            };
            return result;
        }
    }
}