using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Xml.Linq;
using Newtonsoft.Json;
using static System.Net.Http.HttpClient;

namespace Hes2
{
    class Program
    {
        static void Main(string[] args)
        {

            string baglanti = "https://api.saglikbakanligi.gov.tr/HES/dogrula";

            var data = HttpHelper.GetDataFromApi<List<Urun>>(baglanti).Result;
            // Console.Read();
            WebRequest istek = HttpWebRequest.Create(baglanti);
            WebResponse cevap;

            cevap = istek.GetResponse();

            StreamReader donenBilgi = new StreamReader(cevap.GetResponseStream());

            string bilgiAl = donenBilgi.ReadToEnd().Trim();

            List<Urun> urunBilgisi = JsonConvert.DeserializeObject<List<Urun>>(bilgiAl);
            // List<Urun> urunBilgisi = Newtonsoft.Json.JsonConvert.DeserializeObject<Urun>(bilgiAl);
            ////JsonConvert.DeserializeObject
            // //XDocument gorest = XDocument.Load(connection);

            //var id = urunBilgisi.Descendants("id").ElementAt(0);
            // for (int i = 0; i < items[0].)
            // var items = JsonConvert.DeserializeObject<List<Urun>>(bilgiAl);
            Console.WriteLine("Riskli Grup:");
            foreach (var item in urunBilgisi)
            {

                if (item.status == "risky")
                {
                    Console.WriteLine(item.hes);
                }
            }
            Console.WriteLine("Risksiz Grup:");
            foreach (var item in urunBilgisi)
            {

                if (item.status == "riskless")
                {
                    Console.WriteLine(item.hes);
                }
            }

            //Console.WriteLine(bilgiAl.ToString());

           
            Console.ReadLine();

            //List<Urun> urun = bilgiAl.Select(c => new Urun
            //{
            //    id = c.ToString()
            //    if (id )
            //}).ToList();


        }
        public class HttpHelper
        {
            public static async Task<T> GetDataFromApi<T>(string url)
            {
                using (var client = new System.Net.Http.HttpClient())
                {
                    var result = await client.GetAsync(url);
                    result.EnsureSuccessStatusCode();
                    string resultContentString = await result.Content.ReadAsStringAsync();
                    T resultContent = JsonConvert.DeserializeObject<T>(resultContentString);
                    return resultContent;
                }
            }
        }
    }
}
