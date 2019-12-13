using PokeApiNet;
using PokeApiNet.Models;
using PokeApp.Model;
using PokeApp.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PokeApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DetailPage : ContentPage
    {
        //Form formPoke;
        string namePoke = "";
        int idPoke = 0;

        public DetailPage()
        {
            InitializeComponent();
        }

        public DetailPage(string namePoke)
        {
            InitializeComponent();
            this.namePoke = namePoke;
            setup(namePoke);
        }

        public async void setup(string pokeName)
        {
            if(pokeName != "")
            {
                //RootObject pokemon = await PokeApi.getDadosPokemon(formPoke.url);
                PokeApiClient pokeClient = new PokeApiClient();
                Pokemon pokemon = await pokeClient.GetResourceAsync<Pokemon>(pokeName);

                this.idPoke = pokemon.Id;
                poke_imagem.Source = pokemon.Sprites.FrontDefault; //ImageSource.FromUri(new Uri(pokemon.sprites.front_default));
                poke_name.Text = pokemon.Name.ToUpper();
                poke_weight.Text = pokemon.Weight.ToString();
                poke_height.Text = pokemon.Height.ToString();

                StringBuilder types = new StringBuilder();
                if (pokemon.Types != null)
                {
                    foreach (var item in pokemon.Types)
                    {
                        types.Append(item.Type.Name).Append(" - ");
                    }
                    types.Remove(types.Length - 3, 3);
                    
                }
                else
                    types.Append("-");

                poke_types.Text = types.ToString().ToUpper();
            }
        }

        private void MoreInfo_Clicked(object sender, EventArgs e)
        {
            InformationPage infoPage = new InformationPage(namePoke);
            Navigation.PushAsync(infoPage, true);
        }
    }
}