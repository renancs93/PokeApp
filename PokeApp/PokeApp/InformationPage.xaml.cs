using PokeApiNet;
using PokeApiNet.Models;
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
	public partial class InformationPage : ContentPage
	{
		public InformationPage (String pPokemonName)
		{
			InitializeComponent();
            //lblPrinc.Text = pPokemonName;
            setup(pPokemonName);
        }

        private async void setup(string pPokenName) {
            PokeApiClient pokeClient = new PokeApiClient();
            Pokemon pokemon = await pokeClient.GetResourceAsync<Pokemon>(pPokenName);
            //List<Move> lstMovimentos = await pokeClient.GetResourceAsync(pokemon.Moves.Select(move => move.Move));

            List<Ability> lstHabilidade = await pokeClient.GetResourceAsync(pokemon.Abilities.Select(item => item.Ability));

            //PokemonSpecies species = await pokeClient.GetResourceAsync(pokemon.Species);

            StringBuilder blr = new StringBuilder();

            foreach (Ability hab in lstHabilidade) {
                
                blr.Append(hab.Name).Append(" - ");
            }

            blr.Remove(blr.Length - 3, 3);

            lblPrinc.Text = blr.ToString();
        }
	}
}