using ControleCinema.ConsoleApp.Compartilhado;
using ControleCinema.ConsoleApp.ModuloFilme;
using ControleCinema.ConsoleApp.ModuloFuncionario;
using ControleCinema.ConsoleApp.ModuloIngresso;
using ControleCinema.ConsoleApp.ModuloSala;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleCinema.ConsoleApp.ModuloSessao
{
    public class TelaCadastroSessao : TelaBase, ITelaCadastravel
    {
        // emprestimo == sessao

        private readonly Notificador notificador;
        private readonly IRepositorio<Sessao> repositorioSessao;
        private readonly IRepositorio<Funcionario> repositorioFuncionario;
        private readonly IRepositorio<Sala> repositorioSala;
        private readonly IRepositorio<Filme> repositorioFilme;
        private readonly TelaCadastroSala telaCadastroSala;
        private readonly TelaCadastroFilme telaCadastroFilme;
        private readonly TelaCadastroFuncionario telaCadastroFuncionario;

        public TelaCadastroSessao(Notificador notificador, IRepositorio<Sessao> repositorioSessao, IRepositorio<Filme> repositorioFilme, IRepositorio<Funcionario> repositorioFuncionario, IRepositorio<Sala> repositorioSala, TelaCadastroSala telaCadastroSala, TelaCadastroFilme telaCadastroFilme, TelaCadastroFuncionario telaCadastroFuncionario) : base ("Cadastro de sessões")
        {
            this.notificador = notificador;
            this.repositorioSessao = repositorioSessao;
            this.repositorioFilme = repositorioFilme;
            this.repositorioFuncionario = repositorioFuncionario;
            this.repositorioSala = repositorioSala;
            this.telaCadastroSala = telaCadastroSala;
            this.telaCadastroFilme = telaCadastroFilme;
            this.telaCadastroFuncionario = telaCadastroFuncionario;
        }


        public override string MostrarOpcoes()
        {
            MostrarTitulo(Titulo);

            Console.WriteLine("Digite 1 para Registrar uma Sessão");
            Console.WriteLine("Digite 2 para Editar uma Sessão");
            Console.WriteLine("Digite 3 para Excluir uma Sessão");
            Console.WriteLine("Digite 4 para Visualizar as Sessões");
            Console.WriteLine("Digite 5 para Vender Ingressos");
            

            Console.WriteLine("Digite s para sair");

            string opcao = Console.ReadLine();

            return opcao;
        }


        public void Inserir()
        {
            MostrarTitulo("Inserindo nova Sessão");

            // Validação do Funcionario // Amigo == Funcionario
            Funcionario funcionarioSelecionado = ObtemFuncionario();

            if (funcionarioSelecionado == null)
            {
                notificador.ApresentarMensagem("Nenhum funcionario selecionado", TipoMensagem.Erro);
                return;
            }        


            // Validação do Filme // Filme == Revista
            Filme filmeSelecionado = ObtemFilme();

            //Validação da Sala // 

            Sala salaSelecionada = ObtemSala();

            //Validação da Sessão
            Sessao sessao = ObtemSessao(funcionarioSelecionado, salaSelecionada, filmeSelecionado);

            string statusValidacao = repositorioSessao.Inserir(sessao);

            if (statusValidacao == "REGISTRO_VALIDO")
                notificador.ApresentarMensagem("Sessão cadastrada com sucesso!", TipoMensagem.Sucesso);
            else
                notificador.ApresentarMensagem(statusValidacao, TipoMensagem.Erro);


        }
        public void Editar()
        {
            MostrarTitulo("Editando Sessões");

            bool temSessoesCadastradas = VisualizarSessoes("Pesquisando");

            if (temSessoesCadastradas == false)
            {
                notificador.ApresentarMensagem(
                    "Nenhuma sessão cadastrada para poder editar", TipoMensagem.Atencao);
                return;
            }
            int numeroSessao = ObterNumeroSessao();

            Funcionario funcionarioSelecionado = ObtemFuncionario();

            Filme filmeSelecionado = ObtemFilme();

            Sala  salaSelecionada = ObtemSala();

            Sessao sessaoAtualizada = ObtemSessao(funcionarioSelecionado, salaSelecionada, filmeSelecionado);

            bool conseguiuEditar = repositorioSessao.Editar(x => x.id == numeroSessao, sessaoAtualizada);

            if (!conseguiuEditar)
                notificador.ApresentarMensagem("Não foi possível editar.", TipoMensagem.Erro);
            else
                notificador.ApresentarMensagem("Sessão editada com sucesso", TipoMensagem.Sucesso);
        }

        public void Excluir()
        {
            MostrarTitulo("Excluindo Sessão");

            bool temSessoesCadastradas = VisualizarSessoes("Pesquisando");

            if (temSessoesCadastradas == false)
            {
                notificador.ApresentarMensagem(
                    "Nenhuma sessão cadastrada para poder excluir", TipoMensagem.Atencao);
                return;
            }

            int numeroSessao = ObterNumeroSessao();

            bool conseguiuExcluir = repositorioSessao.Excluir(x => x.id == numeroSessao);

            if (!conseguiuExcluir)
                notificador.ApresentarMensagem("Não foi possível editar.", TipoMensagem.Erro);
            else
                notificador.ApresentarMensagem("Sessão excluída com sucesso", TipoMensagem.Sucesso);
        }

        public bool VisualizarRegistros(string tipo)
        {
            if (tipo == "Tela")
                MostrarTitulo("Visualização de Sessões");

            List<Sessao> sessoes = repositorioSessao.SelecionarTodos();

            if (sessoes.Count == 0)
            {
                notificador.ApresentarMensagem("Não há nenhuma sessão ocorrendo", TipoMensagem.Atencao);
                return false;
            }

            foreach (Sessao sessao in sessoes)
                Console.WriteLine(sessao.ToString());

            Console.ReadLine();

            return true;
        }


        public bool VisualizarSessoes(string tipo)
        {
            if (tipo == "Tela")
                MostrarTitulo("Visualização de Sessões");

            List<Sessao> sessoes = repositorioSessao.SelecionarTodos();

            if (sessoes.Count == 0)
            {
                notificador.ApresentarMensagem("Não há nenhuma sessão disponível", TipoMensagem.Atencao);
                return false;
            }

            foreach (Sessao sessao in sessoes)
                Console.WriteLine(sessao.ToString());

            Console.ReadLine();

            return true;
        }

        public bool visializarDetalhes()
        {

            return true;
        }

        public bool obtemIngressos()
        {
            MostrarTitulo("Venda de Ingressos");

            List<Ingresso> ingressos = new List<Ingresso>();
            List<Sessao> sessoes = repositorioSessao.SelecionarTodos();

            if (sessoes.Count == 0)
            {
                notificador.ApresentarMensagem("Não há nenhuma sessão disponível", TipoMensagem.Atencao);
                return false;
            }

            foreach (Sessao sessao in sessoes)
                Console.WriteLine(sessao.ToString());

            Console.ReadLine();

            int sessaoSelecionada = ObterNumeroSessao();

          //  bool conseguiuVender = repositorioSessao.venderIngressos(sessaoSelecionada);

            return true;
        }


        //Métodos Privados

        private Sessao ObtemSessao(Funcionario funcionario,Sala sala , Filme filme)
        {
            Sessao novaSessao = new Sessao(funcionario, sala, filme);

            return novaSessao;
        }


        private Funcionario ObtemFuncionario()
        {
            bool temFuncionariosDisponiveis = telaCadastroFuncionario.VisualizarRegistros("Pesquisando");

            if (!temFuncionariosDisponiveis)
            {
                notificador.ApresentarMensagem("Não há nenhum funcionário disponível para cadastrar na sessão.", TipoMensagem.Atencao);
                return null;
            }

            Console.Write("Digite o número do funcionário que ficará responsável pela sessão:");
            int numeroFuncionarioSessao = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine();

            Funcionario funcionarioSelecionado = repositorioFuncionario.SelecionarRegistro(x => x.id == numeroFuncionarioSessao);

            return funcionarioSelecionado;
        }

        private Filme ObtemFilme()
        {
            bool temFilmesDisponiveis = telaCadastroFilme.VisualizarRegistros("Pesquisando");

            if (!temFilmesDisponiveis)
            {
                notificador.ApresentarMensagem("Não há nenhum filme disponível para cadastrar sessões.", TipoMensagem.Atencao);
                return null;
            }

            Console.Write("Digite o número do filme que irá para sessão: ");
            int numeroFilmeSessao = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine();

            Filme filmeSelecionado = repositorioFilme.SelecionarRegistro(x => x.id == numeroFilmeSessao);

            return filmeSelecionado;
        }

        private Sala ObtemSala()
        {
            bool temSalasDisponiveis = telaCadastroSala.VisualizarRegistros("Pesquisando");

            if (!temSalasDisponiveis)
            {
                notificador.ApresentarMensagem("Não há nenhuma sala disponível para cadastrar sessões.", TipoMensagem.Atencao);
            }

            Console.WriteLine("Digite o número da sala que será a sessão: ");
            int numeroSalaSessao = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine();

            Sala salaSelecionada = repositorioSala.SelecionarRegistro(x => x.id == numeroSalaSessao);

            return salaSelecionada;
        }

        private int ObterNumeroSessao()
        {
            int numeroSessao;
            bool numeroSessaoEncontrada;

            do
            {
                Console.Write("Digite o número da sessão que deseja selecionar: ");
                numeroSessao = Convert.ToInt32(Console.ReadLine());

                numeroSessaoEncontrada = repositorioSessao.ExisteRegistro(x => x.id == numeroSessao);

                if (!numeroSessaoEncontrada)
                    notificador.ApresentarMensagem("Número de sessão não encontrado, digite novamente", TipoMensagem.Atencao);

            } while (!numeroSessaoEncontrada);

            return numeroSessao;
        }
    }
}
