using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ReceitasWebApi.Models
{
    public class ReceitaDTO
    {

        public int ReceitaId { get; set; }

        public string Nome { get; set; }

        public int Porcoes { get; set; }

        public int Calorias { get; set; }

        public string ModoPreparo { get; set; }

        public List<IngredienteDTO> Ingredientes { get; set; }
    }
}