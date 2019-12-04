using Newtonsoft.Json;
using PokeApp.Model;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace PokeApp.Service
{
    class PokeApi
    {
        private static String URL_BASE = "https://pokeapi.co/api/v2";


        //public static List<Form> listaPokemons()
        //{
        //    List<Form> lstPokemon = new List<Form>();
        //    string consulta =  string.Format("{0}/{1}", URL_BASE, "/pokemon");

        //    WebClient client = new WebClient();
        //    try
        //    {
        //        var content = client.DownloadString(consulta);
        //        var result = JsonConvert.DeserializeObject<RootObject>(content);

        //        lstPokemon = result.results;
        //    }
        //    catch
        //    {

        //    }

        //    return lstPokemon;
        //}

        public static async Task<RootObject> listaPokemons()
        {
            //List<Form> lstPokemon = new List<Form>();
            RootObject ret = new RootObject();
            string consulta = string.Format("{0}/{1}", URL_BASE, "pokemon");

            //WebClient client = new WebClient();
            HttpClient client = new HttpClient();
            try
            {
                //var content = client.DownloadString(consulta);
                var content = await client.GetStringAsync(consulta);
                ret = JsonConvert.DeserializeObject<RootObject>(content);
            }
            catch
            {

            }

            return ret;
        }


        public static async Task<RootObject> buscarPokemon(string name)
        {
            RootObject ret = new RootObject();

            if (name.Trim() != "")
            {
                string consulta = string.Format("{0}/{1}/{2}", URL_BASE, "pokemon", name);

                //WebClient client = new WebClient();
                HttpClient client = new HttpClient();
                try
                {
                    //var content = client.DownloadString(consulta);
                    var content = await client.GetStringAsync(consulta);
                    ret = JsonConvert.DeserializeObject<RootObject>(content);
                }
                catch
                {

                }
            }
            
            return ret;
        }

        public static async Task<RootObject> getDadosPokemon(string url)
        {
            RootObject ret = new RootObject();

            if (url.Trim() != "")
            {
                string consulta = url;

                HttpClient client = new HttpClient();
                try
                {
                    var content = await client.GetStringAsync(consulta);
                    ret = JsonConvert.DeserializeObject<RootObject>(content);
                }
                catch
                {

                }
            }

            return ret;
        }


    }
}
