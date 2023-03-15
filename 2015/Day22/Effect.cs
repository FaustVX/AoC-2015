#nullable enable
namespace AdventOfCode.Y2015.Day22;

abstract record class Effect(string Name, int Duration)
{
    public int Duration { get; private set; } = Duration;
    public void Apply(Character me)
    {
        ApplyImpl(me);
        Duration--;
    }

    protected abstract void ApplyImpl(Character me);
}

sealed record class ShieldEffect() : Effect("Shield", 6)
{
    protected override void ApplyImpl(Character me)
    => me.Defense += 7;
}

sealed record class PoisonEffect() : Effect("Poison", 6)
{
    protected override void ApplyImpl(Character me)
    => me.HP -= 3;
}

sealed record class RechargeEffect() : Effect("Recharge", 5)
{
    protected override void ApplyImpl(Character me)
    => me.Mana += 101;
}
