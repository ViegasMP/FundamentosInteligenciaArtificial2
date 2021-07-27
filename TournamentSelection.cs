using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
public class TournamentSelection : SelectionMethod
{
	private int tournamentSize;

	public TournamentSelection(int tournamentSize) : base()
	{
		this.tournamentSize = tournamentSize;
	}

	public override List<Individual> selectIndividuals(List<Individual> oldpop, int num)
	{
		if (oldpop.Count < tournamentSize)
		{
			throw new System.Exception("The population size is smaller than the tournament size.");
		}

		List<Individual> selectedInds = new List<Individual>();
		for (int i = 0; i < num; i++)
		{
			selectedInds.Add(tournamentSelection(oldpop,tournamentSize).Clone());
		}

		return selectedInds;
	}

	public Individual tournamentSelection(List<Individual> population, int tournamentSize)
	{
		//escolhe o melhor indivíduo num torneio de tournamentSize indivíduos
		
		//* YOUR CODE HERE //
		Individual best = null;

		for (int i = 0; i < tournamentSize; i++)
		{
			Individual ind = population[Random.Range(0, population.Count-1)];

			/* Maximação */
			//caso nao exista um best ou o novo best seja pior que o do individuo atual, guardamos um novo best
			if (best == null || ind.Fitness > best.Fitness)
				best = ind.Clone();
		}
		return best;
		//throw new System.NotImplementedException();
	}
}
