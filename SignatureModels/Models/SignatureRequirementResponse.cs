using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignatureModels.Models
{
    public class SignatureRequirementResponse : SignatureBase
    {
        public string SignatureRequirement { get; set; }
        public void SetRequirements(string _signatureRequirement) => SignatureRequirement = _signatureRequirement;
        public SignatureRequirementResponse(string _plaintiff, string _defendant) : base(_plaintiff, _defendant) { }
    }
}
