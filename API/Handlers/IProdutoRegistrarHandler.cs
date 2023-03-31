using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Commands;

namespace API.Handlers
{
    public interface IProdutoRegistrarHandler
    {
        public Task<ProdutoResponse> Handle(ProdutoRequest p);
    }
}