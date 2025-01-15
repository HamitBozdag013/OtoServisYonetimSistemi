using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace OtoServisYonetimSistemi.Entities.Web
{
    public class Blog
    {
        [Key]
        public int Id { get; set; }
        [MaxLength(100)]
        public string Baslik { get; set; }
        [AllowHtml]
        [UIHint("tinymce_full_compressed")]
        public string Icerik { get; set; }
        public DateTime Tarih { get; set; }
    }
}
