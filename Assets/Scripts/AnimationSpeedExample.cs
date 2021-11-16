using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationSpeedExample : MonoBehaviour
{
    [SerializeField] private Animator animator;

    public void ChangeSpeed(float S)
    {
        animator.SetFloat("AnimationSpeed", S);
        PlayerPrefs.SetFloat("AnimationSpeed", S);

        Menu.CanvasAnimator.SetFloat("AnimationSpeed", S);
    }
}
