using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignatureModels.Models
{
    public class SignatureRequest
    {
        public string plaintiff { get; set; }
        public string defendant { get; set; }
        public SignatureRequest(string _plaintiff, string _defendant)
        {
            plaintiff = _plaintiff;
            defendant = _defendant;
        }

    }
}
