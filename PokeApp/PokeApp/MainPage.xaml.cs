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
                    RootObject pokemon = await PokeApi.buscarPokemon(busca);
                    lstPokemons.ItemsSource = pokemon.forms;
                }
                if(busca.Length == 0)
                {
                    carregamentoInicial();
                }

            }
            catch {}

        }

        private void lstPokemons_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e == null)
                return;

            var detailPage = new DetailPage(e.SelectedItem as Form);
            Navigation.PushAsync(detailPage, true);
        }

        
    }
}
