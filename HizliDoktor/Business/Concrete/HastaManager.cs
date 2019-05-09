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

        public HastaManager(IHastaDal _hastaDal)
        {
            hastaDal = _hastaDal;
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

        public void Guncelle(Hasta hasta)
        {
            hastaDal.Update(hasta);
        }

        public Hasta Sil(int hastaId)
        {
            Hasta hasta = hastaDal.Get(x => x.Id == hastaId);
            hastaDal.Delete(hasta);
            return hasta;
        }
    }
}
