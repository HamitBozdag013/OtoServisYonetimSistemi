using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OtoServisYonetimSistemi.Entities.Web
{
    public class Slider
    {
        [Key]
        public int Id { get; set; }
        [MaxLength(100)]
        public string Baslik { get; set; }
        [MaxLength(500)]
        public string Tanimlama { get; set; }
        public string ResimYolu { get; set; }
    }
}
