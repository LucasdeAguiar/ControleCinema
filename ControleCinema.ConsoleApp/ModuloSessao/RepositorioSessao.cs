using ControleCinema.ConsoleApp.Compartilhado;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleCinema.ConsoleApp.ModuloSessao
{
    public class RepositorioSessao : RepositorioBase<Sessao>
    {
        public RepositorioSessao()
        {
        }
        
        public bool venderIngressos(int numeroSessao)
        {
            return true;
        }
    }
}
