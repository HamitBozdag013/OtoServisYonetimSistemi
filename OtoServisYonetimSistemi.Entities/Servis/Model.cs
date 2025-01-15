using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OtoServisYonetimSistemi.Entities.Servis
{
    public class Model
    {
        [Key]
        public int Id { get; set; }
        public string ModelAd { get; set; }
        public int MarkaId { get; set; }
        public bool Silindi { get; set; }
        public virtual Marka Marka { get; set; }
        public List<IsEmri> IsEmris { get; set; }
    }
}
