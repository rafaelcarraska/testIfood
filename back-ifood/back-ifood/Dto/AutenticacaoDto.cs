using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace ifood_back.Dto
{
    [DataContract]
    public class AutenticacaoDto
    {
        [DataMember]
        public string login { get; set; }

        [DataMember]
        public string senha { get; set; }
    }
}
