using System;
using System.Collections.Generic;
using System.Linq;

public class Computer : IComputer
{

    private List<Invader> records;
    private int energy;
    private bool areRecordsSorted;

    public Computer(int energy)
    {
        this.records = new List<Invader>();
        if(energy < 0)
        {
            throw new ArgumentException();
        }
        this.energy = energy;
        this.areRecordsSorted = false;
    }

    public int Energy
    {
        get
        {
            return this.energy < 0 ? 0 : this.energy;
        }
    }

    public void Skip(int turns)
    {
        for (int i = 0; i < this.records.Count; i++)
        {
            this.records[i].Distance -= turns;
            if (this.records[i].Distance <= 0)
            {
                this.energy -= this.records[i].Damage;
                this.records.RemoveAt(i);
                i--;
            }
        }
    }

    public void AddInvader(Invader invader)
    {
        this.records.Add(invader);
    }

    public void DestroyHighestPriorityTargets(int count)
    {
        // Destroy all targets
        if (this.records.Count <= count)
        {
            this.records.Clear();
            return;
        }
        // Sort records
        if (!areRecordsSorted)
        {
            this.records.Sort();
            areRecordsSorted = true;
        }

        this.records = this.records.Skip(count).ToList();
    }

    public void DestroyTargetsInRadius(int radius)
    {
        this.records.RemoveAll(x => x.Distance <= radius);
    }

    public IEnumerable<Invader> Invaders()
    {
        foreach (var invader in this.records)
        {
            yield return invader;
        }
    }
}
