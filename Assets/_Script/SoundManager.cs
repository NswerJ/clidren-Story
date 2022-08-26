using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(instance);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void SFXPlay(string sfxName, AudioClip clip)
    {
        GameObject jump = new GameObject(sfxName + "Sound");
        AudioSource audiosource = jump.AddComponent<AudioSource>();
        audiosource.clip = clip;
        audiosource.Play();

        Destroy(jump, clip.length);
    }
}
