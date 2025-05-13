using UnityEngine;

[RequireComponent(typeof(Display), typeof(ChangeScene))]
public class DiceGame : MonoBehaviour
{
    [SerializeField] private int maxRounds = 5;

    [SerializeField] private string playerName = "Jogador";
    [SerializeField] private string computerName = "Computador";

    [SerializeField] private string sceneName;

    private int _currentRound;
    private Player _player, _computer;
    private Display _display;
    private ChangeScene _changeScene;
    private void Start()
    {
        _player = new Player(playerName);
        _computer = new Player(computerName);

        _display = GetComponent<Display>();
        _changeScene = GetComponent<ChangeScene>();

        _currentRound = 0;

        _display.playerName.text = _player.Name;
        _display.playerScore.text = "";
        
        _display.computerName.text = _computer.Name;
        _display.computerScore.text = "";

        _display.title.text = "Click to start the game";
        _display.buttonLabel.text = "Roll the dice";
    }

    public void Click()
    {
        if (_currentRound == -1)
            _changeScene.LoadSceneByName(sceneName);
        else if (_currentRound < maxRounds)
            Roll();
        else
            ShowResults();
    }

    private void ShowResults()
    {
        _display.buttonLabel.text = "Back to the menu";

        int playerRollsSum = _player.RollsSum();
        int computerRollsSum = _computer.RollsSum();

        string winner = null;

        if (playerRollsSum > computerRollsSum)
            winner = playerName;
        else if (computerRollsSum > playerRollsSum)
            winner = computerName;

        if (winner != null)
            _display.title.text = $"Result: {winner} Wins!";
        else
            _display.title.text = "Result: Tie!";

        _currentRound = -1;
    }

    private void Roll()
    {
        _currentRound++;
        _display.title.text = $"Round ({_currentRound} / {maxRounds})";

        if (_currentRound == maxRounds)
            _display.buttonLabel.text = "Show results";
        
        // Regra do número 6
        int playerRoll = _player.Roll();
        int computerRoll = _computer.Roll();

        if (playerRoll == 6)
            _computer.DiscardRandomRollOnSix(0);
        
        if (computerRoll == 6)
            _player.DiscardRandomRollOnSix(0);

        _display.playerScore.text = $"Jogadas: {_player.RollsHistory()} (Soma: {_player.RollsSum()})"; // interpolação de strings, menos verbosa que "string.Format()" que se baseia em índices
        _display.computerScore.text = string.Format("Jogadas: {0} (Soma: {1})", _computer.RollsHistory(), _computer.RollsSum());
    }

}
