using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstract
{
    public interface IBolumService
    {
        void Ekle(Bolum bolum);
        Bolum Sil(int bolumId);
        void Guncelle(Bolum bolum);
        Bolum Getir(int bolumId);
        List<Bolum> Bolumler(int hastaneId);
    }
}
