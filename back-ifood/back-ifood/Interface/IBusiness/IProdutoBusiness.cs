using back_ifood.Model;
using ifood_back.Dto;

namespace back_ifood.Interface.IBusiness
{
    public interface IProdutoBusiness
    {
        Result Listar();

        Produto ListarProduto(string idProduto);

        Result Salvar(Produto produto);

        Result UpdateAnexo(string idProduto, string imagem);

        Result Validar(Produto produto);

        Result Delete(string idProduto);
    }
}
