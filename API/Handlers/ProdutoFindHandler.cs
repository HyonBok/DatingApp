using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Commands;
using API.Interfaces;
using MediatR;

namespace API.Handlers
{
    public class ProdutoFindHandler : IRequestHandler<ProdutoFindByIdRequest, ProdutoFindByIdResponse>
    {
        private readonly IProdutoRepository _repository;
        
        public ProdutoFindHandler(IProdutoRepository repository)
        {
            _repository = repository;
            
        }

        public async Task<ProdutoFindByIdResponse> Handle(ProdutoFindByIdRequest request, CancellationToken token)
        {
            var p = await _repository.GetProdutoByIdAsync(request.Id);

            var result = new ProdutoFindByIdResponse
            {
                Id = p.Id,
                Nome = p.Nome,
                Preco = p.Preco,
                PrecoDesconto = p.Preco,
                Marca = p.Marca,
                UnidadeVenda = p.UnidadeVenda
            };
            return result;
        }
    }
}