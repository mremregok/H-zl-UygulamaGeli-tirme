using Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Concrete
{
    public class Hasta : Kisi, IEntity
    {
        public string Mail { get; set; }
        public bool IsMailVerified { get; set; }
    }
}
