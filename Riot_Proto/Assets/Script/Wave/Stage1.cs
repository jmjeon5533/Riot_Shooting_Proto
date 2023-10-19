using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Stage1", menuName = "WaveScript/wave1", order = 0)]
public class Stage1 : WaveScript
{
    void Start()
    {
        Waves.Add(wave1);
    }
    void wave1()
    {

    }
}
