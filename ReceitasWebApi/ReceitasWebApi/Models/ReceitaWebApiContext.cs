using System.Data.Entity;

namespace ReceitasWebApi.Models
{
    public class ReceitaWebApiContext : DbContext
    {
        public ReceitaWebApiContext() : base("ReceitaDB") {

            // "inicia" o database com seeds ou seja com dados pré-definidos
            Database.SetInitializer(new ReceitasDBInitializer());
        }
         
        public DbSet<Receita> Receitas { get; set; }

        public DbSet<Ingrediente> Ingredientes { get; set; }
    }
}