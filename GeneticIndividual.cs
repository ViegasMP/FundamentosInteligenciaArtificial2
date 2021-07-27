using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static MetaHeuristic;

public class GeneticIndividual : Individual {

    public GeneticIndividual(int[] topology, int numberOfEvaluations, MutationType mutation) : base(topology, numberOfEvaluations, mutation) {
	}

	public override void Initialize () 
	{
		for (int i = 0; i < totalSize; i++)
		{
			genotype[i] = Random.Range(-1.0f, 1.0f);
		}
	}

   public override void Initialize(NeuralNetwork nn)
    {
        int count = 0;
        for (int i = 0; i < topology.Length - 1; i++)
        {
            for (int j = 0; j < topology[i]; j++)
            {
                for (int k = 0; k < topology[i + 1]; k++)
                {
                    genotype[count++] = nn.weights[i][j][k];
                }

            }
        }
    }

    public override Individual Clone()
    {
        GeneticIndividual new_ind = new GeneticIndividual(this.topology, this.maxNumberOfEvaluations, this.mutation);

        genotype.CopyTo(new_ind.genotype, 0);
        new_ind.fitness = this.Fitness;
        new_ind.evaluated = false;

        return new_ind;
    }


    public override void Mutate(float probability, float mean, float standard_deviation)
    {
        switch (mutation)
        {
            case MetaHeuristic.MutationType.Gaussian:
                MutateGaussian(probability, mean, standard_deviation);
                break;
            case MetaHeuristic.MutationType.Random:
                MutateRandom(probability);
                break;
        }
    }
    public void MutateRandom(float probability)
    {
        for (int i = 0; i < totalSize; i++)
        {
            if (Random.Range(0.0f, 1.0f) < probability)
            {
                genotype[i] = Random.Range(-1.0f, 1.0f);
            }
        }
    }

    public void MutateGaussian(float probability, float mean, float standard_deviation)
    {
        /* YOUR CODE HERE! */
        //float mean = 0f;
        //float standard_deviation = 0.5f;

        //para cada genotipo do indivíduo
        for (int i = 0; i < genotype.Length; i++)
        {
            //seguindo uma aleatoriedade e uma probabilidade
            if (Random.Range(0.0f, 1.0f) < probability)
            {
                //faz-se uma pequena mudança nesse genótipo do indivíduo seguindo a distribuição gaussiana
                //os valores de mean e standard deviation são determinados na interface do Unity e passados como parâmetros
                genotype[i] += NextGaussian(mean, standard_deviation);
            }
        }
        //throw new System.NotImplementedException();
    }

    public override void Crossover(Individual partner, float probability)
    {
        /* YOUR CODE HERE! */
        /* Nota: O crossover deverá alterar ambos os indivíduos */

        //criámos um parceiro
        GeneticIndividual ind2 = (GeneticIndividual) partner;
        //definimos um crossoverPoint random
        int crossoverPoint = Random.Range(0, totalSize-1);
        float genotype_aux;

        //aleatoriamente e seguindo uma probabilidade
        if (Random.Range(0.0f, 1.0f) < probability)
        {
            //a partir do ponto de crossover definido
            for (int i=crossoverPoint; i < totalSize; i++)
            {
                //combinamos os genotipos do indivíduo e seu parceiro
                genotype_aux = genotype[i];
                genotype[i] = ind2.genotype[i];
                ind2.genotype[i] = genotype_aux;

            }
        }

        //throw new System.NotImplementedException();
    }


}
