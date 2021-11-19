using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AZloader : MonoBehaviour
{
    public static AZloader Instance { get; private set; }
    public static List<string> SavedGames = new List<string>();

    public List<GameData> games;
    public GameObject template;
    public RectTransform ScrollThing;
    public Sprite missingTexture;

    void Awake()
    {
        SavedGames = PlayerPrefs.GetString("SaveGame", "games/null;").Split(';').ToList();

        Instance = this;
        AZbutton.games = games;
        AZbutton.Refresh();
    }
}
