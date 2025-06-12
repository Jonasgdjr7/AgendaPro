using AgendaPro.Dominio.Enums;

namespace AgendaPro.API.Dtos
{
    public class ReservaDto
    {
        public Guid Id { get; set; }
        public string NomeSala { get; set; }
        public string NomeUsuario { get; set; }
        public DateTime DataHoraInicio { get; set; }
        public DateTime DataHoraFim { get; set; }
        public string Status { get; set; } // Usamos string para ser mais amigável na API
    }
}
