using Autofac;
using Business.Abstract;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
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
            List<Randevu> randevular = randevuDal.GetList(x => x.BolumId == bolumId);
            return randevular;
        }

        public List<Randevu> DoktorRandevulari(int doktorId)
        {
            List<Randevu> randevular = randevuDal.GetList(x => x.DoktorId == doktorId);
            return randevular;
        }

        public List<Randevu> HastaRandevulari(int hastaId)
        {
            List<Randevu> randevular = randevuDal.GetList(x => x.HastaId == hastaId);
            return randevular;
        }

        public List<Randevu> TumRandevular(int hastaneId)
        {
            List<Randevu> randevular = randevuDal.GetList(x => x.HastaneId == hastaneId);
            return randevular;
        }

        public List<DateTime> MusaitTarihleriGetir(int doktorId, DateTime gun)
        {
            List<Randevu> randevular = randevuDal.GetList(x => x.DoktorId == doktorId);

            List<DateTime> musaitTarihler = new List<DateTime>();

            //totalde 9 saat çalışılacak, her randevu 30 dakika
            for (int i = 0; i < 18; i++)
            {
                DateTime dateTime = gun;
                dateTime.AddMinutes(30);
                musaitTarihler.Add(dateTime);
            }

            //musait tarihlerden randevu olan tarihleri çıkartalım.
            foreach (Randevu randevu in randevular)
            {
                DateTime varOlanRandevu = musaitTarihler.SingleOrDefault(x => x == randevu.Tarih);

                if (varOlanRandevu != null) musaitTarihler.Remove(varOlanRandevu);
            }

            return musaitTarihler;
        }
    }
}
