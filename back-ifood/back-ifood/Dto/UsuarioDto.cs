using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace ifood_back.Dto
{
    [DataContract]
    public class UsuarioDto
    {
        public string id { get; set; }
        public string login { get; set; }
    }
}
