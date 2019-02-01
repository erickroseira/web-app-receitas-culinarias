using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ReceitasWebApi.Models
{
    public class EFReceitaRepository : IReceitaRepository
    {
        // contexto db
        private ReceitaWebApiContext receitas_db = new ReceitaWebApiContext();

        public IQueryable<Receita> Receitas
        {
            get { return receitas_db.Receitas; }
        }

        public void AdicionaReceita(Receita receita)
        {
            if (receita != null)
            {
                var igredientes_lista = new List<Ingrediente>();

                // para cada ingrediente retornado do form recupere-o do banco de dados e adicine-o na lista igredientes_lista
                foreach (Ingrediente igr in receita.Ingredientes)
                {
                    igredientes_lista.Add(receitas_db.Ingredientes.SingleOrDefault(i => i.IngredienteId == igr.IngredienteId));
                }

                // atualiza a lista de ingredientes do objeto Receita 
                receita.Ingredientes = igredientes_lista;

                //adiciona as receitas no banco de dados e salva as mudanças
                receitas_db.Receitas.Add(receita);
                receitas_db.SaveChanges();                

            }
        }

        public void DisposeReceitasDbContext()
        {

            receitas_db.Dispose(); //dispose receitas database context

        }
    }
}