using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ReceitasWebApi.Models
{
    public interface IIngredienteRepository
    {
        IQueryable<Ingrediente> Ingredientes { get; }

        void DisposeReceitasDbContext();
    }
}