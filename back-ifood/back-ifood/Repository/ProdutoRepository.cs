using back_ifood.Model;
using back_ifood.Data;
using back_ifood.Interface.IRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace back_ifood.Repository
{
    public class ProdutoRepository : IProdutoRepository
    {
        private readonly ApplicationDbContext _context;

        public ProdutoRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<Produto> Listar()
        {
            var listaProduto = _context.Produto.ToList();

            return listaProduto;
        }

        public Produto ListarProduto(string id)
        {
            var ProdutoDb = _context.Produto.AsNoTracking().FirstOrDefault(x => x.idProduto == id);
            return ProdutoDb;
        }

        public string Insert(Produto produto)
        {
            _context.Produto.Add(produto);
            _context.SaveChanges();
            return produto.idProduto;
        }

        public void Update(Produto produto)
        {
            _context.Produto.Update(produto);
            _context.SaveChanges();
        }

        public void Delete(string id)
        {
            var ProdutoDb = _context.Produto.FirstOrDefault(x => x.idProduto == id);
            _context.Produto.Remove(ProdutoDb);
            _context.SaveChanges();
        }

        public long ExisteDescricao(Produto produto)
        {
            try
            {
                long qtd;
                if (!string.IsNullOrEmpty(produto.idProduto))
                {
                    qtd = _context.Produto.Count(x =>
                    x.idProduto != produto.idProduto &&
                    x.descricao == produto.descricao);
                }
                else
                {
                    qtd = _context.Produto.Count(x =>
                    x.descricao == produto.descricao);
                }

                return qtd;
            }
            catch (Exception ex)
            {
                string er = ex.Message;
                throw;
            }

        }
    }
}
