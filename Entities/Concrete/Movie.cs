using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public class Movie
    {
        public bool Adult { get; set; }
        [Key]
        public long Id { get; set; }
        public string? original_title { get; set; }
        public double? Popularity { get; set; }
        public bool Video { get; set; }
    }
}
