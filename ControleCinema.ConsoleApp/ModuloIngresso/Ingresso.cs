using ControleCinema.ConsoleApp.Compartilhado;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleCinema.ConsoleApp.ModuloIngresso
{
    public class Ingresso : EntidadeBase
    {
        private readonly string tipo;

        public Ingresso(string tipo)
        {
            this.tipo = tipo;
        }

        public string Tipo => tipo;


        public override string ToString()
        {

            return "Número: " + id + Environment.NewLine +
            "Tipo: " + tipo + Environment.NewLine;

        }

    }
}
