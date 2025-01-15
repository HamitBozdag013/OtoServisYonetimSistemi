using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OtoServisYonetimSistemi.Entities.Web
{
    public class Uygulama
    {
        [Key]
        public int Id { get; set; }
        [MaxLength(100)]
        public string Baslik { get; set; }
        public string ResimYolu { get; set; }
    }
}
