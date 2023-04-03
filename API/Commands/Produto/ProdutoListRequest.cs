using MediatR;

namespace API.Commands
{
    public class ProdutoListRequest : IRequest<List<ProdutoResponse>>
    {
        
    }
}