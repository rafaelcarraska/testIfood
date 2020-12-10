using back_ifood.Components;
using back_ifood.Interface.IBusiness;
using back_ifood.Interface.IRepository;
using back_ifood.Model;
using ifood_back.Dto;
using Microsoft.AspNetCore.Http;
using System;

namespace back_ifood.Business
{
    public class ProdutoBusiness : Uteis, IProdutoBusiness
    {
        Result result = new Result();
        private readonly IHttpContextAccessor _httpContext;
        private readonly IProdutoRepository _produtoRepository;

        public ProdutoBusiness(IHttpContextAccessor httpContext, IProdutoRepository produtoRepository)
        {
            _httpContext = httpContext;
            _produtoRepository = produtoRepository;
        }

        public Result Listar()
        {
            var result = new Result();
            try
            {
                result.sucesso = true;
                result.content = _produtoRepository.Listar();
            }
            catch (Exception ex)
            {
                result.sucesso = false;
                result.exception = ex.Message;
                result.erro = $"Erro ao listar Produtos";
            }
            return result;

        }

        public Produto ListarProduto(string id)
        {
            var Produto = _produtoRepository.ListarProduto(id);
            if (Produto != null)
            {
                return Produto;
            }
            return new Produto();
        }

        public Result Salvar(Produto produto)
        {
            result = Validar(produto);
            if (result.erro == null)
            {
                if (string.IsNullOrEmpty(produto.idProduto))
                {
                    return Insert(produto);
                }

                return Update(produto);
            }

            return result;
        }

        private Result Insert(Produto produto)
        {
            result = new Result();
            try
            {
                produto.idProduto = Guid.NewGuid().ToString();

                result.content = _produtoRepository.Insert(produto);
                result.sucesso = true;                
            }
            catch (Exception ex)
            {
                result.erro = $"Erro ao adicionar o Produto: {produto.descricao}.";
                result.exception = ex.Message;

            }
            return result;
        }

        private Result Update(Produto produto)
        {
            result = new Result();
            try
            {
                var ProdutoOriginal = ListarProduto(produto.idProduto);

                _produtoRepository.Update(produto);
                result.content = produto.idProduto;
                result.sucesso = true;
            }
            catch (Exception ex)
            {
                result.erro = $"Erro ao editar o Produto: {produto.descricao}.";
                result.exception = ex.Message;
            }
            return result;
        }

        public Result UpdateAnexo(string idProduto, string imagem)
        {
            result = new Result();
            try
            {
                var produto = ListarProduto(idProduto);
                produto.imagem = imagem;

                Update(produto);

                result.sucesso = true;
            }
            catch (Exception ex)
            {
                result.exception = ex.Message;
                result.erro = $"Erro ao adicionar participante ao Produto.";
            }
            return result;
        }

        public Result Validar(Produto produto)
        {
            result = new Result();
            if (!_produtoRepository.ExisteDescricao(produto).Equals(0))
            {
                result.sucesso = false;
                result.erro = "Essa descrição do Produto já existe!";
            }

            return result;
        }

        public Result Delete(string id)
        {
            result = new Result();
            try
            {
                _produtoRepository.Delete(id);
                result.sucesso = true;
            }
            catch (Exception ex)
            {
                result.erro = $"Erro ao deletar o Produto.";
                result.exception = ex.Message;
            }
            return result;

        }
    }
}
