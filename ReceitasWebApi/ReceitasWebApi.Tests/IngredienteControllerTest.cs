using ReceitasWebApi.Controllers;
using NUnit.Framework;
using Moq;
using ReceitasWebApi.Models;
using System.Linq;
using System.Collections.Generic;
using Newtonsoft.Json;

//using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ReceitasWebApi.Tests
{
    [TestFixture]
    public class IngredienteControllerTest
    {
        [Test]
        public void teste_retornarIngredientes()
        {
            Mock<IIngredienteRepository> ingredienteMock = new Mock<IIngredienteRepository>();

            // criando um mock para produzir uma lista fake de ingredientes (arrange)
            ingredienteMock.Setup(i => i.Ingredientes).Returns(new List<Ingrediente>()
                {
                    new Ingrediente { IngredienteId=1, Nome = "Leite Bovino_Teste", Descricao = "Ingrediente de origem animal utilizado no preparo de queijos, bolos, etc." },
                    new Ingrediente { IngredienteId=2, Nome = "Cominho_Teste", Descricao = "Especiaria utilizada para saborizar alimentos." },
                    new Ingrediente { IngredienteId=3, Nome = "Peito de Frango_Teste", Descricao = "Ingrediente de origem animal utilizada como proteína principal." },
                    new Ingrediente { IngredienteId=4, Nome = "Feijão_Teste", Descricao = "Leguminosa bastante utilizada no almoço." },
                    new Ingrediente { IngredienteId=5, Nome = "Arroz_Teste", Descricao = "Ingrediente muito comum utilizado no preparo de refeições." },
                    new Ingrediente { IngredienteId=6, Nome = "Cebola_Teste", Descricao = "Ingrediente utilizado como tempero em uma variedade enorme de pratos e refeições." },
                    new Ingrediente { IngredienteId=7, Nome = "Óleo de Girassol_Teste", Descricao = "Ingrediente utilizado para fritar alimentos." },
                    new Ingrediente { IngredienteId=8, Nome = "Ovo_Teste", Descricao = "Ingrediente de origem animal utilizado em diversas receitas." },
                    new Ingrediente { IngredienteId=9, Nome = "Alho_Teste", Descricao = "Ingrediente utilizado para saborizar alimentos." }
                }.AsQueryable());


            IngredienteController ingrediente_ctrl = new IngredienteController(ingredienteMock.Object);


            //testa o método (act)
            
            var ingredientes = (List<IngredienteDTO>) ingrediente_ctrl.getIngredientesDisponiveis().Data;

            // verifica a corretude (assert)
            Assert.IsInstanceOf<List<IngredienteDTO>>(ingredientes);
        }
    }
}


