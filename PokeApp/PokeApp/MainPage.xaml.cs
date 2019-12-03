using PokeApp.Model;
using PokeApp.Service;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace PokeApp
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();

            List<Form> lista = PokeApi.listaPokemons();
            lstPokemons.ItemsSource = lista;
        }

        //private async Task txtBusca_SearchButtonPressedAsync(object sender, EventArgs e)
        //{
        //    if (txtBusca.Text.Trim() != "")
        //    {
        //        List<Pokemon> x = await PokeApi.buscarPokemon(txtBusca.Text.Trim());

        //        lstPokemons.ItemsSource = x;
        //    }
                
        //}

        private async void txtBusca_SearchButtonPressed(object sender, EventArgs e)
        {
            if (txtBusca.Text.Trim() != "")
            {
                List<Form> x = await PokeApi.buscarPokemon(txtBusca.Text.Trim());

                lstPokemons.ItemsSource = x;
            }
            else
            {
                List<Form> lista = PokeApi.listaPokemons();
                lstPokemons.ItemsSource = lista;
            }
        }
    }
}
