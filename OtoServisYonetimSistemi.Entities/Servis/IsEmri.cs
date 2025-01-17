﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OtoServisYonetimSistemi.Entities.Servis
{
    public class IsEmri
    {
        [Key]
        public int Id { get; set; }
        public int MusteriId { get; set; }
        public int ModelId { get; set; }
        public string Plaka { get; set; }
        public string SaseNo { get; set; }
        public int AracKm { get; set; }
        public int ModelYil { get; set; }
        public string YakitTuru { get; set; }
        public DateTime GelisTarihi { get; set; }
        public string GelisSebebi { get; set; }
        public bool Kapali { get; set; }
        public string OdemeSekli { get; set; }
        public string Aciklama { get; set; }
        public decimal AlinanUcret { get; set; }
        public DateTime? BitisTarihi { get; set; }
        public virtual Musteri Musteri { get; set; }
        public virtual Model Model { get; set; }
        public List<Islem> Islems { get; set; }
    }
}
