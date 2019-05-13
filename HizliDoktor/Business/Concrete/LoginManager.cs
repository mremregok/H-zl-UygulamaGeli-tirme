using Autofac;
using Business.Abstract;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace Business.Concrete
{
    public class LoginManager : ILoginService
    {
        IHastaDal hastaDal;
        IDoktorDal doktorDal;

        public LoginManager(IHastaDal _hastaDal, IDoktorDal _doktorDal)
        {
            hastaDal = _hastaDal;
            doktorDal = _doktorDal;
        }

        public string DogrulamaMailiGonder(string mail)
        {
            Random rastgele = new Random();

            SmtpClient client = new SmtpClient("smtp.live.com", 587);
            client.UseDefaultCredentials = false;
            client.EnableSsl = true;
            client.Credentials = new NetworkCredential("hizlidoktor00@outlook.com", "1236987450Hd.");

            string onayKodu = rastgele.Next(100000, 999999).ToString();
            string message = "Merhaba " + mail + System.Environment.NewLine + "Onay Kodunuz: " + onayKodu;

            MailMessage mailMessage = new MailMessage();
            mailMessage.From = new MailAddress("hizlidoktor00@outlook.com");
            mailMessage.To.Add(mail);
            mailMessage.Body = message;
            mailMessage.Subject = "Hızlı Doktor Onay Kodu";
            client.Send(mailMessage);

            return onayKodu;
        }

        public bool GirisYap(string TC, string sifre, bool yoneticiGirisi)
        {
            if (yoneticiGirisi && TC == "1" && sifre == "admin") return true;

            if (yoneticiGirisi)
            {
                Doktor doktor = doktorDal.Get(x => x.TC == TC);

                return (doktor != null && doktor.Sifre == sifre);
            }
            else
            {
                Hasta hasta = hastaDal.Get(x => x.TC == TC);

                return (hasta != null && hasta.Sifre == sifre);
            }
        }

        public string MailDogrula(string mail, string kod, string girilenKod)
        {
            if (kod == girilenKod)
            {
                Hasta hasta = hastaDal.Get(x => x.Mail == mail);
                hasta.IsMailVerified = true;
                hastaDal.Update(hasta);
                return hasta.TC;
            }
            else return string.Empty;
        }

        public bool UyeOl(Hasta hasta)
        {
            Hasta bulunanHasta = hastaDal.Get(x => x.TC == hasta.TC);
            if (bulunanHasta!=null)  return false;

            bulunanHasta = hastaDal.Get(x => x.Mail == hasta.Mail);
            if (bulunanHasta != null) return false;
           
            hastaDal.Add(hasta);
            return true;
        }
    }
}
