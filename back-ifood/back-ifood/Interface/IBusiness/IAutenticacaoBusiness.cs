using ifood_back.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ifood_back.Interface.IBusiness
{
    public interface IAutenticacaoBusiness
    {
        Result validaLogin(AutenticacaoDto autenticacao);

        string UsuarioLogado();
    }
}
