using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using b33bo.timedEvents;

public class Menu : MonoBehaviour
{
    public static Animator CanvasAnimator;

    [SerializeField]
    Animator anim;

    public static float AnimSpeed { get; private set; }

    void Awake()
    {
        CanvasAnimator = anim;

        AnimSpeed = PlayerPrefs.GetFloat("AnimationSpeed", 1.5f);
        anim.SetFloat("AnimationSpeed", AnimSpeed);
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
        }, 1 / AnimSpeed);
    }

    public void LoadScene(string Scene)
    {
        anim.Play("Exit");
        TimedEvents.RunAfterTime(() =>
        {
            SceneManager.LoadScene(Scene);
        }, 1 / AnimSpeed);
    }

    public void ChangeVolume(float volume) =>
        AudioListener.volume = volume;

    bool isTestingVolume = false;
    public void TestVolume(AudioClip testingSound)
    {
        if (isTestingVolume)
            return;

        isTestingVolume = true;
        TimedEvents.RunAfterTime(() => isTestingVolume = false, testingSound.length);
        AudioSource.PlayClipAtPoint(testingSound, Vector3.zero);
    }

    public void DeleteAllData() =>
        PlayerPrefs.DeleteAll();
}
