using ControleCinema.ConsoleApp.Compartilhado;
using ControleCinema.ConsoleApp.ModuloFuncionario;
using ControleCinema.ConsoleApp.ModuloGenero;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleCinema.ConsoleApp.ModuloFilme
{
    public class TelaCadastroFilme : TelaBase, ITelaCadastravel
    {
        private readonly TelaCadastroGenero telaCadastroGenero;
        private readonly IRepositorio<Genero> repositorioGenero;
        private readonly IRepositorio<Filme> _repositorioFilme;
        private readonly Notificador _notificador;

        public TelaCadastroFilme(TelaCadastroGenero telaCadastroGenero, IRepositorio<Genero> repositorioGenero, IRepositorio<Filme> repositorioFilme, Notificador notificador) : base("Cadastro de Filmes")
        {
            this.telaCadastroGenero = telaCadastroGenero;
            this.repositorioGenero = repositorioGenero;
            _repositorioFilme = repositorioFilme;
            _notificador = notificador;
        }

        //Genero == Categoria
        public void Inserir() 
        {
            MostrarTitulo("Inserindo novo Filme");

            Genero generoSelecionado = ObtemGenero();

            

            if (generoSelecionado == null)
            {
                _notificador.ApresentarMensagem("Cadastre um gênero antes de cadastrar filmes!", TipoMensagem.Atencao);
                return;
            }

            Filme novoFilme = ObterFilme(generoSelecionado);

            string statusValidacao = _repositorioFilme.Inserir(novoFilme);

            if (statusValidacao == "REGISTRO_VALIDO")
                _notificador.ApresentarMensagem("Filme inserido com sucesso", TipoMensagem.Sucesso);
            else
                _notificador.ApresentarMensagem(statusValidacao, TipoMensagem.Erro);
        }
        public void Editar()
        {
            MostrarTitulo("Editando Filme");

            bool temFilmesCadastrados = VisualizarRegistros("Pesquisando");

            if (temFilmesCadastrados == false)
            {
                _notificador.ApresentarMensagem("Nenhuma filme cadastrado para poder editar", TipoMensagem.Atencao);
                return;
            }

            int numeroFilme = ObterNumeroFilme();

            Console.WriteLine();


            Genero generoSelecionado = ObtemGenero();

            Filme filmeAtualizado = ObterFilme(generoSelecionado);

            bool conseguiuEditar = _repositorioFilme.Editar(x => x.id == numeroFilme, filmeAtualizado);

            if (!conseguiuEditar)
                _notificador.ApresentarMensagem("Não foi possível excluir.", TipoMensagem.Sucesso);
            else
                _notificador.ApresentarMensagem("Revista editada com sucesso", TipoMensagem.Sucesso);
        }

        public void Excluir()
        {
            MostrarTitulo("Excluindo Filme");

            bool temFilmesCadastrados = VisualizarRegistros("Pesquisando");

            if (temFilmesCadastrados == false)
            {
                _notificador.ApresentarMensagem(
                    "Nenhum filme cadastrado para poder excluir", TipoMensagem.Atencao);
                return;
            }

            int numeroFilme = ObterNumeroFilme();

            bool conseguiuExcluir = _repositorioFilme.Excluir(x => x.id == numeroFilme);

            if (!conseguiuExcluir)
                _notificador.ApresentarMensagem("Não foi possível excluir.", TipoMensagem.Sucesso);
            else
                _notificador.ApresentarMensagem("Revista excluída com sucesso", TipoMensagem.Sucesso);
        }

        
        public bool VisualizarRegistros(string tipo)
        {
            if (tipo == "Tela")
                MostrarTitulo("Visualização de Filmes");

            List<Filme> filmes = _repositorioFilme.SelecionarTodos();

            if (filmes.Count == 0)
            {
                _notificador.ApresentarMensagem("Não há nenhum filme disponível", TipoMensagem.Atencao);
                return false;
            }

            foreach (Filme filme in filmes)
                Console.WriteLine(filme.ToString());

            Console.ReadLine();

            return true;
        }


        //Métodos Privados

        private Filme ObterFilme( Genero generoSelecionado)
        {
            Console.Write("Digite o título do filme: ");
            string titulo = Console.ReadLine();

            Console.Write("Digite a duração do filme: ");
            int duracao = Convert.ToInt32(Console.ReadLine());

            

            Filme novoFilme = new Filme(titulo,duracao, generoSelecionado);

            return novoFilme;
        }



        private Genero ObtemGenero()
        {
            bool temGenerosDisponiveis = telaCadastroGenero.VisualizarRegistros("");

            if (!temGenerosDisponiveis)
            {
                _notificador.ApresentarMensagem("Você precisa cadastrar um gênero antes de um filme!", TipoMensagem.Atencao);
                return null;
            }

            Console.Write("Digite o número do gênero do filme: ");
            int numGeneroSelecionado = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine();

            Genero generoSelecionado = repositorioGenero.SelecionarRegistro(x => x.id == numGeneroSelecionado);

            return generoSelecionado;
        }

        private int ObterNumeroFilme()
        {
            int numeroFilme;
            bool numeroFilmeEncontrado;

            do
            {
                Console.Write("Digite o número do filme que deseja selecionar: ");
                numeroFilme = Convert.ToInt32(Console.ReadLine());

                numeroFilmeEncontrado = _repositorioFilme.ExisteRegistro(x => x.id == numeroFilme);

                if (numeroFilmeEncontrado == false)
                    _notificador.ApresentarMensagem("Número de revista não encontrado, digite novamente", TipoMensagem.Atencao);

            } while (numeroFilmeEncontrado == false);

            return numeroFilme;
        }
    }
}
