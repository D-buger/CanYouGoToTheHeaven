using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance = null;

    AudioSource audioSource;
    [SerializeField] int SFXPlayerCount = 4;

    [SerializeField] AudioClip currentPlayingBGM;

    Dictionary<string, int> audioSourceIndex = new Dictionary<string, int>();
    Queue<AudioSource> sfxPlayerComponents = new Queue<AudioSource>();

    void MakeSingleton()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
    }

    public void ChangeBGM(string _audioClipName)
    {

    }

    public void PlaySFX(string _audioCilpName)
    {

    }

    private void Awake()
    {
        MakeSingleton();
        MakeSFXPlayers();
        audioSource = GetComponent<AudioSource>();
    }

    void MakeSFXPlayers()
    {
        for (int i = 0; i < SFXPlayerCount; i++)
        {
            GameObject sfxPlayer = new GameObject("SFXPlayer");

            sfxPlayer.transform.SetParent(transform);

            AudioSource comp = sfxPlayer.AddComponent<AudioSource>();
            sfxPlayerComponents.Enqueue(comp);
        }
    }
}
