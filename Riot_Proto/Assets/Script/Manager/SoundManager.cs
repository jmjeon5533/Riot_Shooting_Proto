using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SoundInfo
{
    public string name;
    public AudioClip clip;
}

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance { get; private set; }
    public GameObject SoundObject;
    public SoundInfo[] Sounds;

    private Dictionary<string, AudioClip> SoundDic = new();
    [Range(0, 1)]
    public float BGMVolume = 0.5f;
    [Range(0, 1)]
    public float SFXVolume = 0.5f;
    public enum SoundState
    {
        BGM,
        SFX
    }
    private void Awake()
    {
        if (instance == null) instance = this;
        else if (instance != this) Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
        for (int i = 0; i < Sounds.Length; i++)
        {
            SoundDic.Add(Sounds[i].name, Sounds[i].clip);
        }
    }
    void Start()
    {
        var playerData = SceneManager.instance.playerData;
        BGMVolume = playerData.BGMVolume;
        SFXVolume = playerData.SFXVolume;
    }
    void Update()
    {

    }
    public GameObject SetAudio(AudioClip audio, SoundState soundState, bool looping, float pitch = 1)
    {
        var sound = Instantiate(SoundObject, Camera.main.transform.position, Quaternion.identity)
        .GetComponent<AudioSource>();
        sound.pitch = pitch;
        sound.volume = soundState == SoundState.BGM ? BGMVolume : SFXVolume;
        sound.clip = audio;
        sound.GetComponent<Sound>().soundState = soundState;
        sound.loop = looping;
        sound.Play();
        if (!looping) Destroy(sound.gameObject, audio.length);
        return sound.gameObject;
    }
    public GameObject SetAudio(string audioPath, SoundState soundState, bool looping, float pitch = 1)
    {
        var sound = Instantiate(SoundObject, Camera.main.transform.position, Quaternion.identity)
        .GetComponent<AudioSource>();
        sound.pitch = pitch;
        sound.volume = soundState == SoundState.BGM ? BGMVolume : SFXVolume;
        sound.clip = SoundDic[audioPath];
        sound.GetComponent<Sound>().soundState = soundState;
        sound.loop = looping;
        sound.Play();
        if (!looping) Destroy(sound.gameObject, SoundDic[audioPath].length);
        return sound.gameObject;
    }
}
