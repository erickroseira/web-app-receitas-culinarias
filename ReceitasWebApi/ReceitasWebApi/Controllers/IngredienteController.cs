using ReceitasWebApi.Models;
using System.Linq;
using System.Web.Mvc;

namespace ReceitasWebApi.Controllers
{
    public class IngredienteController : Controller
    {

        private IIngredienteRepository ingredienteRepo;

        // construtor utilizado pelo funcionamento normal do programa 
        public IngredienteController()
        {
            this.ingredienteRepo = new EFIngredienteRepository();
        }

        // construtor que será utilizado pelos testes unitários
        public IngredienteController(IIngredienteRepository ingredienteRepo)
        {
            this.ingredienteRepo = ingredienteRepo;
        }

        #region GET /ingredientesNgDropDown (retorna a lista de ingredientes no formato padrão do ng-dropdown-multiselect)

        [HttpGet] //restringe método para apenas aceitar requisicoes GET
        [Route("ingredientesNgDropDown")] //customiza rota com attribute routing para a especificada dentro de [Route(...)]
        public JsonResult get_ingredientes()
        {
           var listaIngredientes = ingredienteRepo.Ingredientes.Select(i => new {
                id = i.IngredienteId,
                label = i.Nome,
                i.IngredienteId,
                i.Nome,
                i.Descricao
            }).ToList();

            return Json(listaIngredientes, JsonRequestBehavior.AllowGet);

        }

        #endregion

        #region GET /ingredientes (devolve os ingredientes disponíveis, ordenados alfabeticamente)

        [HttpGet]
        [Route("ingredientes")]
        public JsonResult getIngredientesDisponiveis()
        {

            var listaOrdendaIngredientes = ingredienteRepo.Ingredientes.OrderBy(ingrediente => ingrediente.Nome)
                                                      .Select(i => new  IngredienteDTO(){
                                                          IngredienteId = i.IngredienteId,
                                                          Nome = i.Nome,
                                                          Descricao = i.Descricao
                                                      }).ToList();


            return Json(listaOrdendaIngredientes, JsonRequestBehavior.AllowGet);            
        }

        #endregion

        // método que que efetua o Dispose da instância do dbContext
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                ingredienteRepo.DisposeReceitasDbContext();
            }
            base.Dispose(disposing);
        }
    }
   
}