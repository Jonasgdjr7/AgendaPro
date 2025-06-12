using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgendaPro.Aplicacao.Dtos
{
    public class ReservaDto
    {
        public Guid Id { get; set; }
        public string NomeSala { get; set; }
        public string NomeUsuario { get; set; }
        public DateTime DataHoraInicio { get; set; }
        public DateTime DataHoraFim { get; set; }
        public string Status { get; set; }
    }
}
