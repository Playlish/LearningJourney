using System;
using System.Collections.Generic;

[Serializable]
public class SaveData
{
    public string currentWord;
    public List<char> correctLettersGuessed = new List<char>();
    public List<char> incorrectLettersGuessed = new List<char>();  
    public int score;
    // public int remaingGuesses;
    // public int correctGuesses;
}
