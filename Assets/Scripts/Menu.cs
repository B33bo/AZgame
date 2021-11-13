using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using b33bo.timedEvents;

public class Menu : MonoBehaviour
{
    public static Animator CanvasAnimator;

    [SerializeField]
    Animator anim;

    void Awake()
    {
        CanvasAnimator = anim;
    }

    public void OpenFolder(string folder)
    {
        folder = folder.Replace("%pers%", Application.persistentDataPath).Replace("%data%", Application.dataPath);

        if (!(Directory.Exists(folder) || File.Exists(folder)))
            Directory.CreateDirectory(folder);

        Application.OpenURL($"file://{folder}");
    }

    public void QuitGame()
    {
        anim.Play("Exit");

        TimedEvents.RunAfterTime(() =>
        {
            Application.Quit();

#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#endif
        }, 1.5f);
    }
}
