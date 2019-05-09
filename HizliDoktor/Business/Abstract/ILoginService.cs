using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstract
{
    public interface ILoginService
    {
        bool Login(string TC, string password);
        
    }
}
