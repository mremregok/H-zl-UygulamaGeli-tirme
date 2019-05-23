using Autofac;
using Business.Abstract;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Concrete
{
    public class HastaneManager : IHastaneService
    {
        IHastaneDal hastaneDal;
        IBolumDal bolumDal;
        IDoktorDal doktorDal;
        IRandevuDal randevuDal;

        public HastaneManager(IHastaneDal _hastaneDal, IBolumDal _bolumDal, IDoktorDal _doktorDal, IRandevuDal _randevuDal)
        {
            hastaneDal = _hastaneDal;
            bolumDal = _bolumDal;
            doktorDal = _doktorDal;
            randevuDal = _randevuDal;
        }

        public void Ekle(Hastane hastane)
        {
            hastaneDal.Add(hastane);
        }

        public Hastane Getir(int hastaneId)
        {
            return hastaneDal.Get(x => x.Id == hastaneId);
        }

        public void Guncelle(Hastane hastane)
        {
            hastaneDal.Update(hastane);
        }

        public List<Hastane> Hastaneler(string il, string ilce)
        {
            return hastaneDal.GetList(x => x.Il == il && x.Ilce == ilce);
        }

        public List<string> HastaneOlanIlceler(string il)
        {
            List<Hastane> hastaneler = hastaneDal.GetList(x => x.Il == il);

            List<string> ilceler = new List<string>();
            foreach (Hastane hastane in hastaneler)
            {
                if (!ilceler.Exists(x => x == hastane.Ilce)) ilceler.Add(hastane.Ilce);
            }

            return ilceler;
        }

        public List<string> HastaneOlanIller()
        {
            List<Hastane> hastaneler = hastaneDal.GetList();

            List<string> iller = new List<string>();
            foreach (Hastane hastane in hastaneler)
            {
                if(!iller.Exists(x => x == hastane.Il)) iller.Add(hastane.Il);
            }

            return iller;
        }

        public Hastane Sil(int hastaneId)
        {
            Hastane hastane = hastaneDal.Get(x => x.Id == hastaneId);
            List<Bolum> bolumler = bolumDal.GetList(x => x.HastaneId == hastaneId);
            List<Doktor> doktorlar = doktorDal.GetList(x => x.HastaneId == hastaneId);
            List<Randevu> randevular = randevuDal.GetList(x => x.HastaneId == hastaneId);

            foreach (var item in bolumler)
            {
                bolumDal.Delete(item);
            }

            foreach (var item in doktorlar)
            {
                doktorDal.Delete(item);
            }

            foreach (var item in randevular)
            {
                randevuDal.Delete(item);
            }

            hastaneDal.Delete(hastane);

            return hastane;
        }

        public List<Hastane> TumHastaneler()
        {
            return hastaneDal.GetList();
        }
    }
}
