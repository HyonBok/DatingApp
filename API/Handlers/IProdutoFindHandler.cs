using API.Commands;

namespace API.Handlers
{
    public interface IProdutoFindHandler
    {
        Task<ProdutoFindByIdResponse> Handle(ProdutoFindByIdRequest command);
    }
}