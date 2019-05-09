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

        public BolumManager(IBolumDal _bolumDal)
        {
            bolumDal = _bolumDal;
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

            bolumDal.Delete(bolum);

            return bolum;
        }
    }
}
