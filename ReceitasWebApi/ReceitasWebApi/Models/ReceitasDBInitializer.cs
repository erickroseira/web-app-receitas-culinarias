using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ReceitasWebApi.Models
{
    public class ReceitasDBInitializer : DropCreateDatabaseAlways<ReceitaWebApiContext>
    {

        protected override void Seed(ReceitaWebApiContext receita_context)
        {

            IList<Ingrediente> ingredientes_base = new List<Ingrediente>();

            //inserção de 20 (vinte) ingredientes pré-definidos no database no startup da aplicação

            ingredientes_base.Add(new Ingrediente() { Nome = "Sal", Descricao ="Ingrediente utilizado para dar ou realçar os sabores do alimento." });
            ingredientes_base.Add(new Ingrediente() { Nome = "Açúcar", Descricao = "Ingrediente utilizado para adoçicar o alimento/preparo." });
            ingredientes_base.Add(new Ingrediente() { Nome = "Farinha de Trigo", Descricao = "Ingrediente utilizado para preparo de bolos, massas, tortas, pão, etc." });
            ingredientes_base.Add(new Ingrediente() { Nome = "Pimenta do Reino", Descricao = "Especiaria/Tempero utilizado para saborizar alimentos." });
            ingredientes_base.Add(new Ingrediente() { Nome = "Leite Bovino", Descricao = "Ingrediente de origem animal utilizado no preparo de queijos, bolos, etc." });
            ingredientes_base.Add(new Ingrediente() { Nome = "Ovo", Descricao = "Ingrediente de origem animal utilizado em diversas receitas." });
            ingredientes_base.Add(new Ingrediente() { Nome = "Cominho", Descricao = "Especiaria utilizada para saborizar alimentos." });
            ingredientes_base.Add(new Ingrediente() { Nome = "Peito de Frango", Descricao = "Ingrediente de origem animal utilizada como proteína principal." });
            ingredientes_base.Add(new Ingrediente() { Nome = "Feijão", Descricao = "Leguminosa bastante utilizada no almoço." });
            ingredientes_base.Add(new Ingrediente() { Nome = "Arroz", Descricao = "Ingrediente muito comum utilizado no preparo de refeições." });
            ingredientes_base.Add(new Ingrediente() { Nome = "Cebola", Descricao = "Ingrediente utilizado como tempero em uma variedade enorme de pratos e refeições." });
            ingredientes_base.Add(new Ingrediente() { Nome = "Óleo de Girassol", Descricao = "Ingrediente utilizado para fritar alimentos." });
            ingredientes_base.Add(new Ingrediente() { Nome = "Alho", Descricao = "Ingrediente utilizado para saborizar alimentos." });
            ingredientes_base.Add(new Ingrediente() { Nome = "Creme de Leite", Descricao = "Ingrediente utilizado para realização de molhos." });
            ingredientes_base.Add(new Ingrediente() { Nome = "Cúrcuma", Descricao = "Especiaria utilizada para saborizar alimentos." });
            ingredientes_base.Add(new Ingrediente() { Nome = "Fermento em Pó", Descricao = "Ingrediente comumente utilizado na preparação de bolos e pães." });
            ingredientes_base.Add(new Ingrediente() { Nome = "Margarina", Descricao = "Ingrediente utilizado em bolos, pães e frituras." });
            ingredientes_base.Add(new Ingrediente() { Nome = "Camarão", Descricao = "Ingrediente de origem animal utilizado como proteína." });
            ingredientes_base.Add(new Ingrediente() { Nome = "Barra de Chocolate", Descricao = "Ingrediente utilizado para preparar sobremesas." });
            ingredientes_base.Add(new Ingrediente() { Nome = "Leite Condensado", Descricao = "Ingrediente utilizada em sobremesas." });

            
            // insere os ingredientes acima pré-definidos na base de dados

            receita_context.Ingredientes.AddRange(ingredientes_base);
            base.Seed(receita_context);
        }
    }
}