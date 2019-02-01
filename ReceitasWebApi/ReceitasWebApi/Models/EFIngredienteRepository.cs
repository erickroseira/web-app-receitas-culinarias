using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ReceitasWebApi.Models
{
    public class EFIngredienteRepository : IIngredienteRepository
    {
        //instancia objeto ReceitaWebApiContext
        private ReceitaWebApiContext receitas_db = new ReceitaWebApiContext();
        

        public IQueryable<Ingrediente> Ingredientes
        {
            get { return receitas_db.Ingredientes; }                                          
        }

        public void DisposeReceitasDbContext()
        {

          receitas_db.Dispose(); //dispose receitas database context
            
        }

    }
}