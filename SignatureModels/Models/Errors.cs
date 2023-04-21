using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignatureModels.Models
{
    public class Errors
    {
        public string MissingHashtag { get; set; }
        public string BeginResponseRequirements { get; set; }
        public string HigherSignature { get; set; }
        public string UnknownSignature { get; set; }
    }
}
