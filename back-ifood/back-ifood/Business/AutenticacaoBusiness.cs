using ifood_back.Dto;
using ifood_back.Interface.IBusiness;
using ifood_back.Security;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Linq;
using System.Security.Claims;

namespace ifood_back.Business
{
    public class AutenticacaoBusiness : IAutenticacaoBusiness
    {
        Result result = new Result();

        private readonly IHttpContextAccessor _httpContext;
        private readonly IJsonWebToken _jsonWebToken;

        public AutenticacaoBusiness(IHttpContextAccessor httpContext, IJsonWebToken jsonWebToken)
        {
            _httpContext = httpContext;
            _jsonWebToken = jsonWebToken;
        }

        public Result validaLogin(AutenticacaoDto autenticacao)
        {
            result = new Result();
            result.erro = string.Empty;
            try
            {
                var client = new RestClient("https://dev.sitemercado.com.br/api/login");
                client.Timeout = -1;
                var request = new RestRequest(Method.POST);
                String encoded = System.Convert.ToBase64String(System.Text.Encoding.GetEncoding("ISO-8859-1").GetBytes(autenticacao.login + ":" + autenticacao.senha));
                request.AddHeader("Authorization", $"Basic {encoded}");
                IRestResponse response = client.Execute(request);


                dynamic data = JsonConvert.DeserializeObject<Object>(response.Content);


                if (data?.success.Value)
                {
                    var usuario = new UsuarioDto() { id = new Guid().ToString(), login = autenticacao.login };

                    if (usuario != null)
                    {
                        result.content = CreateJwt(autenticacao, usuario.id);

                        result.sucesso = true;
                        return result;
                    }

                }


                result.erro = "Usuário ou senha invalida!";
            }
            catch (Exception ex)
            {
                result.exception = ex.Message;
                result.erro = "erro ao realizar a autenticação!";
                throw;
            }

            return result;
        }

        public string UsuarioLogado()
        {
            var usuarioId = _httpContext.HttpContext.User.Claims.Where(c => c.Type == ClaimTypes.GivenName).Select(c => c.Value).SingleOrDefault();
            return usuarioId;
        }

        private string CreateJwt(AutenticacaoDto autenticacao, string usuarioId)
        {
            var sub = autenticacao.login;

            return _jsonWebToken.Encode(sub, usuarioId);
        }
    }
}
