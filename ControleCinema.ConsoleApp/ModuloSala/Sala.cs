using ControleCinema.ConsoleApp.Compartilhado;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleCinema.ConsoleApp.ModuloSala
{
    public class Sala : EntidadeBase
    {
        private int capacidade;
        private readonly int numeroAssentos;

        public Sala(int capacidade, int numeroAssentos)
        {
            this.capacidade = capacidade;
            this.numeroAssentos = numeroAssentos;
        }

        public int Capacidade => capacidade;

        public int NumeroAssentos => numeroAssentos;

        public void reservarAssentos(int quantidade)
        {
            this.capacidade -= quantidade;
        }

        public override string ToString()
        {

            return "Número: " + id + Environment.NewLine +
            "Capacidade: " + Capacidade + Environment.NewLine +
            "Número de assentos: " + NumeroAssentos + Environment.NewLine;
           
        }


    }
}
