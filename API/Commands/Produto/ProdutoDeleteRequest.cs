using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;

namespace API.Commands
{
    public class ProdutoDeleteRequest : IRequest<ProdutoResponse>
    {
        public int Id { get; set; }
    }
}