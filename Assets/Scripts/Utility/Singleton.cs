using System;
using UnityEngine;

public class Singleton : MonoBehaviour
{
    public static Singleton Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Debug.Log("Found a second instance of this singleton, destroying copycat...", gameObject);
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }
}
