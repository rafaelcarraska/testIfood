using back_ifood.Model;
using System.Collections.Generic;

namespace back_ifood.Interface.IRepository
{
    public interface IProdutoRepository
    {
        List<Produto> Listar();

        Produto ListarProduto(string id);

        string Insert(Produto list_Produto);

        void Update(Produto list_Produto);

        void Delete(string id);

        long ExisteDescricao(Produto produto);
    }
}
