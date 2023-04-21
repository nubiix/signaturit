using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignatureModels.Models
{
    public class SignatureBase
    {
        public SignatureGroup ImplicatedParts { get; set; }
        public SignatureBase(string _plaintiff, string _defendant) => ImplicatedParts = new SignatureGroup(_plaintiff, _defendant);
        public void setRatingImplicatedParts(int ratingPlaintinff, int ratingDefendant)
        {
            if (ImplicatedParts != null)
            {
                ImplicatedParts.SetPlaintiffRating(ratingPlaintinff);
                ImplicatedParts.SetDefendantRating(ratingDefendant);
            }
        }
        public SignatureGroup GetImplicatedParts() => ImplicatedParts;
    }
}
