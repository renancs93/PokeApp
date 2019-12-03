using Newtonsoft.Json;
using PokeApp.Model;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;


namespace PokeApp.Service
{
    class PokeApi
    {
        private static String URL_BASE = "https://pokeapi.co/api/v2";


        public static List<Pokemon> listaPokemons(string name = null)
        {
            List<Pokemon> lstPokemon = new List<Pokemon>();

            string consulta =  string.Format("{0}/{1}", URL_BASE, "/pokemon");

            if (name != null)
                consulta = string.Format("{0}/{1}", consulta, name);

            WebClient client = new WebClient();

            try
            {
                var content = client.DownloadString(consulta);
                var result = JsonConvert.DeserializeObject<RootObject>(content);


                lstPokemon = result.Results;
            }
            catch
            {

            }

            
            return lstPokemon;
        }

    }
}
