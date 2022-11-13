using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ML_Getpair
{
    class SellingItem
    {
        public string Titulo { get; set; }
        public decimal Precio { get; set; }
        public string Id { get; set; }

        public SellingItem(String title,String Precio, String Id)
        {
            this.Titulo = title;
            decimal d = 0;
            Decimal.TryParse(Precio, out d);
            this.Precio = d;
            this.Id = Id;
        }
    }
}
