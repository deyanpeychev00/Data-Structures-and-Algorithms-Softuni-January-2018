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
        int compare = this.Distance.CompareTo(other.Distance);
        if(compare == 0)
        {
            return other.Damage.CompareTo(this.Damage);
        }
        return compare;
    }
}
