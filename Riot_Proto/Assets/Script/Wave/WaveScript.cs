using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class WaveScript : ScriptableObject
{
    public delegate void Wavedelegate();

    public List<Wavedelegate> Waves = new List<Wavedelegate>();
}
