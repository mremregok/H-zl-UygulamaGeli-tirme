using Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Concrete
{
    public class Admin : IEntity
    {
        public int Id { get; set; }
        public string Ad { get; set; }
    }
}
