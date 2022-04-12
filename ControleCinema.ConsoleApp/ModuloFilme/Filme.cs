using ControleCinema.ConsoleApp.Compartilhado;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ControleCinema.ConsoleApp.ModuloGenero;

namespace ControleCinema.ConsoleApp.ModuloFilme
{
    public class Filme : EntidadeBase
    {
        private readonly string titulo;
        private readonly int duracao;
        private readonly Genero genero;

        public Filme(string titulo, int duracao , Genero generoSelecionado)
        {
            this.titulo = titulo;
            this.duracao = duracao;
            genero = generoSelecionado;
           
        }

        public string Titulo => titulo;

        public int Duracao => duracao;

        public Genero Genero => genero;

        public override string ToString()
        {

            return "Número: " + id + Environment.NewLine +
            "Título: " + Titulo + Environment.NewLine +
            "Duração: " + Duracao + Environment.NewLine + "min " + 
            "Gênero: " + Genero.Descricao + Environment.NewLine;

        }
    }
}
