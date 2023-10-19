using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class WaveScript : ScriptableObject
{
    public delegate IEnumerator Wavedelegate();

    public List<Wavedelegate> Waves = new List<Wavedelegate>();
    public abstract IEnumerator wave1();
    public abstract IEnumerator wave2();
    public abstract IEnumerator wave3();
    public abstract IEnumerator wave4();
    public abstract IEnumerator wave5();
    public abstract IEnumerator wave6();

}
