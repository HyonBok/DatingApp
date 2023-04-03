using MediatR;

namespace API.Commands
{
    public class ProdutoAddFotoRequest : IRequest<ProdutoResponse>
    {

        public IFormFile file { get; set; }
        public int IdProduto { get; set; }
    }
}