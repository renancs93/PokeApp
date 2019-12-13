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
            PokeName.Text = pPokemonName;

            setup(pPokemonName);
        }

        private async void setup(string pPokenName) {
            PokeApiClient pokeClient = new PokeApiClient();
            Pokemon pokemon = await pokeClient.GetResourceAsync<Pokemon>(pPokenName);
            PokemonSpecies species = await pokeClient.GetResourceAsync(pokemon.Species);
            List<Item> lstItens = await pokeClient.GetResourceAsync(pokemon.HeldItems.Select(item => item.Item));
            List<Ability> lstHabilidade = await pokeClient.GetResourceAsync(pokemon.Abilities.Select(item => item.Ability));


            StringBuilder bldrItens = new StringBuilder();
            if (lstItens.Count > 0)
            {
                foreach (Item item in lstItens)
                    bldrItens.Append(item.Name).Append(", ");

                bldrItens.Remove(bldrItens.Length - 2, 2);
            }
            else
                bldrItens.Append("N/A");

            StringBuilder bldrHabilidades = new StringBuilder();
            if (lstHabilidade.Count > 0)
            {
                foreach (Ability habilidade in lstHabilidade)
                    bldrHabilidades.Append(habilidade.Name).Append(", ");

                bldrHabilidades.Remove(bldrHabilidades.Length - 2, 2);
            }
            else
                bldrHabilidades.Append("N/A");

            setImages(pokemon.Sprites);
            poke_habitat.Text = species.Habitat.Name.ToUpper();
            poke_baseExp.Text = pokemon.BaseExperience.ToString();
            poke_lstHabilidades.Text = bldrHabilidades.ToString().ToUpper();
            poke_lstItens.Text = bldrItens.ToString().ToUpper();
        }

        public void setImages(PokemonSprites pokemonSprites)
        {
            imgBackShiny.Source = pokemonSprites.BackShiny;
            imgFrontShiny.Source = pokemonSprites.FrontShiny;
            imgFrontDefault.Source = pokemonSprites.FrontDefault;
            imgBackDefault.Source = pokemonSprites.BackDefault;
        }
	}
}