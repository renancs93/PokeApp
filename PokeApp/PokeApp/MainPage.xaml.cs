using PokeApiNet;
using System;
using System.ComponentModel;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace PokeApp
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        PokeApiClient pokeClient = new PokeApiClient();
        private readonly DebounceHelper debounceHelper;

        int page = 1;

        public MainPage()
        {
            InitializeComponent();
            
            carregamentoInicial();
            debounceHelper = new DebounceHelper(TimeSpan.FromMilliseconds(500), ExecuteSearch);
        }

        private async void carregamentoInicial()
        {
            NamedApiResourceList<Pokemon> listaFull = await pokeClient.GetNamedResourcePageAsync<Pokemon>(25, page);

            //RootObject lista = await PokeApi.listaPokemons();

            lstPokemons.ItemsSource = listaFull.Results;
        }

        private void txtBusca_TextChanged(object sender, TextChangedEventArgs e)
        {
            debounceHelper.Debounce(e.NewTextValue);
        }

        private async void ExecuteSearch(string query)
        {
            try
            {
                string busca = query.ToLower().Trim();

                if (busca.Length > 2)
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
            catch (Exception ex)
            {

            }
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

    public class DebounceHelper
    {
        private readonly TimeSpan delay;
        private readonly Action<string> action;
        private CancellationTokenSource cts;

        public DebounceHelper(TimeSpan delay, Action<string> action)
        {
            this.delay = delay;
            this.action = action;
            cts = new CancellationTokenSource();
        }

        public void Debounce(string text)
        {
            // Cancelar qualquer execução anterior pendente
            cts.Cancel();
            cts = new CancellationTokenSource();

            // Agendar a execução após o atraso
            _ = ScheduleExecution(text, cts.Token);
        }

        private async Task ScheduleExecution(string text, CancellationToken cancellationToken)
        {
            try
            {
                // Aguardar o atraso
                await Task.Delay(delay, cancellationToken);

                // Se não foi cancelado, executar a ação
                if (!cancellationToken.IsCancellationRequested)
                    action(text);
            }
            catch (TaskCanceledException)
            {
                // A tarefa foi cancelada, não faz nada
            }
        }
    }
}
