using System.IO;
using UnityEngine;
using UnityEngine.UI;
public class LevelStart : MonoBehaviour
{

    [SerializeField] string _selectedDifficulty;
   public string chosenWord;
    public char[] characters;
    string filePath;

    [SerializeField] GameObject _emptyDisplayPrefab;
    [SerializeField] Transform _spawnLocation;
    public Text[] _textDisplay;
    public KeyboardSetUp keyboardSetUp;

    public string SelectedDifficulty
    {
        get { return _selectedDifficulty; }
    }
    void Start()
    {
        //Calling the difficulty selection function to select the difficulty
         DifficultySelection(_selectedDifficulty);
        if (chosenWord == "")
        {
            //Calling the split text file function to choose a random word
            chosenWord = SplitTextFiles(ReadTextFile());
        }               
        //Splits the characters in the word to an array of individuals
        characters = chosenWord.ToCharArray();
        //
        _textDisplay = new Text[chosenWord.Length];
        //Counting the amount of characters in the chosen word and placing empty letter prefabs in their place in scene
        for (int i = 0; i < characters.Length; i++)
        {
            //Sets the empty letter prefab to be in place of each character in the word and places them in the world
            Text letterPrefab = Instantiate(_emptyDisplayPrefab,_spawnLocation).GetComponentInChildren<Text>();
            _textDisplay[i] = letterPrefab;
        }
        keyboardSetUp.StartLoadedGame();
    }

    public void DifficultySelection(string value)
    {
        _selectedDifficulty = value;
        //
        filePath = $"{Application.streamingAssetsPath}/Difficulty/{_selectedDifficulty}.txt";
        //Prints the file path to console
        Debug.Log(filePath);
    }
    string ReadTextFile()
    {
        return File.ReadAllText(filePath);
    }
    //Splitting the text file list of words into individual words and randomly choosing one
    string SplitTextFiles(string words)
    {
        //Splits the words between every "|" symbol
        string[] wordsInFile = words.Split('|');
        //Randomly choosing one of the words in the list
        string chooseWord = wordsInFile[Random.Range(0,wordsInFile.Length)];
        //Returning the chosen word that can now be replaced into empty letter prefabs
        return chooseWord;
    }
}
