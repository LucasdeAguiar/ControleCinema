using ControleCinema.ConsoleApp.Compartilhado;
using ControleCinema.ConsoleApp.ModuloFilme;
using ControleCinema.ConsoleApp.ModuloFuncionario;
using ControleCinema.ConsoleApp.ModuloSala;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleCinema.ConsoleApp.ModuloSessao
{
    public class Sessao : EntidadeBase
    {
        private readonly Funcionario funcionario;
        private Filme filme;
        private Sala sala;
        private DateTime horario;
        private int qtIngressosDisponiveis;
        private bool EstaAberto;
        public string Status { get => EstaAberto ? "Aberto" : "Fechado"; }

        public Sessao(Funcionario funcionario, Sala sala, Filme filme)
        {
            this.funcionario = funcionario;
            this.sala = sala;
            this.filme = filme;
            this.qtIngressosDisponiveis = this.sala.NumeroAssentos;
        }

        public void venderIngresso(int quantidade)
        {
            if (qtIngressosDisponiveis >= 0)
            {
                Console.WriteLine("Os ingressos acabaram..");
            }
            else
            {
                qtIngressosDisponiveis -= quantidade;
            }
        }

        public override string ToString()
        {
            return "Sessão: " + id + Environment.NewLine +
                "Filme: " + filme.Titulo + Environment.NewLine +
                "Sala: " + sala.id + Environment.NewLine +
                "Quantidade de Ingressos: " + this.qtIngressosDisponiveis + Environment.NewLine;
             
        }

        public void abrir()
        {
            if (!EstaAberto)
            {
                EstaAberto = true;
                horario = DateTime.Today;
            
            }
        }

        public void fechar()
        {
            if (!EstaAberto)
            {
                EstaAberto = false;
                horario = DateTime.Now;

            }
        }

    }
}
