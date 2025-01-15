using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OtoServisYonetimSistemi.Entities.Servis
{
    public class Musteri
    {
        [Key]
        public int Id { get; set; }
        [MaxLength(100)]
        [Required(ErrorMessage ="Adı Soyadı / Unvan alanı boş geçilemez")]
        public string AdSoyad { get; set; } //Adı Soyad / Unvan
        [MaxLength(20)]
        [Required(ErrorMessage = "Telefon alanı boş geçilemez")]
        public string Telefon { get; set; }
        [MaxLength(100)]
        public string Eposta { get; set; }
        [MaxLength(500)]
        public string Adres { get; set; }
        public List<IsEmri> IsEmris { get; set; }
    }
}
