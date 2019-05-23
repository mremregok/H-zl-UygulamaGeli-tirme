using Autofac;
using Business.Abstract;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace Business.Concrete
{
    public class RandevuManager : IRandevuService
    {
        IRandevuDal randevuDal;

        public RandevuManager(IRandevuDal _randevuDal)
        {
            randevuDal = _randevuDal;
        }

        public bool Ekle(Randevu randevu)
        {
            List<Randevu> randevular = randevuDal.GetList(x => x.DoktorId == randevu.DoktorId);

            bool zatenVar = randevular.Exists(x => x.Tarih == randevu.Tarih);

            if (!zatenVar) { randevuDal.Add(randevu); return true; }
            else return false;
        }

        public Randevu Sil(int randevuId)
        {
            Randevu randevu = randevuDal.Get(x => x.Id == randevuId);
            randevuDal.Delete(randevu);

            return randevu;
        }

        public List<Randevu> BolumRandevulari(int bolumId)
        {
            List<Randevu> randevular = randevuDal.GetList(x => x.BolumId == bolumId && x.HastaId != 0);
            randevular = randevular.OrderBy(x => x.Tarih).ToList();
            return randevular;
        }

        public List<Randevu> DoktorRandevulari(int doktorId)
        {
            List<Randevu> randevular = randevuDal.GetList(x => x.DoktorId == doktorId && x.Tarih.Value < DateTime.Now.AddMonths(1) && x.HastaId != 0);
            randevular = randevular.OrderBy(x => x.Tarih).ToList();
            return randevular;
        }

        public List<Randevu> HastaRandevulari(int hastaId)
        {
            List<Randevu> randevular = randevuDal.GetList(x => x.HastaId == hastaId && x.HastaId != 0);
            return randevular;
        }

        public List<Randevu> TumRandevular(int hastaneId)
        {
            List<Randevu> randevular = randevuDal.GetList(x => x.HastaneId == hastaneId && x.HastaId != 0);
            randevular = randevular.OrderBy(x => x.Tarih).ToList();
            return randevular;
        }

        public List<DateTime> MusaitTarihleriGetir(int doktorId, DateTime gun)
        {
            List<Randevu> randevular = randevuDal.GetList(x => x.DoktorId == doktorId);

            List<DateTime> musaitTarihler = new List<DateTime>();

            DateTime dateTime = gun;

            for (int i = 0; i < 18; i++)
            {
                dateTime = dateTime.AddMinutes(20);
                DateTime tempDate = dateTime;

                if (randevular.SingleOrDefault(x => x.Tarih.Value == dateTime) != null)//eğer randevu zaten varsa
                    tempDate = dateTime.AddSeconds(13);

                musaitTarihler.Add(tempDate);
            }

            return musaitTarihler;
        }

        public void RandevuMailiGonder(string hastaMail, string mesaj)
        {
            SmtpClient client = new SmtpClient("smtp.live.com", 587);
            client.UseDefaultCredentials = false;
            client.EnableSsl = true;
            client.Credentials = new NetworkCredential("hizlidoktor00@outlook.com", "1236987450Hd.");

            MailMessage mailMessage = new MailMessage();
            mailMessage.From = new MailAddress("hizlidoktor00@outlook.com");
            mailMessage.To.Add(hastaMail);
            mailMessage.Body = mesaj;
            mailMessage.Subject = "Hızlı Doktor - Randevu bilgilendirme";
            client.Send(mailMessage);
        }

        public Randevu Getir(DateTime randevuTarihi)
        {
            Randevu randevu = randevuDal.Get(x => x.Tarih.Value == randevuTarihi);
            return randevu;
        }
    }
}
