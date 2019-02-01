using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace ReceitasWebApi.Models
{
    
    public class Ingrediente
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IngredienteId { get; set; }

        [Required]        
        public string Nome { get; set; }

        [Required]        
        public string Descricao { get; set; }

        public virtual ICollection<Receita> Receitas { get; set; }
    }
}