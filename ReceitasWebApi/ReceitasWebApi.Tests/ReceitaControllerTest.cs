using Moq;
using NUnit.Framework;
using ReceitasWebApi.Controllers;
using ReceitasWebApi.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Script.Serialization;

namespace ReceitasWebApi.Tests
{
    [TestFixture]
    public class ReceitaControllerTest
    {

        private Mock<IReceitaRepository> receitasMock;
        private Mock<IIngredienteRepository> ingredienteMock;
        private ReceitaController receitaCtrl;

        public ReceitaControllerTest()
        {
            this.ingredienteMock = new Mock<IIngredienteRepository>();
            this.receitasMock = new Mock<IReceitaRepository>();

            #region criacao do MOCK de Ingredientes

            // Simulando dados da tabela de ingredientes por meio do moCK
            this.ingredienteMock.Setup(i => i.Ingredientes).Returns(new List<Ingrediente>()
                {
                    new Ingrediente { IngredienteId=1, Nome = "Leite Bovino_Teste", Descricao = "Ingrediente de origem animal utilizado no preparo de queijos, bolos, etc.",
                                      Receitas = new List<Receita>{
                                          new Receita { ReceitaId = 1, Nome = "Bolo de Sal", Porcoes = 10, Calorias = 890, ModoPreparo = "Pegue o sal, misture a farinha de trigo com o leite ..." }
                                      }
                                    },
                    new Ingrediente { IngredienteId=2, Nome = "Cominho_Teste", Descricao = "Especiaria utilizada para saborizar alimentos.", Receitas = new List<Receita>{ } },
                    new Ingrediente { IngredienteId=3, Nome = "Peito de Frango_Teste", Descricao = "Ingrediente de origem animal utilizada como proteína principal.",
                                      Receitas = new List<Receita>
                                      {
                                          new Receita { ReceitaId = 2, Nome = "Almoço", Porcoes = 10, Calorias = 1290, ModoPreparo = "Pegue o feijao e arroz e cozinhe, depois frite o frango e coma ..." }
                                      }
                                    },
                    new Ingrediente { IngredienteId=4, Nome = "Feijão_Teste", Descricao = "Leguminosa bastante utilizada no almoço.",
                                      Receitas = new List<Receita>
                                      {
                                          new Receita { ReceitaId = 2, Nome = "Almoço", Porcoes = 10, Calorias = 1290, ModoPreparo = "Pegue o feijao e arroz e cozinhe, depois frite o frango e coma ...",
                                                        Ingredientes = new List<Ingrediente>(){
                                                                                                new Ingrediente() {IngredienteId = 4, Nome = "Feijão_Teste", Descricao = "Leguminosa bastante utilizada no almoço." },
                                                                                                new Ingrediente() {IngredienteId = 5, Nome = "Arroz_Teste", Descricao = "Ingrediente muito comum utilizado no preparo de refeições." },
                                                                                                new Ingrediente() {IngredienteId = 3, Nome = "Peito de Frango_Teste", Descricao = "Ingrediente de origem animal utilizada como proteína principal." },
                                                                                                new Ingrediente() {IngredienteId = 12, Nome = "Sal_Teste", Descricao ="Ingrediente utilizado para dar ou realçar os sabores do alimento." }
                  }
                                                        }
                                      }
                                    },
                    new Ingrediente { IngredienteId=5, Nome = "Arroz_Teste", Descricao = "Ingrediente muito comum utilizado no preparo de refeições.",
                                      Receitas = new List<Receita>
                                      {
                                          new Receita { ReceitaId = 2, Nome = "Almoço", Porcoes = 10, Calorias = 1290, ModoPreparo = "Pegue o feijao e arroz e cozinhe, depois frite o frango e coma ..." }
                                      }
                                    },
                    new Ingrediente { IngredienteId=6, Nome = "Cebola_Teste", Descricao = "Ingrediente utilizado como tempero em uma variedade enorme de pratos e refeições.", Receitas = new List<Receita>{ } },
                    new Ingrediente { IngredienteId=7, Nome = "Óleo de Girassol_Teste", Descricao = "Ingrediente utilizado para fritar alimentos.", Receitas = new List<Receita>{ } },
                    new Ingrediente { IngredienteId=8, Nome = "Ovo_Teste", Descricao = "Ingrediente de origem animal utilizado em diversas receitas.", Receitas = new List<Receita>{ } },
                    new Ingrediente { IngredienteId=9, Nome = "Alho_Teste", Descricao = "Ingrediente utilizado para saborizar alimentos.", Receitas = new List<Receita>{ } },
                    new Ingrediente { IngredienteId=10, Nome = "Farinha de Trigo_Teste", Descricao = "Ingrediente utilizado para preparo de bolos, massas, tortas, pão, etc.",
                                      Receitas = new List<Receita>
                                      {
                                          new Receita { ReceitaId = 1, Nome = "Bolo de Sal", Porcoes = 10, Calorias = 890, ModoPreparo = "Pegue o sal, misture a farinha de trigo com o leite ..." }
                                      }
                                    },
                    new Ingrediente { IngredienteId=11, Nome = "Açúcar_Teste", Descricao = "Ingrediente utilizado para adoçicar o alimento/preparo.",
                                      Receitas = new List<Receita>
                                      {
                                          new Receita { ReceitaId = 1, Nome = "Bolo de Sal", Porcoes = 10, Calorias = 890, ModoPreparo = "Pegue o sal, misture a farinha de trigo com o leite ..." }
                                      }
                                    },
                    new Ingrediente { IngredienteId=12, Nome = "Sal_Teste", Descricao ="Ingrediente utilizado para dar ou realçar os sabores do alimento.",
                                      Receitas = new List<Receita>
                                      {
                                          new Receita { ReceitaId = 1, Nome = "Bolo de Sal", Porcoes = 10, Calorias = 890, ModoPreparo = "Pegue o sal, misture a farinha de trigo com o leite ..." },
                                          new Receita { ReceitaId = 2, Nome = "Almoço", Porcoes = 10, Calorias = 1290, ModoPreparo = "Pegue o feijao e arroz e cozinhe, depois frite o frango e coma ..." }
                                      }      
                                    }
                }.AsQueryable());

            #endregion


            #region criacão do MOCK de Receitas

            // Simulando dados do banco (receitas) por meio do moCK. 

            this.receitasMock.Setup(r => r.Receitas).Returns(new List<Receita>()
            {
              new Receita(){
                  ReceitaId = 1, Nome = "Bolo de Sal", Porcoes = 10, Calorias = 890,
                  Ingredientes = new List<Ingrediente>(){
                      new Ingrediente() {IngredienteId = 12, Nome = "Sal_Teste", Descricao ="Ingrediente utilizado para dar ou realçar os sabores do alimento." },
                      new Ingrediente() {IngredienteId = 11, Nome = "Açúcar_Teste", Descricao = "Ingrediente utilizado para adoçicar o alimento/preparo." },
                      new Ingrediente() {IngredienteId = 10, Nome = "Farinha de Trigo_Teste", Descricao = "Ingrediente utilizado para preparo de bolos, massas, tortas, pão, etc." },
                      new Ingrediente() {IngredienteId = 1, Nome = "Leite Bovino_Teste", Descricao = "Ingrediente de origem animal utilizado no preparo de queijos, bolos, etc." }
                  },
                  ModoPreparo = "Pegue o sal, misture a farinha de trigo com o leite ..."
              },
              new Receita(){
                  ReceitaId = 2, Nome = "Almoço", Porcoes = 10, Calorias = 1290,
                  Ingredientes = new List<Ingrediente>(){
                      new Ingrediente() {IngredienteId = 4, Nome = "Feijão_Teste", Descricao = "Leguminosa bastante utilizada no almoço." },
                      new Ingrediente() {IngredienteId = 5, Nome = "Arroz_Teste", Descricao = "Ingrediente muito comum utilizado no preparo de refeições." },
                      new Ingrediente() {IngredienteId = 3, Nome = "Peito de Frango_Teste", Descricao = "Ingrediente de origem animal utilizada como proteína principal." },
                      new Ingrediente() {IngredienteId = 12, Nome = "Sal_Teste", Descricao ="Ingrediente utilizado para dar ou realçar os sabores do alimento." }
                  },
                  ModoPreparo = "Pegue o feijao e arroz e cozinhe, depois frite o frango e coma ..."
              }
            }.AsQueryable());

            #endregion

            //instancia o controller que será utilizado pelos demais métodos de teste
            this.receitaCtrl = new ReceitaController(this.receitasMock.Object, this.ingredienteMock.Object);
        }

        #region TESTES GET /receitas (devolve todas as receitas em memória)

        [Test]
        public void teste_GetTodasReceitas()
        {
            //Executando a ação de buscar todas as receitas (act)
            var receitas = (List<ReceitaDTO>) receitaCtrl.buscarTodasreceitas().Data;

            //Verifica se a lista retornada é do tipo ReceitaDTO (assert)
            Assert.IsInstanceOf<List<ReceitaDTO>>(receitas);
        }

        [Test]
        public void teste_GetTodasReceitas_Contagem_1()
        {
            //cria mock local de receitas
            Mock<IReceitaRepository> receitasMock_Contagem_1 = new Mock<IReceitaRepository>();

            //mock de receitas com apenas uma receita
            receitasMock_Contagem_1.Setup(r => r.Receitas).Returns(new List<Receita>() {
                    new Receita(){
                  ReceitaId = 1, Nome = "Bolo de Sal", Porcoes = 10, Calorias = 890,
                  Ingredientes = new List<Ingrediente>(){
                      new Ingrediente() {IngredienteId = 10, Nome = "Farinha de Trigo_Teste", Descricao = "Ingrediente utilizado para preparo de bolos, massas, tortas, pão, etc." },
                      new Ingrediente() {IngredienteId = 1, Nome = "Leite Bovino_Teste", Descricao = "Ingrediente de origem animal utilizado no preparo de queijos, bolos, etc." }
                  },
                  ModoPreparo = "Pegue o sal, misture a farinha de trigo com o leite ..."
              }
            }.AsQueryable());

            ReceitaController receitaCtrl = new ReceitaController(receitasMock_Contagem_1.Object, ingredienteMock.Object);

            var receitas = (List<ReceitaDTO>)receitaCtrl.buscarTodasreceitas().Data;

            //verifica se a lista tem apenas um elemento
            Assert.AreEqual(1, receitas.Count);

        }

        [Test]
        public void teste_GetTodasReceitas_RetornaListaVazia()
        {
            //cria um mock com zero receitas
            Mock<IReceitaRepository> receitasMock_Vazia = new Mock<IReceitaRepository>();
            receitasMock_Vazia.Setup(r => r.Receitas).Returns(new List<Receita>() { }.AsQueryable());

            ReceitaController receitaCtrl = new ReceitaController(receitasMock_Vazia.Object, ingredienteMock.Object);

            var receitas = (List<ReceitaDTO>) receitaCtrl.buscarTodasreceitas().Data;

            //verifica se a lista retorna está vazia
            Assert.That(receitas, Is.Empty);
        }

        #endregion

        #region TESTES GET /receitas/{id} (devolve uma receita em memória por id)

        [Test]
        public void teste_GetReceitaEspecificaPeloId()
        {
            //Executando a ação de buscar receita específica pelo id. Neste caso estamos buscando a Receita de ID 1 que é a bolo de Sal, já definida no mock
            var receita = (ReceitaDTO)receitaCtrl.buscarReceitaPorId(1).Data;

            //Verifica corretude do método (assert) 
            Assert.IsTrue(receita.ReceitaId == 1);          //verifica se o id da receita retornada é igual 1, como esperado
            Assert.IsTrue(receita.Nome.Equals("Bolo de Sal")); ////verifica se o nome da receita retornada é igual 'Bolo de Sal', como esperado
        }

        [Test]
        public void teste_GetReceitaEspecificaPeloId_NaoExistente()
        {
            // Executando a ação de buscar receita específica pelo id. Neste caso 
            // como nção existe Receita de id 1000, o método deve retornar objeto vazio
            var receita = receitaCtrl.buscarReceitaPorId(1000).Data;

            //Verifica corretude do método (assert) 
            Assert.That(receita, Is.Null);          //verifica se o id da receita retornada é igual 1, como esperado
            
        }

        #endregion

        #region TESTES GET /receitas/{id}/ingredientes (devolve os ingredientes de uma receita)

        [Test]
        public void teste_GetIngredientesDeReceitaEspecifica()
        {
            //Executando a ação de buscar receita específica pelo id. Neste caso estamos buscando a Receita de ID 2 que é Almoço, já definida no mock
            var ingredientes = (List<IngredienteDTO>) receitaCtrl.ingredientesDaReceitaEspecifica(2).Data;

            // assert

            Assert.DoesNotThrow(delegate {

                /* id dos ingredientes da Receita de Id 12 que é o Almoço;
                 * verifica se todos eles estão presentes na lista de ingredientes
                 * retornados pelo metodo ingredientesDaReceitaEspecifica(2)
                 */
                int[] ingredientes_Id_Receita_Id2 = new int[] { 4, 5, 3, 12 };
                foreach(int igr_id in ingredientes_Id_Receita_Id2)
                {
                    // se o ingrediente não existir será lançada uma exceção do tipo System.NullReferenceException
                    string nome_receita = ingredientes.Find(i => i.IngredienteId == igr_id).Nome; 
                }
                
            });                                                 
        }

        #endregion

        #region TESTES GET /receitas/ingredientes (devolve todos os ingredientes utilizados em receitas)

        [Test]
        public void teste_GetIngredientesUtilizadosEmReceitas()
        {
            //Executando a ação de buscar apenas os ingredientes utilizados em receitas
            var igredientes_utilizados_em_receitas = (List<IngredienteDTO>) receitaCtrl.ingredientesUtilizadosEmReceitas().Data;

            //Verifica se todos os ingredientes utilizados em receitas está na lista de retorno do método 
            Assert.DoesNotThrow(delegate {

                // id de todos os ingredientes utilizados em receitas 

                int[] id_ingredientes_utilizados_em_receitas = new int[] {1, 4, 5, 3, 10, 11, 12 };

                foreach (int igr_id in id_ingredientes_utilizados_em_receitas)
                {
                    // se o ingrediente não existir será lançada uma exceção do tipo System.NullReferenceException
                    string nome_receita = igredientes_utilizados_em_receitas.Find(i => i.IngredienteId == igr_id).Nome;
                }

            });
        }

        [Test]
        public void teste_GetIngredientesUtilizadosEmReceitas_Retorna_Lista_Vazia()
        {

            //cria um mock com zero receitas
            Mock<IReceitaRepository> receitasMock_Vazia = new Mock<IReceitaRepository>();
            receitasMock_Vazia.Setup(r => r.Receitas).Returns(new List<Receita>() { }.AsQueryable());

            //cria um mock de ingredientes mas nenhum deles sendo utilizado em receitas
            Mock<IIngredienteRepository> ingredienteMock_nenhum_Utilizado = new Mock<IIngredienteRepository>();

            ingredienteMock_nenhum_Utilizado.Setup(i => i.Ingredientes).Returns(new List<Ingrediente>()
                {
                    new Ingrediente { IngredienteId=1, Nome = "Leite Bovino_Teste", Descricao = "Ingrediente de origem animal utilizado no preparo de queijos, bolos, etc.", Receitas = new List<Receita>()},
                    new Ingrediente { IngredienteId=2, Nome = "Cominho_Teste", Descricao = "Especiaria utilizada para saborizar alimentos.", Receitas = new List<Receita>()},
                    new Ingrediente { IngredienteId=3, Nome = "Peito de Frango_Teste", Descricao = "Ingrediente de origem animal utilizada como proteína principal.", Receitas = new List<Receita> ()},                   
                }.AsQueryable());

            ReceitaController receitaCtrl = new ReceitaController(receitasMock_Vazia.Object, ingredienteMock_nenhum_Utilizado.Object);

            //Executando a ação de buscar apenas os ingredientes utilizados em receitas
            var igredientes_utilizados_em_receitas = (List<IngredienteDTO>)receitaCtrl.ingredientesUtilizadosEmReceitas().Data;

            //verifica se a lista retornada está vazia como esperado
            Assert.That(igredientes_utilizados_em_receitas, Is.Empty);
        }

        #endregion


        #region TESTES da API GET /receitas/-/ingredientes/{id} (devolve receitas que contenham o ingrediente definido por id) | /receitas/ingredientes/{id}

        [Test]
        public void teste_GetReceitasComIngrediente()
        {
            //Executando a ação de buscar apenas os ingredientes utilizados em receitas
            var receitas_com_feijao = (List<ReceitaDTO>)receitaCtrl.getReceitasComIngrediente(4).Data;

            //verifica se a lista não está vazia
            Assert.That(receitas_com_feijao, Is.Not.Empty);

            // verifica se todos as receitas retornadas tem feijão (id=4) em seus ingredientes
            Assert.That(receitas_com_feijao.All(r=>r.Ingredientes.Any(i=>i.IngredienteId == 4)));            
        }

        [Test]
        public void teste_GetReceitasComIngrediente_Retorna_Nenhuma_Receita()
        {
            //Busca receitas com ingredientes Cebola_Teste ainda não utilizado
            var receitas_com_cebola = (List<ReceitaDTO>)receitaCtrl.getReceitasComIngrediente(6).Data;

            //verifica se a lista retornada está vazia como esperado
            Assert.That(receitas_com_cebola, Is.Empty);
            
        }

        #endregion

        #region TESTES POST /receita (inicializa uma receita)

        [Test]
        public void teste_AdicionaNovaReceita_Sucesso()
        {
            //adiciona receita com todos os campos obrigatórios definidos
            Receita nova_receita = new Receita()
            {
                Nome = "NOVO Bolo NOVO de Sal",
                Porcoes = 15,
                Calorias = 990,
                Ingredientes = new List<Ingrediente>(){
                      new Ingrediente() {IngredienteId = 12, Nome = "Sal_Teste", Descricao ="Ingrediente utilizado para dar ou realçar os sabores do alimento." },
                      new Ingrediente() {IngredienteId = 11, Nome = "Açúcar_Teste", Descricao = "Ingrediente utilizado para adoçicar o alimento/preparo." },
                      new Ingrediente() {IngredienteId = 10, Nome = "Farinha de Trigo_Teste", Descricao = "Ingrediente utilizado para preparo de bolos, massas, tortas, pão, etc." },
                      new Ingrediente() {IngredienteId = 1, Nome = "Leite Bovino_Teste", Descricao = "Ingrediente de origem animal utilizado no preparo de queijos, bolos, etc." }
                  },
                ModoPreparo = "Pegue o sal, misture a farinha de trigo com o leite ..."
            };

            JavaScriptSerializer serializar = new JavaScriptSerializer();

            var resposta = serializar.Serialize(receitaCtrl.cria_receita(nova_receita).Data);

            //verifica se o json retornado é o json de sucesso
            Assert.AreEqual(@"{""success"":true}", resposta);
        }

        [Test]
        public void teste_AdicionaNovaReceita_Ingredientes_Faltando_Erro()
        {
            // cria receita sem informar os ingredientes
            Receita nova_receita = new Receita()
            {
                Nome = "NOVO Bolo NOVO de Sal",
                Porcoes = 15,
                Calorias = 990 ,
                ModoPreparo = "Pegue o sal, misture a farinha de trigo com o leite ..."
            };

            ReceitaController receitaCtrl = new ReceitaController(receitasMock.Object, ingredienteMock.Object);

            // simula um estado inválido do objeto nova_receita
            receitaCtrl.ModelState.AddModelError("Ingredientes", "A receita precisa ter pelo menos um ingrediente.");

            JavaScriptSerializer serializar = new JavaScriptSerializer();

            var resposta = serializar.Serialize(receitaCtrl.cria_receita(nova_receita).Data);

            //verifica se json retornado é o json esperado de erro
            Assert.AreEqual(@"{""success"":false,""message"":""Modelo está em estado inválido!"",""erros"":{""Ingredientes"":""A receita precisa ter pelo menos um ingrediente.""}}", resposta);

        }

        #endregion

    }
}
