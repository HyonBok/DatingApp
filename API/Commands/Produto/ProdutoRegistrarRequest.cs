using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Entities;
using MediatR;

namespace API.Commands
{
    public class ProdutoRegistrarRequest : IRequest<ProdutoResponse>
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Marca { get; set; }
        public float Preco { get; set; }
        public string UnidadeVenda { get; set; }
        public AppUser AppUser { get; set; }
    }
}