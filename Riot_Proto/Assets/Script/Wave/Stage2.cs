using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Stage2", menuName = "WaveScript/wave2", order = 0)]
public class Stage2 : WaveScript
{
    void Start()
    {
        Waves.Add(wave1);
    }
    void wave1()
    {

    }
}
