#nullable enable
namespace AdventOfCode.Y2015.Day22;

class Genetic
{
    public Genetic(Character boss)
    {
        _boss = boss.Copy();
        _rng = new(0);
        MutationProbability = _rng.NextDouble() / 20;
        LengthMutationProbability = _rng.NextDouble() / 50;
        _players = GeneratePlayers().Take(75).ToArray();
        _fitness = new int[_players.Length];
        _fittest = _secondFittest = 0;
    }

    private static IReadOnlyList<Spell> Spells = Spell.GetSpells().ToArray();

    private readonly Character _boss;
    private readonly Character[] _players;
    private readonly int[] _fitness;
    public int GenerationCount { get; private set; }
    private readonly Random _rng;
    public double MutationProbability { get; }
    public double LengthMutationProbability { get; }
    private int _fittest, _secondFittest;

    public int Run()
    {
        var bestFitness = int.MaxValue;
        CalculateAllFitness();

        var i = 2_500;
        while (true)
        {
            NextGeneration();
            if (_fitness[_fittest] < bestFitness)
                bestFitness = _fitness[_fittest];
            if (i <= 0)
                break;
            ResetPlayers();
            i--;
        }

        return bestFitness;
    }

    private void ResetPlayers()
    {
        for (var i = 0; i < _players.Length; i++)
            _players[i] = Character.CreatePlayer(_players[i].Spells);
    }

    public void NextGeneration()
    {
        GenerationCount++;
        Crossover();
        Mutate();
        AddFittestOffspring();
        CalculateAllFitness();

        Debug.WriteLine($"Gen: {GenerationCount}, Fittest: {_fitness[_fittest]}");
    }

    private void Mutate()
    {
        foreach (var player in _players)
            Mutate(player);
    }

    private void CalculateAllFitness()
    {
        for (var i = 0; i < _players.Length; i++)
            CalculateFitness(i);
        Selection();
    }

    private void Selection()
    {
        var (min1, min2) = (0, 1);
        for (var i = 0; i < _fitness.Length; i++)
            if (_fitness[i] < _fitness[min1])
                (min2, min1) = (min1, i);
            else if (_fitness[i] < _fitness[min2])
                min2 = i;
        (_fittest, _secondFittest) = (min1, min2);
    }

    private void Crossover()
    {
        for (var i = Math.Min(_players[_fittest].Spells.Count, _players[_secondFittest].Spells.Count) - 1; i >= 0; i--)
            if (_rng.Next(2) is 0)
                (_players[_fittest].Spells[i], _players[_secondFittest].Spells[i]) = (_players[_secondFittest].Spells[i], _players[_fittest].Spells[i]);
    }

    private void Mutate(Character player)
    {
        if (_rng.NextDouble() < LengthMutationProbability)
            if (_rng.Next(2) is 0 && player.Spells.Count > 2)
                player.Spells.RemoveAt(player.Spells.Count - 1);
            else
                player.Spells.Add(GenerateSpells().First());

        for (var i = 0; i < player.Spells.Count; i++)
            if (_rng.NextDouble() < MutationProbability)
                player.Spells[i] = GenerateSpells().First();
    }

    private void AddFittestOffspring()
    {
        CalculateFitness(_fittest);
        CalculateFitness(_secondFittest);
        _players[LeastFittedIndex()] = GetFittestOffspring();
    }

    private Character GetFittestOffspring()
    {
        if (_fitness[_fittest] < _fitness[_secondFittest])
            return _players[_fittest];
        return _players[_secondFittest];
    }

    private int LeastFittedIndex()
    {
        var maxI = 0;
        for (var i = 0; i < _fitness.Length; i++)
            if (_fitness[i] == int.MaxValue)
                return i;
            else if (_fitness[i] > _fitness[maxI])
                maxI = i;
        return maxI;
    }

    private void CalculateFitness(int i)
    => _fitness[i] = CalculateFitness(_players[i]);

    private int CalculateFitness(Character player)
    {
        var boss = _boss.Copy();
        var i = 0;
        foreach (var spell in player.Spells)
        {
            if (!spell.IsValid(player))
                return int.MaxValue / (i + 1);
            var combat = player.Combat(boss, spell);
            if (combat is false)
                return int.MaxValue / (i + 1);
            if (combat is true)
                return player.ManaSpent * (player.Spells.Count - i);
                i++;
        }
        return int.MaxValue / (player.HP > boss.HP ? 1000 : 1);
    }

    private IEnumerable<Character> GeneratePlayers()
    {
        while (true)
            yield return Character.CreatePlayer(GenerateSpells().Take(_rng.Next(3, 10)).ToList());
    }
    private IEnumerable<Spell> GenerateSpells()
    {
        while (true)
            yield return Spells[_rng.Next(Spells.Count)];
    }
}
