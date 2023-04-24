using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignatureModels.Models
{
    public class SignatureGroup
    {
        public User Plaintiff { get; set; }
        public User Defendant { get; set; }
        public SignatureGroup() { }
        public SignatureGroup(string _plaintiff, string _defendant)
        {
            Plaintiff = new User(_plaintiff);
            Defendant = new User(_defendant);
        }
        public void SetPlaintiffRating(int rating) => Plaintiff.SetRating(rating);
        public void SetDefendantRating(int rating) => Defendant.SetRating(rating);
        public User GetPlaintiff() => Plaintiff;
        public User GetDefendant() => Defendant;
        
    }
}
