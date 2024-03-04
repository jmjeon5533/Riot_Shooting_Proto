using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveExcuter : MonoBehaviour
{
    public static WaveExcuter instance {get; private set;}
    public WaveScript[] waveScripts;
    void Awake()
    {
        instance = this;
        foreach (var waveScript in waveScripts)
        {
            waveScript.Waves.Clear();

            waveScript.Waves.Add(waveScript.wave1);
            waveScript.Waves.Add(waveScript.wave2);
            waveScript.Waves.Add(waveScript.wave3);
            waveScript.Waves.Add(waveScript.wave4);
            waveScript.Waves.Add(waveScript.wave5);
            waveScript.Waves.Add(waveScript.wave6);
            //waveScript.Waves.Add(waveScript.wave7); //
            waveScript.Waves.Add(waveScript.wave8);
            waveScript.Waves.Add(waveScript.wave9);
            waveScript.Waves.Add(waveScript.wave10);
            waveScript.Waves.Add(waveScript.wave11);
            waveScript.Waves.Add(waveScript.wave12);
            waveScript.Waves.Add(waveScript.wave13);
            waveScript.Waves.Add(waveScript.wave14);
            waveScript.Waves.Add(waveScript.wave15);

            //8, 13, 15
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
