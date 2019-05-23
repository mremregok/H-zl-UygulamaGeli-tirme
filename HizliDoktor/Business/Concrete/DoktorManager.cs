using Business.Abstract;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Concrete
{
    public class DoktorManager : IDoktorService
    {
        IDoktorDal doktorDal;
        IRandevuDal randevuDal;

        public DoktorManager(IDoktorDal _doktorDal, IRandevuDal _randevuDal)
        {
            doktorDal = _doktorDal;
            randevuDal = _randevuDal;
        }

        public List<Doktor> TumDoktorlar()
        {
            return doktorDal.GetList();
        }

        public List<Doktor> Doktorlar(int bolumId)
        {
            return doktorDal.GetList(x => x.BolumId == bolumId);
        }

        public void Ekle(Doktor doktor)
        {
            doktorDal.Add(doktor);
        }

        public Doktor Getir(int doktorId)
        {
            Doktor doktor = doktorDal.Get(x => x.Id == doktorId);
            return doktor;
        }

        public Doktor Getir(string TC)
        {
            Doktor doktor = doktorDal.Get(x => x.TC == TC);
            return doktor;
        }

        public void Guncelle(Doktor doktor)
        {
            doktorDal.Update(doktor);
        }

        public Doktor Sil(int doktorId)
        {
            Doktor doktor = doktorDal.Get(x => x.Id == doktorId);
            List<Randevu> randevular = randevuDal.GetList(x => x.DoktorId == doktorId);

            foreach (var item in randevular)
            {
                randevuDal.Delete(item);
            }

            doktorDal.Delete(doktor);

            return doktor;
        }
    }
}
