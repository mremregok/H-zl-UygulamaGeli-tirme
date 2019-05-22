using Business.Abstract;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Concrete
{
    public class HastaManager : IHastaService
    {
        IHastaDal hastaDal;
        IRandevuDal randevuDal;

        public HastaManager(IHastaDal _hastaDal, IRandevuDal _randevuDal)
        {
            hastaDal = _hastaDal;
            randevuDal = _randevuDal;
        }

        public void Ekle(Hasta hasta)
        {
            hastaDal.Add(hasta);
        }

        public Hasta Getir(string TC)
        {
            Hasta hasta = hastaDal.Get(x => x.TC == TC);
            return hasta;
        }

        public Hasta Getir(int hastaId)
        {
            Hasta hasta = hastaDal.Get(x => x.Id == hastaId);
            return hasta;
        }

        public void Guncelle(Hasta hasta)
        {
            hastaDal.Update(hasta);
        }

        public bool RandevuVarMi(int hastaId, int doktorId, DateTime tarih)
        {
            Hasta hasta = Getir(hastaId);
            Randevu randevu  = randevuDal.Get(x => x.HastaId == hastaId && x.DoktorId == doktorId && x.Tarih.Value.ToLongDateString() == tarih.ToLongDateString());

            if (randevu == null) return false;
            else return true;
        }

        public Hasta Sil(int hastaId)
        {
            Hasta hasta = hastaDal.Get(x => x.Id == hastaId);
            hastaDal.Delete(hasta);
            return hasta;
        }

    }
}
