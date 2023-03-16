#nullable enable
namespace AdventOfCode.Y2015.Day22;

[ProblemName("Wizard Simulator 20XX")]
public class Solution : Solver //, IDisplay
{
    public object PartOne(string input)
    {
        var boss = ParseInput(input);
        var me = Character.CreatePlayer();
        return new Genetic(boss).Run();
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
                if (_hp <= 0)
                    throw new DeadException(this);
            }
        }
    }

    public int Attack { get; set; } = attack;
    public int Mana { get; set; } = mana;
    public int Defense { get; set; }
    public HashSet<Effect> Effects { get; private init; } = new(capacity: 3);
    public int ManaSpent { get; set; }
    public List<Spell> Spells { get; init; } = new(capacity: 0);
    public int SpellsUsed { get; private set; }

    public void ApplyEffects()
    {
        try
        {
            foreach (var effect in Effects)
                effect.Apply(this);
        }
        finally
        {
            Effects.RemoveWhere(static e => e.Duration <= 0);
        }
    }

    public Character Copy()
    => this with
    {
        Effects = new(Effects),
        Spells = new(Spells),
    };

    public static Character CreateBoss(int hp, int attack)
    => new("Boss", hp, attack, 0);
    public static Character CreatePlayer()
    => Globals.IsTestInput ? new("Me", 10, 0, 250) : new("Me", 50, 0, 500);
    public static Character CreatePlayer(List<Spell> spells)
    => Globals.IsTestInput ? new("Me", 10, 0, 250) { Spells = spells } : new("Me", 50, 0, 500) { Spells = spells };

    public bool? Combat(Character boss, Spell spell)
    {
        try
        {
            // My turn
            ApplyEffects();
            boss.ApplyEffects();

            spell.Cast(this, boss);
            SpellsUsed++;

            // Boss turn
            ApplyEffects();
            boss.ApplyEffects();

            HP -= boss.Attack;
            return null;
        }
        catch (DeadException dead)
        {
            return ReferenceEquals(dead.Character, boss);
        }
    }

    public override string ToString()
    => $"{Name}: {HP} hp";

    private sealed class DeadException : Exception
    {
        public DeadException(Character character)
        => Character = character;

        public Character Character { get; }
    }
}
