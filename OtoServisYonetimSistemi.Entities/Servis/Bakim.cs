using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OtoServisYonetimSistemi.Entities.Servis
{
    public class Bakim
    {
        [Key]
        public int Id { get; set; }
        public int BakimGrupId { get; set; }
        public string BakimAdi { get; set; }
        public virtual BakimGrup BakimGrup { get; set; }
    }
}
