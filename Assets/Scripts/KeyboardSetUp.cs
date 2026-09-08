using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class KeyboardSetUp : MonoBehaviour
{
    [SerializeField] char[] _keys;
    [SerializeField] GameObject[] _uiButtons;
    public int _correctGuesses;
    [SerializeField] UI _ui;
    [SerializeField] LevelStart _levelStart;
    [SerializeField] SaveData _saveData;

    public LevelStart levelStart;
    public UI uiManager;
    public SaveAndLoad saveAndLoad;

    public List<char> correctLettersGuessed = new List<char> ();
    public List<char> incorrectLettersGuessed = new List<char> ();
    public Sprite defaultSprite, correctSprite, incorrectSprite;

    private void Awake()
    {
        levelStart = GameObject.FindGameObjectWithTag("Manager").GetComponent<LevelStart>();
        uiManager = GameObject.FindGameObjectWithTag("Manager").GetComponent<UI>();
        //load level setup
        ReloadKeyboard();
        
    }
    public void StartLoadedGame()
    {
        CheckLoadedData();
    }

    public void ClickButton(int index)
    {
        if (uiManager.guessesLeft != 0)
        {
            //right guess
            if (levelStart.chosenWord.Contains(_keys[index]))
            {
                Debug.Log("You guessed correct!");
                //change selected button to green when right
               // _uiButtons[index].GetComponent<Image>().color = Color.green;
                _uiButtons[index].GetComponent<Button>().image.sprite = correctSprite;

                //add points to score
                uiManager.currentPoints = uiManager.currentPoints + 10;
                //debug how many points we have
                Debug.Log($"You have {uiManager.currentPoints} points");

                //for every character in the word
                for (int i = 0; i < levelStart.characters.Length; i++)
                {  
                    //if the character is the one guessed
                    if (levelStart.characters[i] == _keys[index])
                    {
                        //display the correctly guessed letter
                        levelStart._textDisplay[i].text = _keys[index].ToString().ToUpper();
                        //add to the correct guesses counter
                        _correctGuesses++;
                        //add the letter to the list of guessed letters
                        correctLettersGuessed.Add(_keys[index]);
                        _uiButtons[index].GetComponent<Button>().interactable = false;
                    }
                }
            }
            //wrong guess
            else
            {
                Debug.Log("You guessed incorrect!");
                //change the selected button when wrong
                _uiButtons[index].GetComponent<Button>().image.sprite = incorrectSprite;

                //remove points
                uiManager.currentPoints = uiManager.currentPoints - 5;
                //debug the amount of points
                Debug.Log($"You have {uiManager.currentPoints} points");
                //removes one remaining guess
                uiManager.guessesLeft--;
                //make that letter unable to be guessed
                _uiButtons[index].GetComponent<Button>().interactable = false;
                //debug which letter was guessed
                Debug.Log($"You guessed: {_keys[index]}");
                //add that letter to the list of wrong letters
                incorrectLettersGuessed.Add(_keys[index]);

                //when level is lost
                if (uiManager.guessesLeft <= 0)
                {
                    Debug.Log("You have lost!");

                    //change scene to points display, try again, quit menu
                    foreach (var key in _uiButtons)
                    {
                        //make the keys unable to be pressed
                        key.GetComponent<Button>().interactable = false;
                    }
                }
            }
            
            
        }
        //when the amount of correct guesses is the same as the amount of correct letters in the word
        if (_correctGuesses == levelStart.characters.Length)
        {
            //you win!
            uiManager.WonMatch();
        }
       
    }
    void ReloadKeyboard()
    {
        //for every button
        for (int i = 0; i < _uiButtons.Length; i++)
        {
            //searching for text inside each button (and it's childen), converting to an uppercase string
            _uiButtons[i].GetComponentInChildren<Text>().text = _keys[i].ToString().ToUpper();
            //setting/resetting buttons to be white & interactable
            _uiButtons[i].GetComponent<Button>().image.color = Color.white;
            _uiButtons[i].GetComponent<Button>().image.sprite = defaultSprite;
            _uiButtons[i].GetComponent<Button>().interactable = true;
            //resets points to 0
            uiManager.currentPoints = 0;
            //reset remaining  guesses            
            uiManager.guessesLeft = 7;
            //reset correct guesses
            _correctGuesses = 0;
        }
    }

    void CheckLoadedData()
    {
        for (int currentLetterWeAreChecking = 0; currentLetterWeAreChecking < _keys.Length; currentLetterWeAreChecking++)
        {
            foreach (char letter in correctLettersGuessed)
            {
                if (_keys[currentLetterWeAreChecking] == letter)
                {
                    
                    //_uiButtons[currentLetterWeAreChecking].GetComponent<Button>().image.color = Color.green;
                    _uiButtons[currentLetterWeAreChecking].GetComponent<Button>().image.sprite = correctSprite;

                    _uiButtons[currentLetterWeAreChecking].GetComponent<Button>().interactable = false;
                }
                for (int i = 0; i < levelStart.characters.Length; i++)
                {
                    if (levelStart.characters[i] == letter)
                    {
                        levelStart._textDisplay[i].text = letter.ToString().ToUpper();
                    }
                }
            }
            foreach (char letter in incorrectLettersGuessed)
            {
                if (_keys[currentLetterWeAreChecking] == letter)
                {
                    //  _uiButtons[currentLetterWeAreChecking].GetComponent<Button>().image.color = Color.red;
                    _uiButtons[currentLetterWeAreChecking].GetComponent<Button>().image.sprite = incorrectSprite;

                    _uiButtons[currentLetterWeAreChecking].GetComponent<Button>().interactable = false;
                    uiManager.guessesLeft--;

                }
            }
            if (uiManager.guessesLeft <= 0)
            {
                Debug.Log("You have lost!");

                //change scene to points display, try again, quit menu
                foreach (var key in _uiButtons)
                {
                    //make the keys unable to be pressed
                    key.GetComponent<Button>().interactable = false;
                }
            }
           
        }
    }
}

