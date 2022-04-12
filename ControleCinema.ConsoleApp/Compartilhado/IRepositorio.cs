using System;
using System.Collections.Generic;

namespace ControleCinema.ConsoleApp.Compartilhado
{
    public interface IRepositorio<T> where T : EntidadeBase
    {
        string Inserir(T entidade);
        bool Editar(int idSelecionado, T novaEntidade);
        bool Excluir(int idSelecionado);
        bool ExisteRegistro(int idSelecionado);
        T SelecionarRegistro(int idSelecionado);
        List<T> Filtrar(Predicate<T> condicao);
        List<T> SelecionarTodos();

        T SelecionarRegistro(Predicate<T> condicao);

        bool ExisteRegistro(Predicate<T> condicao);

        bool Editar(Predicate<T> condicao, T novaEntidade);

        bool Excluir(Predicate<T> condicao);

  

    }
}