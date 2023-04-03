using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Commands;
using API.Interfaces;
using AutoMapper;
using MediatR;

namespace API.Handlers
{
    public class ProdutoAtualizarHandler : IRequestHandler<ProdutoAtualizarRequest, ProdutoResponse>
    {
        private readonly IProdutoRepository _repository;
        private readonly IMapper _mapper;
        public ProdutoAtualizarHandler(IProdutoRepository repository, IMapper mapper)
        {
            _mapper = mapper;
            _repository = repository;
        }
        public async Task<ProdutoResponse> Handle(ProdutoAtualizarRequest request, CancellationToken token)
        {
            var produto = await _repository.GetProdutoByIdAsync(request.Id);

            if(produto == null) return null;

            _mapper.Map(request, produto);
            await _repository.SaveAllAsync();

            return null;
        }
    }
}