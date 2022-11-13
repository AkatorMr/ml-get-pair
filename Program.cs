using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json.Linq;

namespace ML_Getpair
{
    class Program
    {
        private static List<Proveedor> lProveedor = new List<Proveedor>();

        //Flags para el control de ejecución
        static bool bDistintoZero = false;
        static bool bStopEnd = false;

        static void Main(string[] real_args)
        {
            if (real_args.Length == 0)
                return;

           List<string> args2 = new List<string>();
            
            foreach (string s in real_args)
            {
                if (s.StartsWith("/z"))
                    bDistintoZero = true;
                else if (s.StartsWith("/s"))
                    bStopEnd = true;
                else
                    args2.Add(s);
            }
            string[] args = args2.ToArray();

            if (args.Length == 0)
                return;


            //cada argumento es un item a buscar
            Console.WriteLine("Paso 0");
            foreach (string s in args)
            {
               

                Curl curl = new Curl("http://api.mercadolibre.com/sites/MLA/search?q=" + s + "&offset=" + 0);
                JObject jo = curl.JO;

                JArray ja = (JArray)jo.SelectToken("results");
                //Console.WriteLine(ja.ToString());

                //IEnumerable<JToken> ijt = ja.SelectTokens("id");
                foreach (JObject j in ja)
                {
                    JToken a = j.SelectToken("seller");
                    String ids = a.SelectToken("id").ToString();

                    bool bAgregar = true;
                    foreach (Proveedor p in lProveedor)
                    {
                        if (p.Id.Equals(ids))
                        {
                            bAgregar = false;
                            break;
                        }
                    }
                    if (bAgregar)
                        lProveedor.Add(new Proveedor(ids));

                    //Console.WriteLine(ids);
                    //break;
                }
                //lProveedor.Add();
                //Console.WriteLine(s);
                
                //Solo tomamos el primer argumento como útil
                //break;
            }



            Console.WriteLine("Paso 1");
            foreach (Proveedor p in lProveedor)
            {
                foreach (String search in args)
                {

                    bool bContiene = false;
                    String id = p.Id;
                    //Console.WriteLine(id);
                    Curl cu = new Curl("https://api.mercadolibre.com/sites/MLA/search?q=" + search + "&seller_id=" + id);

                    JObject jo = cu.JO;

                    JArray ja = (JArray)jo.SelectToken("results");
                    //Console.WriteLine(ja.ToString());

                    //IEnumerable<JToken> ijt = ja.SelectTokens("id");
                    foreach (JObject j in ja)
                    {
                        j.Remove("seller");

                        SellingItem si =
                            new SellingItem(
                                j.SelectToken("title").ToString().ToLower(),
                                j.SelectToken("price").ToString(),
                                j.SelectToken("id").ToString()
                                );
                        /*Console.WriteLine(j.SelectToken("id").ToString());
                        
                        Console.WriteLine(j.ToString());
                        break;
                        */
                        foreach (string que in args)
                        {
                            //Console.Write(si.Titulo);
                            //Console.Write("-");
                            //Console.Write(que);
                            //Console.Write("=");

                            //Console.WriteLine(si.Titulo.Contains(que));

                            if (si.Titulo.Contains(que))
                            {
                                p.Score += si.Precio;

                                if (!bContiene)
                                    p.Share++;

                                bContiene = true;

                            }
                        }

                        p.lSellingItem.Add(si);
                        //Console.WriteLine(j.SelectToken("title"));
                    }
                    //break;
                }
                //break;

            }

            List<Proveedor> sorted = lProveedor.OrderBy(x => x.Share).ToList();

            foreach (Proveedor p in sorted)
            {
                if(p.Score>0 || !bDistintoZero)
                    Console.WriteLine(p.ToString());
            }

            if(bStopEnd)
                Console.ReadLine();
        }
    }
}
