using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndMusicManager : MonoBehaviour
{
    [SerializeField] private AudioSource cheerMusic;
    [SerializeField] private AudioSource backgroundMusic;

    void Start()
    {
        Destroy(FindObjectsOfType<DontDestroyAudio>()[0].gameObject);
        cheerMusic.PlayOneShot(cheerMusic.clip);
        backgroundMusic.PlayDelayed(8.4f);
    }
}
