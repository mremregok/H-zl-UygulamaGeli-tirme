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

        public void Add(Bolum bolum)
        {
            bolumDal.Add(bolum);
        }
    }
}
