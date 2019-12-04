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

                poke_id.Text = pokemon.id.ToString();
                poke_name.Text = pokemon.name.ToUpper();
                poke_weight.Text = pokemon.weight.ToString();
                poke_height.Text = pokemon.height.ToString();

                poke_imagem.Source = pokemon.sprites.front_default; //ImageSource.FromUri(new Uri(pokemon.sprites.front_default));
            }
        }

    }
}