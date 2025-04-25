using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Models
{
    public class Stock
    {
        public int Id { get; set; }
        public double Price { get; set; }
        public string Industry { get; set; } = string.Empty;

        public List<Comment> Comments { get; set; } = new List<Comment>();

    }
}