using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Commands
{
    public class ProdutoFotoResponse
    {
        public int Id { get; set; }
        public string Url { get; set; }
        public bool IsMain { get; set; }
    }
}