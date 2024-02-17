using System;
using System.Collections.Generic;
using System.Linq;

namespace DenominationRoutineApp
{
	class Program
	{
			static void Main()
			{
				int[] denominations = { 10, 50, 100 };

				int[] payouts = { 30, 50, 60, 80, 140, 230, 370, 610, 980 };

				foreach (var pay in payouts)
				{
					Console.WriteLine($"Combinations for {pay} EUR:");

					var combinations = CalculateCombo(pay, denominations);

					foreach (var combination in combinations)
					{
						Console.WriteLine(combination);
					}

					Console.WriteLine();
				}
				Console.ReadLine();
			}

			static List<string> CalculateCombo(int amount, int[] denominations)
			{
				List<string> combinations = new List<string>();
				CalculateCombinationsService(amount, denominations, 0, new List<int>(), combinations);
				return combinations;
			}

			static void CalculateCombinationsService(int remainingAmount, int[] denominations, int index, List<int> currentCombination, List<string> combinations)
			{
				if (remainingAmount == 0)
				{
					combinations.Add(CreateCombinationString(currentCombination));
					return;
				}
				var conditionCheck = new[]{
					remainingAmount < 0,
					index >= denominations.Length
				};

				if (conditionCheck.Any(x=>x))
				{
					return;
				}

				// Include the current denomination
				currentCombination.Add(denominations[index]);
				CalculateCombinationsService(remainingAmount - denominations[index], denominations, index, currentCombination, combinations);
				currentCombination.RemoveAt(currentCombination.Count - 1);

				// Exclude the current denomination
				CalculateCombinationsService(remainingAmount, denominations, index + 1, currentCombination, combinations);
			}

			static string CreateCombinationString(List<int> combination)
			{
				Dictionary<int, int> count = new Dictionary<int, int>();
				foreach (var denomination in combination)
				{
					if (count.ContainsKey(denomination))
					{
						count[denomination]++;
					}
					else
					{
						count[denomination] = 1;
					}
				}

				List<string> parts = new List<string>();
				foreach (var pair in count)
				{
					parts.Add($"{pair.Value} x {pair.Key} EUR");
				}

				return string.Join(" + ", parts);
			}
		
	}
}
