using PokeApiNet;
using PokeApiNet.Models;
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
        PokeApiClient pokeClient = new PokeApiClient();

        int page = 1;

        public MainPage()
        {
            InitializeComponent();
            
            carregamentoInicial();
        }

        private async void carregamentoInicial()
        {
            NamedApiResourceList<Pokemon> listaFull = await pokeClient.GetNamedResourcePageAsync<Pokemon>(25, page);

            //RootObject lista = await PokeApi.listaPokemons();

            lstPokemons.ItemsSource = listaFull.Results;
        }

        //private async void txtBusca_SearchButtonPressed(object sender, EventArgs e)
        //{
        //    string busca = txtBusca.Text.ToLower().Trim();

        //    if (busca != "")
        //    {
        //        RootObject pokemon = await PokeApi.buscarPokemon(busca);

        //        lstPokemons.ItemsSource = pokemon.forms;
        //    }
        //    else
        //    {
        //        carregamentoInicial();
        //    }
        //}

        private async void txtBusca_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                string busca = txtBusca.Text.ToLower().Trim();

                if(busca.Length > 2)
                {
                    //RootObject pokemon = await PokeApi.buscarPokemon(busca);
                    Pokemon pokemon = await pokeClient.GetResourceAsync<Pokemon>(busca);
                    lstPokemons.ItemsSource = pokemon.Forms;
                }
                if (busca.Length == 0)
                {
                    carregamentoInicial();
                }

            }
            catch {}

        }

        private void lstPokemons_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem == null)
            {
                return;
            }

            try
            {
                //var poke = e.SelectedItem as Form;
                var poke = e.SelectedItem as NamedApiResource<Pokemon>;
                var pokeF = e.SelectedItem as NamedApiResource<PokemonForm>;

                if (poke != null || pokeF != null)
                {
                    var detailPage = new DetailPage(poke != null? poke.Name : pokeF.Name);
                    Navigation.PushAsync(detailPage, true);
                }
            }
            catch { }
        }

        
    }
}
