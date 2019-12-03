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


        public static List<Form> listaPokemons()
        {
            List<Form> lstPokemon = new List<Form>();
            string consulta =  string.Format("{0}/{1}", URL_BASE, "/pokemon");

            WebClient client = new WebClient();
            try
            {
                var content = client.DownloadString(consulta);
                var result = JsonConvert.DeserializeObject<RootObject>(content);

                lstPokemon = result.results;
            }
            catch
            {

            }

            return lstPokemon;
        }


        public static async Task<List<Form>> buscarPokemon(string name)
        {
            List<Form> lstPokemon = new List<Form>();

            if (name.Trim() != "")
            {
                string consulta = string.Format("{0}/{1}/{2}", URL_BASE, "/pokemon", name);

                //WebClient client = new WebClient();
                HttpClient client = new HttpClient();
                try
                {
                    //var content = client.DownloadString(consulta);
                    var content = await client.GetStringAsync(consulta);
                    Form result = JsonConvert.DeserializeObject<Form>(content);

                    lstPokemon.Add(result);
                }
                catch
                {

                }
            }
            
            return lstPokemon;
        }

    }
}
