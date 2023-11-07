using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Stage1", menuName = "WaveScript/wave1", order = 0)]
public class Stage1 : WaveScript
{
    public override IEnumerator wave1()
    {
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
        var enemy = PoolManager.Instance.GetObject("Mage4", new Vector3(15,0,0));
        GameManager.instance.curEnemys.Add(enemy);
        enemy.GetComponent<EnemyBase>().MovePos = new Vector3(8, 0, 0);
        enemy.GetComponent<Mage4>().batSpawn();
        yield return null;    
    }
    public override IEnumerator wave6()
    {
        var enemy = PoolManager.Instance.GetObject("Golem1", new Vector3(15, 0, 0));
        GameManager.instance.curEnemys.Add(enemy);
        enemy.GetComponent<EnemyBase>().MovePos = new Vector3(8, 0, 0);
        enemy.GetComponent<Golem1>().IsShield = true;
        yield return new WaitForSeconds(3);
        Debug.Log(6);
        for (int i = 0; i < 15; i++)
        {
            var y = Random.Range(-5f,5f);
            var bat = PoolManager.Instance.GetObject("Bat3", new Vector3(15, y, 0));
            GameManager.instance.curEnemys.Add(bat);
            bat.GetComponent<Bat3>().movedir = (GameManager.instance.player.transform.position + 
                (Vector3)Random.insideUnitCircle - bat.transform.position).normalized;
            yield return new WaitForSeconds(0.2f);
        }
    }

    //Extra Wave
    public override IEnumerator wave7()
    {
        yield return new WaitForSeconds(1.8f);
        for (int i = 0; i < 2; i++)
        {
            var vecY = 4 - (i * 8);
            var e = PoolManager.Instance.GetObject("Mage1", new Vector3(15, vecY, 0));
            GameManager.instance.curEnemys.Add(e);
            e.GetComponent<EnemyBase>().MovePos = new Vector3(7, Random.Range(-6.5f, 3.5f), 0);
        }
        yield return new WaitForSeconds(1f);
        //var enemy = PoolManager.Instance.GetObject("Mage5", new Vector3(15, 0, 0));
        //GameManager.instance.curEnemys.Add(enemy);
        //enemy.GetComponent<EnemyBase>().MovePos = new Vector3(8, 0, 0);
        float firstspawnY = 3.5f;
        for (int k = 0; k < 3; k++)
        {
            int spawnPosNum = Random.Range(0, 4);

            for (int i = 0; i < 5; i++)
            {
                if (spawnPosNum != i)
                {
                    var enemy = PoolManager.Instance.GetObject("Bat5", new Vector3(13 + k * 5, firstspawnY, 0), Quaternion.identity);
                    enemy.GetComponent<Bat5>().HP = 100;
                    GameManager.instance.curEnemys.Add(enemy);
                }
                firstspawnY -= 2.5f;
            }

            yield return new WaitForSeconds(1f);
        }

        yield return new WaitForSeconds(2f);
    }

    public override IEnumerator wave8()
    {
        var enemy = PoolManager.Instance.GetObject("Mage6", new Vector3(15, 0, 0));
        GameManager.instance.curEnemys.Add(enemy);

        yield return new WaitForSeconds(1f);
    }
    public override IEnumerator wave9()
    {
        for (int i = 0; i < 2; i++)
        {
            var vecY = 4 - (i * 8);
            var e = PoolManager.Instance.GetObject("Mage1", new Vector3(15, vecY, 0));
            GameManager.instance.curEnemys.Add(e);
            e.GetComponent<EnemyBase>().MovePos = new Vector3(7, Random.Range(-6.5f, 3.5f), 0);
        }

        float height = GameManager.instance.player.transform.position.y;
        for (int i = 0; i < 20; i++)
        {
            var enemy = PoolManager.Instance.GetObject("Bat6", new Vector3(13, 0, 0)).GetComponent<Bat6>();
            var e = PoolManager.Instance.GetObject("Bat6", new Vector3(13, 0, 0)).GetComponent<Bat6>();
            e.sinLine.y *= -1;
            e.axisHorizon = height;
            enemy.axisHorizon = height;
            yield return new WaitForSeconds(0.5f);
        }
        yield return new WaitForSeconds(1f);
    }

    public override IEnumerator wave11()
    {
        for (int i = 0; i < 2; i++)
        {
            var vecY = 4 - (i * 8);
            var e = PoolManager.Instance.GetObject("Mage1", new Vector3(15, vecY, 0));
            GameManager.instance.curEnemys.Add(e);
            e.GetComponent<EnemyBase>().MovePos = new Vector3(7, Random.Range(-6.5f, 3.5f), 0);
        }

        var eo = PoolManager.Instance.GetObject("Spider2", new Vector3(15, 0, 0));
        GameManager.instance.curEnemys.Add(eo);
        yield return new WaitForSeconds(1f);
    }
}
