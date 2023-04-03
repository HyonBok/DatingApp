using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;

namespace API.Commands
{
    public class ProdutoListSelfRequest : IRequest<List<ProdutoResponse>>
    {
        public string Nome { get; set; }
    }
}