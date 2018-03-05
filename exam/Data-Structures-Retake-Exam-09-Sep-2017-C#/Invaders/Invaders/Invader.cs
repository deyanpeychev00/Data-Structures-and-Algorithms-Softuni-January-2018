using System;

public class Invader : IInvader
{
    public Invader(int damage, int distance)
    {
        this.Distance = distance;
        this.Damage = damage;
    }
    
    public int Damage { get; set; }
    public int Distance { get; set; }

    public int CompareTo(IInvader other)
    {
        return this.Distance.CompareTo(other.Distance) != 0 ? this.Distance.CompareTo(other.Distance) : other.Damage.CompareTo(this.Damage);
    }
}
