using OtoServisYonetimSistemi.Entities.Servis;
using OtoServisYonetimSistemi.Entities.Web;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OtoServisYonetimSistemi.DataAccessLayer
{
    public class DataContext:DbContext
    {
        public DbSet<Slider> Sliders { get; set; }
        public DbSet<Kampanya> Kampanyas { get; set; }
        public DbSet<Uygulama> Uygulamas { get; set; }
        public DbSet<Hakkimizda> Hakkimizdas { get; set; }
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<Iletisim> Iletisims { get; set; } 
        public DbSet<Musteri> Musteris { get; set; }
        public DbSet<Marka> Markas { get; set; }
        public DbSet<IsEmri> IsEmris { get; set; }
        public DbSet<Bakim> Bakims { get; set; }
        public DbSet<BakimGrup> BakimGrups { get; set; }
        public DbSet<Islem> Islems { get; set; }

    }
}
