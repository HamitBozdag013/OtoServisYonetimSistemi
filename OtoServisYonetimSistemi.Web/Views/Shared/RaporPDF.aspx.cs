using Microsoft.Reporting.WebForms;
using OtoServisYonetimSistemi.BusinessLayer.Concrete;
using OtoServisYonetimSistemi.Entities.Servis;
using OtoServisYonetimSistemi.Entities.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace OtoServisYonetimSistemi.Web.Views.Shared
{
    public partial class RaporPDF : System.Web.Mvc.ViewPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    int isEmriId = Convert.ToInt32(ViewBag.IsEmriId);
                    Repository<Islem> repositoryIslem = new Repository<Islem>();
                    Repository<Iletisim> repositoryIletisim = new Repository<Iletisim>();
                    Repository<IsEmri> repositoryIsEmri = new Repository<IsEmri>();
                    var islemler = repositoryIslem.Get(i => i.IsEmriId == isEmriId).ToList();
                    var detay = repositoryIsEmri.Get(i => i.Id == isEmriId, includeProperties:"Musteri, Model").FirstOrDefault();
                    

                    ReportParameter[] reportParameters = new ReportParameter[12];
                    reportParameters[0] = new ReportParameter("Unvan", repositoryIletisim.Get().FirstOrDefault().Unvan);
                    reportParameters[1] = new ReportParameter("Iletisim", Regex.Replace(repositoryIletisim.Get().FirstOrDefault().IletisimBilgi, "<.*?>", string.Empty));
                    reportParameters[2] = new ReportParameter("Marka", detay.Model.Marka.MarkaAd);
                    reportParameters[3] = new ReportParameter("Model", detay.Model.ModelAd);
                    reportParameters[4] = new ReportParameter("Plaka", detay.Plaka);
                    reportParameters[5] = new ReportParameter("SaseNo", detay.SaseNo);
                    reportParameters[6] = new ReportParameter("AracKm", detay.AracKm.ToString());
                    reportParameters[7] = new ReportParameter("ModelYil", detay.ModelYil.ToString());
                    reportParameters[8] = new ReportParameter("AdSoyad", detay.Musteri.AdSoyad);
                    reportParameters[9] = new ReportParameter("Telefon",detay.Musteri.Telefon);
                    reportParameters[10] = new ReportParameter("OdemeSekli", detay.OdemeSekli);
                    reportParameters[11] = new ReportParameter("AlinanUcret", detay.AlinanUcret.ToString());


                    ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/RaporDizayn/RaporSonuc.rdlc");
                    ReportViewer1.LocalReport.DataSources.Clear();
                    ReportDataSource reportDataSource = new ReportDataSource("DSIslemler", islemler);
                    ReportViewer1.LocalReport.SetParameters(reportParameters);
                    ReportViewer1.LocalReport.DataSources.Add(reportDataSource);
                }
                catch (Exception)
                {

                    throw;
                }
            }

        }
    }
}