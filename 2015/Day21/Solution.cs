#nullable enable
namespace AdventOfCode.Y2015.Day21;

[ProblemName("RPG Simulator 20XX")]
public class Solution : Solver //, IDisplay
{
    public object PartOne(string input)
    => Execute(input, lookForMinGold: true);

    private static int Execute(string input, bool lookForMinGold)
    {
        var boss = ParseInput(input);
        var lastGoldSpent = lookForMinGold ? int.MaxValue : 0;

        foreach (var (weapon, armor, dualRing) in GetItems())
        {
            var goldSpent = weapon.Gold + armor.Gold + dualRing.Gold;

            if (lookForMinGold ? goldSpent >= lastGoldSpent : goldSpent <= lastGoldSpent)
                continue;

            var me = new Character("Me", 100, weapon.Attack + dualRing.Attack, armor.Defense + dualRing.Defense);

            if (IsPlayerAlive(me, boss) == lookForMinGold)
                lastGoldSpent = goldSpent;
        }

        return lastGoldSpent;
    }

    private static Character ParseInput(ReadOnlySpan<char> input)
    {
        var lines = input.EnumerateLines();

        lines.MoveNext();
        var hp = int.Parse(lines.Current.Slice(lines.Current.IndexOf(':') + 1));
        lines.MoveNext();
        var att = int.Parse(lines.Current.Slice(lines.Current.IndexOf(':') + 1));
        lines.MoveNext();
        var def = int.Parse(lines.Current.Slice(lines.Current.IndexOf(':') + 1));

        return new("Boss", hp, att, def);
    }

    private static (IReadOnlyList<Item> weapons, IReadOnlyList<Item> armors, IReadOnlyList<Item> rings) GetStoreItems()
    {
        return (Weapons().ToArray(), Armors().ToArray(), DualRings().ToArray());

        static IEnumerable<Item> Weapons()
        {
            yield return Item.Weapon("Dagger", 8, 4);
            yield return Item.Weapon("Shortsword", 10, 5);
            yield return Item.Weapon("Warhammer", 25, 6);
            yield return Item.Weapon("Longsword", 40, 7);
            yield return Item.Weapon("Greataxe", 74, 8);
        }

        static IEnumerable<Item> Armors()
        {
            yield return Item.None();
            yield return Item.Armor("Leather", 13, 1);
            yield return Item.Armor("Chainmail", 31, 2);
            yield return Item.Armor("Splintmail", 53, 3);
            yield return Item.Armor("Bandedmail", 75, 4);
            yield return Item.Armor("Platemail", 102, 5);
        }

        static IEnumerable<Item> DualRings()
        {
            foreach (var item1 in Rings())
                foreach (var item2 in Rings())
                    if (item1.Id != item2.Id)
                        yield return Item.Combine(item1, item2);

            static IEnumerable<Item> Rings()
            {
                yield return Item.None();
                yield return Item.Weapon("Damage +1", 25, 1);
                yield return Item.Weapon("Damage +2", 50, 2);
                yield return Item.Weapon("Damage +3", 100, 3);
                yield return Item.Armor("Defense +1", 20, 1);
                yield return Item.Armor("Defense +2", 40, 2);
                yield return Item.Armor("Defense +3", 60, 3);
            }
        }
    }

    private static IEnumerable<(Item weapon, Item armor, Item dualRings)> GetItems()
    {
        var (weapons, armors, dualRings) = GetStoreItems();

        foreach (var weapon in weapons)
            foreach (var armor in armors)
                foreach (var dualRing in dualRings)
                    yield return (weapon, armor, dualRing);
    }

    private static bool IsPlayerAlive(Character me, Character boss)
    {
        var attackMe = Math.Max(1, me.Attack - boss.Defense);
        var attackBoss = Math.Max(1, boss.Attack - me.Defense);

        do
        {
            boss.HP -= attackMe;
            if (boss.HP <= 0)
                return true;
            me.HP -= attackBoss;
            if (me.HP <= 0)
                return false;
        } while (true);
    }

    public object PartTwo(string input)
    => Execute(input, lookForMinGold: false);
}

readonly record struct Item(string Name, int Gold, int Attack, int Defense)
{
    private static int _idGenerator;
    public int Id { get; private init; }
    public static Item None()
    => new("None", 0, 0, 0) { Id = _idGenerator++ };
    public static Item Weapon(string name, int gold, int attack)
    => new(name, gold, attack, 0) { Id = name.GetHashCode() };
    public static Item Armor(string name, int gold, int defense)
    => new(name, gold, 0, defense) { Id = name.GetHashCode() };
    public static Item Combine(Item item1, Item item2)
    => new($"{item1.Name} + {item2.Name}", item1.Gold + item2.Gold, item1.Attack + item2.Attack, item1.Defense + item2.Defense);
}

record struct Character(string Name, int HP, int Attack, int Defense);
