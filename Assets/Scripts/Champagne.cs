using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using b33bo.timedEvents;

public class Champagne : MonoBehaviour
{
    private static Champagne instance;
    [SerializeField]
    private Animator A_anim, B_anim;

    [SerializeField]
    private ParticleSystem A_particle, B_particle;

    [SerializeField]
    private AudioClip PoofSound;

    void Awake() =>
        instance = this;

    public static void Play()
    {
        Transform camPos = Camera.main.transform;
        Vector3 origin = camPos.position;

        TimedEvents.RepeatForSeconds((float time) =>
        {
            camPos.position = Vector3.Lerp(origin, new Vector3(0, -4.5f, -10), time);
        }, 1);
        instance.StartCoroutine(ChanpagneSequence());
    }

    private static IEnumerator ChanpagneSequence()
    {
        yield return new WaitForSeconds(1);
        instance.A_anim.Play("Win");
        instance.B_anim.Play("Win");
        yield return new WaitForSeconds(3);
        AudioSource.PlayClipAtPoint(instance.PoofSound, Camera.main.transform.position);
        yield return new WaitForSeconds(.25f);
        instance.A_particle.Play();
        instance.B_particle.Play();
    }
}
