using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstract
{
    public interface IFavoriService
    {
        void Ekle(Favori favori);
        Favori Sil(int favoriId);
        Favori Getir(int favoriId);
        Favori Getir(int hastaId, int doktorId);
        List<Favori> Favoriler(int hastaId);
    }
}
