using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using TMPro;
using System.Linq;
using b33bo.timedEvents;
using b33bo.utils;

public class Game : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private TMP_InputField inputField;
    [SerializeField] private TextMeshProUGUI NameofGame;
    private List<string> wordsWithLetter = new List<string>();
    private readonly List<char> letters = new List<char>();
    int currentLetter = 0;
    private List<string> words;
    private int hintCurrent;

    void Start()
    {
        FindWords();
        FindLetters();
        NameofGame.text = AZbutton.SelectedGameName;

        if (words.Count == 0)
        {
            Win();
            text.text = "";
            return;
        }

        currentLetter = - 1;
        AdvanceLetter();

        text.text = letters[0].ToString().ToUpper();
    }

    void FindWords()
    {
        if (File.Exists(AZbutton.SelectedGamePath))
        {
            Debug.Log($"Loading <i>{AZbutton.SelectedGamePath}</i> as a file");
            words = File.ReadAllLines(AZbutton.SelectedGamePath).ToList();
        }
        else
        {
            Debug.Log($"Loading <i>{AZbutton.SelectedGamePath}</i> from resources");

            TextAsset newText = Resources.Load<TextAsset>(AZbutton.SelectedGamePath);

            if (!newText)
            {
                Debug.LogError($"Could not load <i>{AZbutton.SelectedGamePath}</i> (from resources)");
                words = new List<string>();
                return;
            }
            else
                words = newText.text.Split('\n').ToList();
        }

        for (int i = 0; i < words.Count; i++)
        {
            words[i] = words[i].ToLower().Trim().Replace(" ", "");
        }

        while (words.Contains(""))
        {
            words.Remove("");
        }
    }

    void FindLetters()
    {
        for (int i = 0; i < words.Count; i++)
        {
            if (words[i].Length == 0)
            {
                letters.Add(char.MinValue);
                continue;
            }

            char currentLetter = words[i][0];
            if (!letters.Contains(currentLetter))
            {
                letters.Add(currentLetter);
            }
        }
    }

    public void Check(string word)
    {
        if (word.Length == 0)
            return;

        if (currentLetter == letters.Count)
            return;

        word = word.ToLower().Trim().Replace(" ", "");
        bool WordExist = words.Contains(word);

        bool CorrectLetter = letters[currentLetter] == word[0];

        if (WordExist && CorrectLetter)
            AdvanceLetter();
    }

    void AdvanceLetter()
    {
        hintCurrent = 0;
        currentLetter++;

        if (currentLetter == letters.Count)
        {
            Win();
            return;
        }

        wordsWithLetter = new List<string>();
        for (int i = 0; i < words.Count; i++)
        {
            if (words[i][0] == letters[currentLetter])
                wordsWithLetter.Add(words[i]);
        }

        text.text = letters[currentLetter].ToString().ToUpper();
        inputField.text = "";
    }

    public void Win()
    {
        print("WIN");
        text.text = "You Win!";
        Champagne.Play();
        AZloader.SavedGames.Add(AZbutton.SelectedGamePath);
        PlayerPrefs.SetString("SaveGame", AZloader.SavedGames.ToFormattedString(";"));

        TimedEvents.RunAfterTime(() =>
        {
            SceneManager.LoadScene("Selection");
        }, 10);
    }

    public void InsertHint()
    {
        if (wordsWithLetter.Count == 0)
            return;

        inputField.text = wordsWithLetter[hintCurrent];

        hintCurrent++;

        if (hintCurrent >= wordsWithLetter.Count)
            hintCurrent = 0;
    }
}
