using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public string Content { get; set; }

        public int? StockId { get; set; }
        // navigation key allowing the comment to be linked to a stock

        public Stock? Stock { get; set; }


    }
}