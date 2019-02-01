using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace ReceitasWebApi.Models
{
    public class Receita
    {
        [Key]
        public int ReceitaId { get; set; } 

        [Required(ErrorMessage ="Informe o nome da Receita.")]
        public string Nome { get; set; }

        [Required(ErrorMessage ="Informe a quantidade de porções.")]
        [Range(1, int.MaxValue, ErrorMessage = "Informe uma quantidade de porções maior ou igual a 1.")]
        public int Porcoes { get; set; }

        [Required(ErrorMessage ="Informe a quantidade de Calorias.")]
        [Range(1, int.MaxValue, ErrorMessage = "Informe uma quantidade de calorias maior ou igual a 1.")]
        public int Calorias { get; set; }

        [Required(ErrorMessage ="Informe o preparo da Receita.")]
        public string ModoPreparo { get; set; }

        [Required(ErrorMessage ="A receita precisa ter pelo menos um ingrediente.")]
        public virtual ICollection<Ingrediente> Ingredientes { get; set; }        

    }
}