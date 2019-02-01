using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReceitasWebApi.Models
{
    public interface IReceitaRepository
    {
        IQueryable<Receita> Receitas { get; }

        void AdicionaReceita(Receita receita);

        void DisposeReceitasDbContext();
    }
}
