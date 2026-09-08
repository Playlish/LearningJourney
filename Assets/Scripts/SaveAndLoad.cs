using UnityEngine;
using System.IO;

public class SaveAndLoad : MonoBehaviour
{
    [SerializeField] SaveData _saveData;
    [SerializeField] string _levelDifficulty;
    [SerializeField] string _filePath;
    //ref our classes
    [SerializeField] UI _ui;
    [SerializeField] LevelStart _levelStart;
    [SerializeField] KeyboardSetUp _keyboardSetUp;

    public static bool loadActive = false;
    private void OnEnable()
    {
        //access script LevelStart
        _levelStart = GameObject.FindGameObjectWithTag("Manager").GetComponent<LevelStart>();
        _ui = GameObject.FindGameObjectWithTag("Manager").GetComponent<UI>();

        _levelDifficulty = _levelStart.SelectedDifficulty;

        _filePath = $"{Application.streamingAssetsPath}/Saves/{_levelDifficulty}_Save.json";
        LoadGame();
    }
    #region Save
    void GetDataToSave()
    {
        //put save data onto SaveData reference;
        _saveData.score = _ui.currentPoints;
                
        //other data that needs to be saved
      //  _saveData.remaingGuesses = (int)_ui.guessesLeft;

        //store the current word


        _saveData.currentWord = _levelStart.chosenWord;
        _saveData.correctLettersGuessed = _keyboardSetUp.correctLettersGuessed;
        _saveData.incorrectLettersGuessed = _keyboardSetUp.incorrectLettersGuessed;
        _saveData.correctGuesses = _keyboardSetUp._correctGuesses;
        Debug.Log($"Saved {_saveData.correctGuesses} correct guesses");
    }
    void SaveJson(SaveData dataToSave, string pathToSaveTo)
    {
        string contentTosave = JsonUtility.ToJson(dataToSave);
        File.WriteAllText(pathToSaveTo, contentTosave);
    }
    public void SaveGame()
    {
        GetDataToSave();
        SaveJson(_saveData, _filePath);
    }
    #endregion
    #region Load
    SaveData LoadData()
    {
        string loadedData = File.ReadAllText(_filePath);
        return JsonUtility.FromJson<SaveData>(loadedData);
    }
    void SendSaveDataToGame()
    {
        _ui.currentPoints = _saveData.score;


        // _ui.guessesLeft = _saveData.remaingGuesses; // <--- this not working
        _levelStart.chosenWord = _saveData.currentWord;

        _keyboardSetUp.correctLettersGuessed = _saveData.correctLettersGuessed;
        _keyboardSetUp.incorrectLettersGuessed = _saveData.incorrectLettersGuessed;
        _keyboardSetUp._correctGuesses = _saveData.correctGuesses;
        Debug.Log($"Loaded {_keyboardSetUp._correctGuesses} correct guesses");
    }
    public void LoadGame()
    {
        if (loadActive == true)
        {
            _saveData = LoadData();
            SendSaveDataToGame();
            loadActive = false;
        }
     
    }
    #endregion
}
