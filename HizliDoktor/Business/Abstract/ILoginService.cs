using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstract
{
    public interface ILoginService
    {
        bool GirisYap(string TC, string password);
        bool UyeOl(Hasta hasta);
    }
}
