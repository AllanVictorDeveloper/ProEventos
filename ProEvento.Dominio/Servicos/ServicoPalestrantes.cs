using ProEvento.Dominio.Interfaces.Repositorios;
using ProEvento.Dominio.Interfaces.Servicos;
using ProEvento.Dominio.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProEvento.Dominio.Servicos
{
    public class ServicoPalestrantes : ServicoBase<Palestrante>, IServicoPalestrantes
    {
        private readonly IRepositorioPalestrantes _repositorioPalestrantes;
        public ServicoPalestrantes(IRepositorioPalestrantes repositorioPalestrantes) : base(repositorioPalestrantes)
        {
            _repositorioPalestrantes = repositorioPalestrantes;
        }

        public async Task<Palestrante[]> GetAllPalestrantesAsync(bool includeEventos = false)
        {
            return await _repositorioPalestrantes.GetAllPalestrantesAsync(includeEventos);
        }

        public async Task<Palestrante[]> GetAllPalestrantesByNomeAsync(string nome, bool includeEventos = false)
        {
            return await _repositorioPalestrantes.GetAllPalestrantesByNomeAsync(nome, includeEventos);
        }

        public async Task<Palestrante> GetPalestranteByIdAsync(int palestranteId, bool includeEventos)
        {
            return await _repositorioPalestrantes.GetPalestranteByIdAsync(palestranteId, includeEventos);
        }
    }
}
