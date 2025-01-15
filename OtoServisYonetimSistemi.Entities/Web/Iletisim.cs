using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace OtoServisYonetimSistemi.Entities.Web
{
    public class Iletisim
    {
        [Key]
        public int Id { get; set; }
        public string Harita { get; set; }
        [AllowHtml]
        [UIHint("tinymce_full_compressed")]
        public string IletisimBilgi { get; set; }
        public string Unvan { get; set; }
    }
}
