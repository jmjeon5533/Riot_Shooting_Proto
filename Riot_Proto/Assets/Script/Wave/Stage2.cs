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
    public override IEnumerator wave1()
    {
        yield return new WaitForSeconds(3);
        Debug.Log(11);
    }
    public override IEnumerator wave2()
    {
        yield return new WaitForSeconds(3);
        Debug.Log(12);
    }
    public override IEnumerator wave3()
    {
        yield return new WaitForSeconds(3);
        Debug.Log(13);
    }
    public override IEnumerator wave4()
    {
        yield return new WaitForSeconds(3);
        Debug.Log(14);
    }
    public override IEnumerator wave5()
    {
        yield return new WaitForSeconds(3);
        Debug.Log(15);
    }
    public override IEnumerator wave6()
    {
        yield return new WaitForSeconds(3);
        Debug.Log(16);
    }

    //Extra Wave
    public override IEnumerator wave7()
    {
        yield return new WaitForSeconds(3);
        Debug.Log(17);
    }

    public override IEnumerator wave8()
    {
        yield return new WaitForSeconds(3);
        Debug.Log(18);
    }
    public override IEnumerator wave9()
    {
        yield return new WaitForSeconds(3);
        Debug.Log(18);
    }
    public override IEnumerator wave10()
    {
        yield return new WaitForSeconds(3);
        Debug.Log(18);
    }
}
