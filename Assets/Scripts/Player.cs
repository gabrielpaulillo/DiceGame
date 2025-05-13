using System.Collections.Generic;
using System.Globalization;
using Random = UnityEngine.Random;

public class Player
{
    // Guarda o nome do jogador
    // Guarda as jogadas do jogador
    // Faz a somatória dos pontos do jogador
    
    // property
    public string Name { get; private set; }
    private List<int> rolls;

    // Tem construtor porque não extende de MonoBehaviour
    public Player(string name)
    {
        Name = name;
        rolls = new List<int>();
    }

    public int Roll()
    {
        int result = Random.Range(1, 7);
        rolls.Add(result);
        
        return result;
    }

    public string RollsHistory()
    {
        string rollsHistory = "";

        foreach (int roll in rolls)
        {
            rollsHistory += $"{roll.ToString()} ";
        }

        return rollsHistory;
    }

    public int RollsSum()
    {
        int rollsSum = 0;

        foreach (int roll in rolls)
        {
            rollsSum += roll;
        }

        return rollsSum;
    }

    public void DiscardRandomRollOnSix(int number)
    {
        int index = Random.Range(0, rolls.Count);
        rolls[index] = number;
    }
}
