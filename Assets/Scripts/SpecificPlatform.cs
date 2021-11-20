using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecificPlatform : MonoBehaviour
{
    [SerializeField]
    private List<RuntimePlatform> platforms;

#pragma warning disable CS0162 // Unreachable code detected
    void Awake()
    {
#if UNITY_EDITOR
        return;
#endif
        if (!platforms.Contains(Application.platform))
            Destroy(gameObject);
    }
#pragma warning restore CS0162 // Unreachable code detected
}
