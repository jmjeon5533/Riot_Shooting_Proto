using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Stage1", menuName = "WaveScript/wave1", order = 0)]
public class Stage1 : WaveScript
{
    public override IEnumerator wave1()
    {
        yield return new WaitForSeconds(1);
        for (int i = 0; i < 20; i++)
        {
            for (int j = 0; j < Random.Range(1, 3); j++)
            {
                var enemy = PoolManager.Instance.GetObject("Bat4", new Vector3(15, 4, 0), Quaternion.identity);
                GameManager.instance.curEnemys.Add(enemy);
                enemy.GetComponent<EnemyBase>().Init();
            }
            yield return new WaitForSeconds(0.06f);
        }
    }
    public override IEnumerator wave2()
    {
        var e = PoolManager.Instance.GetObject("Golem1", new Vector3(15, 0, 0), Quaternion.identity);
        GameManager.instance.curEnemys.Add(e);
        e.GetComponent<EnemyBase>().MovePos = new Vector3(3, 0, 0);
        e.GetComponent<Golem1>().IsShield = false;
        
        yield return new WaitForSeconds(0.5f);
        for (int i = 0; i < 2; i++)
        {
            var vecY = 3 - (i * 6);
            var enemy = PoolManager.Instance.GetObject("Golem1", new Vector3(15, vecY, 0), Quaternion.identity);
            GameManager.instance.curEnemys.Add(enemy);
            enemy.GetComponent<EnemyBase>().MovePos = new Vector3(3, vecY, 0);
            enemy.GetComponent<Golem1>().IsShield = false;
            //enemy.GetComponent<Golem1>().SetBulletSpeed(3 + i);
        }
        yield return new WaitForSeconds(2);
        for (int i = 0; i < 2; i++)
        {
            var vecY = 2 - (i * 4);
            var enemy = PoolManager.Instance.GetObject("Golem1", new Vector3(15, vecY, 0), Quaternion.identity);
            GameManager.instance.curEnemys.Add(enemy);
            enemy.GetComponent<EnemyBase>().MovePos = new Vector3(8, vecY, 0);
            enemy.GetComponent<Golem1>().IsShield = false;
            //enemy.GetComponent<Golem1>().SetBulletSpeed(2 + i);
        }
    }
    public override IEnumerator wave3()
    {
        for(int i = 0; i < 2; i++)
        {
            var vecY = 4 - (i * 8);
            var enemy = PoolManager.Instance.GetObject("Mage3", new Vector3(15, vecY,0));
            GameManager.instance.curEnemys.Add(enemy);
            enemy.GetComponent<EnemyBase>().MovePos = new Vector3(8, vecY, 0);
        }
        yield return new WaitForSeconds(1f);
        var turtle = PoolManager.Instance.GetObject("Turtle2",new Vector3(15,0,0));
        GameManager.instance.curEnemys.Add(turtle);
        turtle.GetComponent<EnemyBase>().MovePos = new Vector3(8, 0, 0);
    }
    public override IEnumerator wave4()
    {
        for(int i = 0; i < 2; i++)
        {
            var vecY = 2 - (i * 4);
            var enemy = PoolManager.Instance.GetObject("Turtle3", new Vector3(15, vecY,0));
            GameManager.instance.curEnemys.Add(enemy);
            enemy.GetComponent<EnemyBase>().MovePos = new Vector3(8, vecY, 0);
        }
        yield return new WaitForSeconds(1);
        
        for(int i = 0; i < 2; i++)
        {
            var vecY = 2 - (i * 4);
            var enemy = PoolManager.Instance.GetObject("Turtle3", new Vector3(15, vecY,0));
            GameManager.instance.curEnemys.Add(enemy);
            enemy.GetComponent<EnemyBase>().MovePos = new Vector3(8, vecY, 0);
        }
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
