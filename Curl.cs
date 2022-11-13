using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json.Linq;
using System.Net;
using System.IO;



namespace ML_Getpair
{
    class Curl
    {
        public JObject JO;

        public Curl(String url)
        {

            WebClient wb = new WebClient();

            String myText = wb.DownloadString(url);
            

            JO = JObject.Parse(myText);
            
           
        }
    }
}
