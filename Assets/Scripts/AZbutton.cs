using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Linq;
using System.IO;
using TMPro;
using b33bo.utils;
using b33bo.timedEvents;

public class AZbutton : MonoBehaviour
{
    public static string SelectedGamePath = "games/alphabet";
    public static string SelectedGameName = "alphabet";
    public static List<GameData> games;
    public static GameObject template;
    public static List<AZbutton> instances = new List<AZbutton>();

    public string Filename;
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private RectTransform rt;
    [SerializeField] private Image icon;

    public static void Refresh()
    {
        if (template == null)
            template = Resources.Load<GameObject>("Template");

        string newPos = Application.persistentDataPath + @"\Custom";
        List<string> files = GenerateFiles(newPos, out List<Sprite> sprites);

        Vector2 currentPos = new Vector2(0, -200);

        //Scale = 512x, where x = number of posts
        AZloader.Instance.ScrollThing.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, files.Count * 512);

        for (int i = 0; i < files.Count; i++)
        {
            GameObject newGameobject = Instantiate(template, AZloader.Instance.ScrollThing);
            AZbutton newButton = newGameobject.GetComponent<AZbutton>();

            newButton.Filename = files[i];
            newButton.icon.sprite = sprites[i];

            newButton.rt.anchoredPosition = currentPos;
            currentPos.y -= newButton.rt.rect.height + 200;
        }
    }

    private static List<string> GenerateFiles(string newPos, out List<Sprite> sprites)
    {
        List<string> files = new List<string>();
        sprites = new List<Sprite>();
        for (int i = 0; i < games.Count; i++)
        {
            files.Add(games[i].Path);
            sprites.Add(games[i].icon);
        }

        try
        {
            string[] newFiles = Directory.GetFiles(newPos);

            for (int i = 0; i < newFiles.Length; i++)
            {
                if (newFiles[i].EndsWith(".png"))
                    continue;

                files.Add(newFiles[i]);

                Sprite newIcon = IMG2Sprite.Instance.LoadNewSprite(GetFileName(newFiles[i]) + ".png");

                if (newIcon == null)
                    sprites.Add(AZloader.Instance.missingTexture);
                else
                    sprites.Add(newIcon);
            }
        }
        catch (IOException exc)
        {
            Debug.LogError($"Failed to load files inside {newPos}.\n {exc.Message}");
        }

        return files;
    }

    void Start()
    {
        instances.Add(this);
        string[] filenameSplit = Filename.Split('/', '\\');

        string newFile = filenameSplit[filenameSplit.Length - 1];
        text.text = GetFileName(newFile);
    }

    static string GetFileName(string input)
    {
        string[] splitByDots = input.Split('.');
        return splitByDots.Sublist(0, splitByDots.Length).ToFormattedString(".");
    }

    void OnDestroy()
    {
        instances.Remove(this);
    }

    public void Load()
    {
        Menu.CanvasAnimator.Play("Exit");
        TimedEvents.RunAfterTime(() =>
        {
            SelectedGameName = text.text;
            SelectedGamePath = Filename;
            SceneManager.LoadScene("Game");
        }, 1 / Menu.AnimSpeed);
    }
}
