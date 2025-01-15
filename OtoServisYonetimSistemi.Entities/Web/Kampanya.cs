using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace OtoServisYonetimSistemi.Entities.Web
{
    public class Kampanya
    {
        [Key]
        public int Id { get; set; }
        [AllowHtml]
        [UIHint("tinymce_full_compressed")]
        public string Icerik { get; set; }
    }
}
