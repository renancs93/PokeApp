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

            carregamentoInicial();
        }

        private async void carregamentoInicial()
        {
            //List<Form> lista = PokeApi.listaPokemons();
            RootObject lista = await PokeApi.listaPokemons();
            lstPokemons.ItemsSource = lista.results;
        }

        private async void txtBusca_SearchButtonPressed(object sender, EventArgs e)
        {
            if (txtBusca.Text.Trim() != "")
            {
                RootObject pokemon = await PokeApi.buscarPokemon(txtBusca.Text.Trim());

                lstPokemons.ItemsSource = pokemon.forms;
            }
            else
            {
                carregamentoInicial();
            }
        }
    }
}
