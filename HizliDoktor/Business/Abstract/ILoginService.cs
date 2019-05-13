using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstract
{
    public interface ILoginService
    {
        bool GirisYap(string TC, string sifre, bool yoneticiGirisi);
        bool UyeOl(Hasta hasta);
        string DogrulamaMailiGonder(string mail);
        string MailDogrula(string mail, string kod, string girilenKod);
    }
}
