using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AZloader : MonoBehaviour
{
    public static AZloader Instance { get; private set; }

    public List<GameData> games;
    public GameObject template;
    public RectTransform ScrollThing;
    public Sprite missingTexture;

    void Awake()
    {
        Instance = this;
        AZbutton.games = games;
        AZbutton.Refresh();
    }
}
