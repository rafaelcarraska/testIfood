using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace back_ifood.Model
{
    [DataContract]
    public class Produto
    {
        [DataMember]
        [Key]
        public string idProduto { get; set; }

        [DataMember]
        [Required(ErrorMessage = "Campo descrição é obrigatório")]
        public string descricao { get; set; }

        [DataMember]
        [Required(ErrorMessage = "Campo valor é obrigatório")]
        public decimal valor { get; set; }

        [DataMember]
        public string imagem { get; set; }
    }
}
