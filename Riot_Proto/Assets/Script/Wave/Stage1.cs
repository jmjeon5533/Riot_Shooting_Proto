using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Stage1", menuName = "WaveScript/wave1", order = 0)]
public class Stage1 : WaveScript
{
    public override IEnumerator wave1()
    {
        yield return new WaitForSeconds(1);
        for(int i = 0; i < 20; i++)
        {
            PoolManager.Instance.GetObject("Bat4",new Vector3(15,4,0),Quaternion.identity);
            yield return new WaitForSeconds(0.1f);
        }
        Debug.Log(1);
    }
    public override IEnumerator wave2()
    {
        yield return new WaitForSeconds(3);
        Debug.Log(2);
    }
    public override IEnumerator wave3()
    {
        yield return new WaitForSeconds(3);
        Debug.Log(3);
    }
    public override IEnumerator wave4()
    {
        yield return new WaitForSeconds(3);
        Debug.Log(4);
    }
    public override IEnumerator wave5()
    {
        yield return new WaitForSeconds(3);
        Debug.Log(5);
    }
    public override IEnumerator wave6()
    {
        yield return new WaitForSeconds(3);
        Debug.Log(6);
    }
}
