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
        Form formPoke;

        public DetailPage()
        {
            InitializeComponent();
        }

        public DetailPage(Form form)
        {
            InitializeComponent();

            formPoke = form;

            setup(formPoke);
        }

        public async void setup(Form formPoke)
        {
            if(formPoke != null)
            {
                RootObject pokemon = await PokeApi.getDadosPokemon(formPoke.url);

                poke_imagem.Source = pokemon.sprites.front_default; //ImageSource.FromUri(new Uri(pokemon.sprites.front_default));
                poke_name.Text = pokemon.name.ToUpper();
                poke_weight.Text = pokemon.weight.ToString();
                poke_height.Text = pokemon.height.ToString();

                StringBuilder types = new StringBuilder();
                if (pokemon.types != null)
                {
                    foreach (var item in pokemon.types)
                    {
                        types.Append(item.type.name).Append(" - ");
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
            InformationPage infoPage = new InformationPage(formPoke.Name);
            Navigation.PushAsync(infoPage, true);
        }
    }
}