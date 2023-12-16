using System;
using UnityEngine;

[System.Serializable]
public class Medal
{
	[SerializeField] private int maxRounds;
	[SerializeField] private int maxSize;

	[SerializeField] private int bestRounds;
	[SerializeField] private int bestSize;

	public bool roundsMedal { get; private set; }
	public bool sizeMedal { get; private set; }

	public Medal(int maxRounds, int maxSize)
	{
		this.maxRounds = maxRounds;
		this.maxSize = maxSize;
		roundsMedal = false;
		sizeMedal = false;
		bestRounds = int.MaxValue;
		bestSize = int.MaxValue;
	}

	public void CheckMedals(int rounds, int size)
	{
		roundsMedal = roundsMedal ? true : rounds <= maxRounds;
		sizeMedal = sizeMedal ? true : size <= maxSize;

		bestRounds = Mathf.Min(bestRounds, rounds);
		bestSize = Mathf.Min(bestSize, size);
	}

	public override string ToString()
	{
		return "Rounds Medal: " + maxRounds + "\nSize Medal: " + maxSize;
	}
}
