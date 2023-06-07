using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Adjuster : MonoBehaviour
{
    public AudioSource audioSource;// Start is called before the first frame update
    void Awake()
    {
        audioSource.volume *= OptionManager.soundVolume;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
