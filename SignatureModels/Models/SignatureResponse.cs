using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignatureModels.Models
{
    public class SignatureResponse: SignatureBase
    {
        public string Winner { get; set; }
        public void SetWinner(string _winner) => Winner = _winner;
        public SignatureResponse(){}
        public SignatureResponse(string _plaintiff, string _defendant) : base(_plaintiff, _defendant) { }
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
