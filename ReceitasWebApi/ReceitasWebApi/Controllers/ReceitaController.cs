using ReceitasWebApi.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Data.Entity;
using Newtonsoft.Json;

namespace ReceitasWebApi.Controllers
{
    public class ReceitaController : Controller
    {
       
        private IReceitaRepository receitaRepo;
        private IIngredienteRepository ingredienteRepo;

        private bool isReceitaRepository = true;

        // construtor que será normalmente utilizado pela aplicacao em running
        public ReceitaController()
        {
            this.receitaRepo = new EFReceitaRepository();
            this.ingredienteRepo = new EFIngredienteRepository();
        }

        // construtor que será utilizado pelos testes unitários
        public ReceitaController(IReceitaRepository receitaRepo, IIngredienteRepository ingredienteRepo)
        {
            this.receitaRepo = receitaRepo;
            this.ingredienteRepo = ingredienteRepo;
        }

        #region Métodos que mapeiam classe Receita e Ingrediente do banco de dados para um Data Transfer Objetc específico

        //método que mapeia Receita para seu DTO correspondente 
        private ReceitaDTO mapeiaParaReceitaDTO(Receita receita)
        {
            return new ReceitaDTO()
            {
                ReceitaId = receita.ReceitaId,
                Nome = receita.Nome,
                Porcoes = receita.Porcoes,
                Calorias = receita.Calorias,
                ModoPreparo = receita.ModoPreparo,
                Ingredientes = receita.Ingredientes.Select(igr => 
                new IngredienteDTO()
                {
                    IngredienteId = igr.IngredienteId,
                    Nome = igr.Nome,
                    Descricao = igr.Descricao
                }).ToList()
            };
        }

        //método que mapeia Ingrediente para seu DTO correspondente
        private IngredienteDTO mapeiaParaIngredienteDTO(Ingrediente ingrediente)
        {
            return new IngredienteDTO() {
                IngredienteId = ingrediente.IngredienteId,
                Nome = ingrediente.Nome,
                Descricao = ingrediente.Descricao
            };
        }

        #endregion

        #region GET /receitas (devolve todas as receitas em memória)

        [HttpGet] //restringe método para apenas aceitar requisicoes GET
        [Route("receitas")] //customiza rota com attribute routing para a especificada dentro de [Route(...)]
        public JsonResult buscarTodasreceitas()
        {

            isReceitaRepository = true;

            /* OBS:  É necessário usar o .ToList().AsEnumerable(), antes do .Select que chama a função de mapeamento DTO,
             *  para forçar a mudança de conexto do EF para Linq, uma vez que entity framework não consegue mapear 
             *  minha função customizada em SQL
            */
            var listaReceitas = receitaRepo.Receitas.Include("Ingredientes").ToList()
                                .AsEnumerable().Select(rec => mapeiaParaReceitaDTO(rec)).ToList();
                       
            return Json(listaReceitas, JsonRequestBehavior.AllowGet);
           
        }

        #endregion

        #region GET /receitas/{id} (devolve uma receita em memória por id)

        [HttpGet]
        [Route("receitas/{receitaId:int}")]
        public JsonResult buscarReceitaPorId(int receitaId)
        {

            isReceitaRepository = true;
            
            var receita = receitaRepo.Receitas.Where(r => r.ReceitaId == receitaId).ToList()
                          .AsEnumerable().Select(rec => mapeiaParaReceitaDTO(rec)).SingleOrDefault();

            if (receita == null)
            {
                 return Json(null, JsonRequestBehavior.AllowGet);
            }

            return Json(receita, JsonRequestBehavior.AllowGet);

        }

        #endregion

        #region GET /receitas/{id}/ingredientes (devolve os ingredientes de uma receita)

        [HttpGet] 
        [Route("receitas/{receitaId:int}/ingredientes")] 
        public JsonResult ingredientesDaReceitaEspecifica(int receitaId)
        {

            isReceitaRepository = true;
         
            var ingredientes = receitaRepo.Receitas.Where(r => r.ReceitaId == receitaId).ToList().AsEnumerable()
                               .SelectMany(r => r.Ingredientes.ToList().AsEnumerable()
                               .Select(ingrediente => mapeiaParaIngredienteDTO(ingrediente))).ToList();

            if (ingredientes == null)
            {
                return Json(new { success = false }, JsonRequestBehavior.AllowGet);
            }

            return Json(ingredientes, JsonRequestBehavior.AllowGet);

        }

        #endregion

        #region GET /receitas/ingredientes (devolve todos os ingredientes utilizados em receitas)

        [HttpGet]
        [Route("receitas/ingredientes")]
        public JsonResult ingredientesUtilizadosEmReceitas()
        {

            isReceitaRepository = false;

            var ingredientes = ingredienteRepo.Ingredientes.Where(i => i.Receitas.Count != 0).ToList()
                               .AsEnumerable().Select(ingrediente => mapeiaParaIngredienteDTO(ingrediente))
                               .ToList();

            if (ingredientes == null)
            {
                return Json(new { success = false }, JsonRequestBehavior.AllowGet);
            }

            return Json(ingredientes, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region GET /receitas/-/ingredientes/{id} (devolve receitas que contenham o ingrediente definido por id) | /receitas/ingredientes/{id}

        [HttpGet]
        [Route("receitas/ingredientes/{ingredienteId:int}")]
        public JsonResult getReceitasComIngrediente(int ingredienteId)
        {

            isReceitaRepository = false;

            var receitas_contendo_ingrediente = ingredienteRepo.Ingredientes.Where(i => i.IngredienteId == ingredienteId).ToList().AsEnumerable()
                                                               .SelectMany(i => i.Receitas.ToList().AsEnumerable().Select(receita => mapeiaParaReceitaDTO(receita))).ToList();

            if (receitas_contendo_ingrediente == null)
            {
                return Json(new { success = false}, JsonRequestBehavior.AllowGet);
            }
            
            return Json(receitas_contendo_ingrediente, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region  POST /receita (inicializa uma receita)

        [HttpPost]
        [Route("receita")]
        public JsonResult cria_receita([Bind(Exclude = "ReceitaId")] Receita receita)
        {
            isReceitaRepository = true;

            // verifica se o objeto receita está valido
            if (ModelState.IsValid)
            {
                receitaRepo.AdicionaReceita(receita);
                return Json(new { success = true });

            }else{

                var erros = new Dictionary<string,string>();
                
                /* para cada propriedade verifica se há erros associados a ela e adiciona a
                 * mensagem de erro no json de resposta
                */
                foreach (var chave in ModelState.Keys)
                {
                    ModelState modelState = null;
                    if (ModelState.TryGetValue(chave, out modelState))
                    {
                        foreach (var error in modelState.Errors)
                        {
                            erros.Add(chave, error.ErrorMessage); //adiciona nome do campo e msg de erro do mesmo
                        }
                    }
                }

                return Json(new { success = false, message = "Modelo está em estado inválido!", erros = erros });

            }

        }

        #endregion

        // método que que efetua o Dispose da instância do dbContext
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (isReceitaRepository)
                {
                    receitaRepo.DisposeReceitasDbContext(); //dispose receitas database context
                }
                else
                {
                    ingredienteRepo.DisposeReceitasDbContext(); //dispose ingredientes database context
                }
                                
            }
            base.Dispose(disposing);
        }

    }
}