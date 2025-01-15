using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OtoServisYonetimSistemi.Web.Custom
{
    public static class Ozel
    {
        public static string LinkDuzenle(string _Text)
        {
            try
            {
                string Text = _Text.Trim();
                Text = Text.Replace('ğ', 'g');
                Text = Text.Replace('Ğ', 'G');
                Text = Text.Replace('ü', 'u');
                Text = Text.Replace('Ü', 'U');
                Text = Text.Replace('ş', 's');
                Text = Text.Replace('Ş', 'S');
                Text = Text.Replace('ı', 'i');
                Text = Text.Replace('İ', 'I');
                Text = Text.Replace('ö', 'o');
                Text = Text.Replace('Ö', 'O');
                Text = Text.Replace('ç', 'c');
                Text = Text.Replace('Ç', 'C');
                Text = Text.Replace('-', '+');
                Text = Text.Replace(' ', '+');
                Text = Text.Trim();
                Text = new System.Text.RegularExpressions.Regex("[^a-zA-z0-9+]").Replace(Text,"");
                Text = Text.Trim();
                Text = Text.Replace('+', '-');
                return Text;
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

    }
}