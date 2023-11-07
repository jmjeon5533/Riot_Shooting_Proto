using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveExcuter : MonoBehaviour
{
    public static WaveExcuter instance {get; private set;}
    public WaveScript[] waveScripts;
    void Start()
    {
        instance = this;
        foreach (var waveScript in waveScripts)
        {
            //waveScript.Waves.Add(waveScript.wave1);
            //waveScript.Waves.Add(waveScript.wave2);
            //waveScript.Waves.Add(waveScript.wave3);
            //waveScript.Waves.Add(waveScript.wave4);
            //waveScript.Waves.Add(waveScript.wave5);
            //waveScript.Waves.Add(waveScript.wave6);
            //Extra Wave
            waveScript.Waves.Add(waveScript.wave7);
            //waveScript.Waves.Add(waveScript.wave8);
            waveScript.Waves.Add(waveScript.wave9);
            //waveScript.Waves.Add(waveScript.wave10);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
