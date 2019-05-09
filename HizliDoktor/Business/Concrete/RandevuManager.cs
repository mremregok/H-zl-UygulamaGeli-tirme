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

        public RandevuManager()
        {
           randevuDal = IOCUtil.Container.Resolve<IRandevuDal>();
        }

        public bool Ekle(Randevu randevu)
        {
            //sorgulamaları yap

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

        public List<DateTime> MusaitTarihleriGetir(int doktorId)
        {
            List<Randevu> randevular = randevuDal.GetList(x => x.DoktorId == doktorId);

            List<DateTime> musaitTarihler = new List<DateTime>();

            //totalde 8 saat çalışılacak, her randevu 30 dakika
            for (int i = 0; i < 16; i++)
            {

            }

            foreach (Randevu randevu in randevular)
            {

            }

            return musaitTarihler;

        }
    }
}
