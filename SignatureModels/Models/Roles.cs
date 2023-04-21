using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignatureModels.Models
{
    public class Roles
    {
        public RolesDefinition King { get; set; }
        public RolesDefinition Notary { get; set; }
        public RolesDefinition Validator { get; set; }
    }
}
