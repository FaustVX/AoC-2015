#nullable enable
namespace AdventOfCode.Y2015.Day22;

[ProblemName("Wizard Simulator 20XX")]
public class Solution : Solver //, IDisplay
{
    public object PartOne(string input)
    {
        var boss = ParseInput(input);
        var me = Globals.IsTestInput ? Character.CreatePlayer(10, 250) : Character.CreatePlayer(50, 500);
        return Rounds(Spell.GetSpells().ToList(), me, boss);
    }

    private static Character ParseInput(ReadOnlySpan<char> input)
    {
        var lines = input.EnumerateLines();

        lines.MoveNext();
        var hp = int.Parse(lines.Current.Slice(lines.Current.IndexOf(':') + 1));
        lines.MoveNext();
        var att = int.Parse(lines.Current.Slice(lines.Current.IndexOf(':') + 1));

        return Character.CreateBoss(hp, att);
    }

    private static int Rounds(IReadOnlyList<Spell> spells, Character me, Character boss, int minManaSpent = int.MaxValue)
    {
        foreach (var spell in spells)
        {
            if (!spell.IsValid(me))
                continue;
            var (combatMe, combatBoss) = (me.Copy(), boss.Copy());
            var combat = combatMe.Combat(combatBoss, spell);
            if (combatMe.ManaSpent >= 100_000)
                continue;
            if (combat is false)
                continue;
            if (combat is true && combatMe.ManaSpent < minManaSpent)
            {
                minManaSpent = combatMe.ManaSpent;
                continue;
            }
            if (Rounds(spells, combatMe, combatBoss, minManaSpent) is var manaSpent && manaSpent < minManaSpent)
                minManaSpent = manaSpent;
        }
        return minManaSpent;
    }

    public object PartTwo(string input)
    {
        return 0;
    }
}

sealed record class Character(string Name, int hp, int attack, int mana)
{
    private int _hp = hp;
    public int HP
    {
        get => _hp;
        set
        {
            if (value > _hp)
                _hp = value;
            else
            {
                value += Defense;
                if (value >= _hp)
                    _hp--;
                else
                    _hp = value;
            }
        }
    }

    public int Attack { get; set; } = attack;
    public int Mana { get; set; } = mana;
    public int Defense { get; set; }
    public HashSet<Effect> Effects { get; private init; } = new(capacity: 3);
    public int ManaSpent { get; set; }

    public void ApplyEffects()
    {
        foreach (var effect in Effects)
            effect.Apply(this);
        Effects.RemoveWhere(static e => e.Duration <= 0);
    }

    public Character Copy()
    => this with
    {
        Effects = new(Effects),
    };

    public static Character CreateBoss(int hp, int attack)
    => new("Boss", hp, attack, 0);
    public static Character CreatePlayer(int hp, int mana)
    => new("Me", hp, 0, mana);

    public bool? Combat(Character boss, Spell spell)
    {
        // My turn
        ApplyEffects();
        boss.ApplyEffects();
        if (boss.HP <= 0)
            return true;

        spell.Cast(this, boss);
        if (boss.HP <= 0)
            return true;

        // Boss turn
        ApplyEffects();
        boss.ApplyEffects();
        if (boss.HP <= 0)
            return true;

        HP -= boss.Attack;
        if (HP <= 0)
            return false;
        return null;
    }

    public override string ToString()
    => $"{Name}: {HP} hp";
}
