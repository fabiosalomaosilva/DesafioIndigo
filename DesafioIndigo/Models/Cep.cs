using System;

namespace DesafioIndigo.Models
{
    public class Consulta
    {
        public int Id { get; set; }
        public string Logradouro { get; set; }
        public string Bairro{ get; set; }
        public string Estado { get; set; }
        public string Municipio { get; set; }
        public string NumeropCep { get; set; }
        public DateTime DataPesquisa { get; set; }
        public string Erro { get; set; }
    }
}
