using System;
using System.ComponentModel.DataAnnotations;

namespace DesafioIndigo.Models
{
    public class Consulta
    {
        public int Id { get; set; }
        public string Logradouro { get; set; }
        public string Bairro{ get; set; }
        public string Estado { get; set; }
  
        [Display(Name = "Município")]
        public string Municipio { get; set; }
     
        [Display(Name = "CEP")]
        public string NumeroCep { get; set; }
      
        [Display(Name = "Data da pesquisa")]
        public DateTime DataPesquisa { get; set; }
        public string Erro { get; set; }
    }
}
