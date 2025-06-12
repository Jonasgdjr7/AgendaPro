using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgendaPro.Aplicacao.Dtos
{
    public class SalaDto
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public int Capacidade { get; set; }
    }
}
