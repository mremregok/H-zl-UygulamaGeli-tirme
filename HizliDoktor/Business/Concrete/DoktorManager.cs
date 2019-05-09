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

        public DoktorManager(IDoktorDal _doktorDal)
        {
            doktorDal = _doktorDal;
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

        public void Guncelle(Doktor doktor)
        {
            doktorDal.Update(doktor);
        }

        public Doktor Sil(int doktorId)
        {
            Doktor doktor = doktorDal.Get(x => x.Id == doktorId);
            doktorDal.Delete(doktor);

            return doktor;
        }
    }
}
