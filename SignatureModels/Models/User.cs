using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SignatureModels.Models
{
    public class User
    {
        public string Sign { get; set; }
        public int Rating { get; set; }
        public User() { }
        public User(string _sign) => Sign = _sign;
        public void SetRating(int _rating) => Rating = _rating;
        public string GetSign() => Sign;
    }
}
