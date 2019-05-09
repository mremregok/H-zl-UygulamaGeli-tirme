using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstract
{
    public interface IHastaneService
    {
        List<string> HastaneOlanIller();
        List<string> HastaneOlanIlceler(string il);
        List<Hastane> TumHastaneler();
        List<Hastane> Hastaneler(string il, string ilce);
        Hastane Getir(int hastaneId);
        void Ekle(Hastane hastane);
        Hastane Sil(int hastaneId);
        void Guncelle(Hastane hastane);
    }
}
