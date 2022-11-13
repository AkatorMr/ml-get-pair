using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ML_Getpair
{
    class Proveedor
    {
        public string Id { get;set; }

        public List<SellingItem> lSellingItem = new List<SellingItem>();

        public decimal Score = 0;
        public int Share { get; set; }
        public Proveedor(String id)
        {
            this.Id = id;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("Vendedor ID: ");
            sb.Append(Id);
            
           
            sb.Append("   ");
            sb.Append(Share);
            sb.Append("\n");
            foreach (SellingItem si in lSellingItem)
            {
                sb.Append("\tIdentificador:");
                sb.Append(si.Id);
                sb.Append("\t");
                sb.Append(si.Titulo);
                sb.Append("\t");
                sb.Append(si.Precio);
                sb.Append("\n");
            }
            return sb.ToString();
        }
    }
}
