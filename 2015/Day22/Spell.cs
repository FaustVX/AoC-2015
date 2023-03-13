#nullable enable
namespace AdventOfCode.Y2015.Day22;

abstract record class Spell(string Name, int ManaCost)
{
    public void Cast(Character me, Character other)
    {
        me.Mana -= ManaCost;
        me.ManaSpent += ManaCost;
        CastImpl(me, other);
    }
    protected abstract void CastImpl(Character me, Character other);
    public virtual bool IsValid(Character me)
    => me.Mana >= ManaCost;

    public static IEnumerable<Spell> GetSpells()
    {
        yield return new MagicMissileSpell();
        yield return new DrainSpell();
        yield return new ShieldSpell();
        yield return new PoisonSpell();
        yield return new RechargeSpell();
    }
}

sealed record class MagicMissileSpell() : Spell("Magic Missile", 53)
{
    protected override void CastImpl(Character me, Character other)
    => other.HP -= 4;
}

sealed record class DrainSpell() : Spell("Drain", 73)
{
    protected override void CastImpl(Character me, Character other)
    {
        other.HP -= 2;
        me.HP += 2;
    }
}

abstract record class EffectSpell<TEffect>(string Name, int ManaCost) : Spell(Name, ManaCost)
where TEffect : Effect, new()
{
    protected sealed override void CastImpl(Character me, Character other)
    => GetCharacter(me, other).Effects.Add(new TEffect());

    protected abstract Character GetCharacter(Character me, Character other);

    public override bool IsValid(Character me)
    => base.IsValid(me) && !me.Effects.Any(static e => e is TEffect { Duration: > 0 });
}

sealed record class ShieldSpell() : EffectSpell<ShieldEffect>("Shield", 113)
{
    protected override Character GetCharacter(Character me, Character other)
    => me;
}

sealed record class PoisonSpell() : EffectSpell<PoisonEffect>("Poison", 173)
{
    protected override Character GetCharacter(Character me, Character other)
    => other;
}

sealed record class RechargeSpell() : EffectSpell<RechargeEffect>("Recharge", 229)
{
    protected override Character GetCharacter(Character me, Character other)
    => me;
}
