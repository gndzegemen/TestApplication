using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Models
{
    public class Test
    {
        public int TestId { get; set; }
        public string Type { get; set; }
        public ICollection<Result> Results { get; set; }
    }
}
