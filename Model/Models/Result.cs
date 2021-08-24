using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Models
{
    public class Result
    {
        public int ResultId { get; set; }

        [ForeignKey("HotelTest")]
        public int HotelTestId { get; set; }
        public HotelTest HotelTest { get; set; }

        public bool Output { get; set; }

    }
}
