using Business.Abstract;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Concrete
{
    public class BolumManager : IBolumService
    {
        IBolumDal bolumDal;
        IDoktorDal doktorDal;
        IRandevuDal randevuDal;

        public BolumManager(IBolumDal _bolumDal, IDoktorDal _doktorDal, IRandevuDal _randevuDal)
        {
            bolumDal = _bolumDal;
            doktorDal = _doktorDal;
            randevuDal = _randevuDal;
        }

        public List<Bolum> Bolumler(int hastaneId)
        {
            return bolumDal.GetList(x => x.HastaneId == hastaneId);
        }

        public void Ekle(Bolum bolum)
        {
            bolumDal.Add(bolum);
        }

        public Bolum Getir(int bolumId)
        {
            Bolum bolum = bolumDal.Get(x => x.Id == bolumId);
            return bolum;
        }

        public void Guncelle(Bolum bolum)
        {
            bolumDal.Update(bolum);
        }

        public Bolum Sil(int bolumId)
        {
            Bolum bolum = bolumDal.Get(x => x.Id == bolumId);
            List<Doktor> doktorlar = doktorDal.GetList(x => x.BolumId == bolumId);
            List<Randevu> randevular = randevuDal.GetList(x => x.BolumId == bolumId);

            foreach (var item in doktorlar)
            {
                doktorDal.Delete(item);
            }

            foreach (var item in randevular)
            {
                randevuDal.Delete(item);
            }

            bolumDal.Delete(bolum);

            return bolum;
        }
    }
}
