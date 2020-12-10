using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace ifood_back.Dto
{
    [DataContract]
    public class Result
    {
        [DataMember]
        public string erro { get; set; }

        public string exception { get; set; }

        [DataMember]
        public bool sucesso { get; set; }

        [DataMember]
        public Object content { get; set; }
    }
}
