using UnityEngine;
using UnityEngine.UI;
public class UI : MonoBehaviour
{
    public Text pointsDisplay;
    public Text guessesDisplay;
    public Text chosenWordDisplay;
    public Text chosenWordLoseDisplay;
    public Text pointsWinDisplay;
    public Text pointsLoseDisplay;

    public float guessesLeft = 7;

    public LevelStart levelStart;

    public int currentPoints;
    [SerializeField] bool _hasLost = false;
    [SerializeField] bool _hasWon = false;

    public GameObject youWinPanel;
    public GameObject youLosePanel;

    void Update()
    {
        //if the points display text isn't displaying the current points (aka not equal to)
        if (pointsDisplay.text != currentPoints.ToString())
        {
            //display the current points as text
            pointsDisplay.text = currentPoints.ToString();
        }
        //if the text isn't already displaying the guesses left (aka not equal to)
        if (guessesDisplay.text != guessesLeft.ToString())
        {
            //display remaining guesses to text
            guessesDisplay.text = guessesLeft.ToString();
        }
        //if game isn't lost and no guesses remain
        if (_hasLost == false && guessesLeft == 0)
        {
            //access script LevelStart to fetch the correct word
            levelStart = GameObject.FindGameObjectWithTag("Manager").GetComponent<LevelStart>();
            //display correct word to text
            chosenWordLoseDisplay.text = levelStart.chosenWord.ToString().ToUpper();
            //display current points on lose screen
            pointsLoseDisplay.text = currentPoints.ToString();
            //show loser popup panel
            youLosePanel.SetActive(true);
            //flip the bool to prevent continued runs
            _hasLost = true;
        }
       
    }
    public void WonMatch()
    {
       

        if (_hasWon == false)
        {
            //access script LevelStart to fetch the correct word
            levelStart = GameObject.FindGameObjectWithTag("Manager").GetComponent<LevelStart>();
            //display correct word to text
            chosenWordDisplay.text = levelStart.chosenWord.ToString().ToUpper();
            //display current points on win screen
            pointsWinDisplay.text = currentPoints.ToString();
            //show winner popup panel
            youWinPanel.SetActive(true);
            //flip the bool to prevent continued runs
            _hasWon = true;
        }
    }
}
