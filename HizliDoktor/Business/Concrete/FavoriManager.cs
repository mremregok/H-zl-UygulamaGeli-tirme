using Autofac;
using Business.Abstract;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Concrete
{
    public class FavoriManager : IFavoriService
    {
        IFavoriDal favoriDal;

        public FavoriManager(IFavoriDal _favoriDal)
        {
            favoriDal = _favoriDal;
        }

        public void Ekle(Favori favori)
        {
            favoriDal.Add(favori);
        }

        public List<Favori> Favoriler()
        {
            List<Favori> favoriler = favoriDal.GetList();

            return favoriler;
        }

        public Favori Getir(int favoriId)
        {
            Favori favori = favoriDal.Get(x => x.Id == favoriId);
            return favori;
        }

        public Favori Sil(int favoriId)
        {
            Favori favori = Getir(favoriId);
            favoriDal.Delete(favori);
            return favori;
        }
    }
}
