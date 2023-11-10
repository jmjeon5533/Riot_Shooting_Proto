using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Dependencies.NCalc;
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
        for (int i = 0; i < 2; i++)
        {
            var vecY = 4 - (i * 8);
            var enemy = PoolManager.Instance.GetObject("Mage3", new Vector3(15, vecY, 0));
            GameManager.instance.curEnemys.Add(enemy);
            enemy.GetComponent<EnemyBase>().MovePos = new Vector3(8, vecY, 0);
        }
        yield return new WaitForSeconds(1f);
        var turtle = PoolManager.Instance.GetObject("Turtle2", new Vector3(15, 0, 0));
        GameManager.instance.curEnemys.Add(turtle);
        turtle.GetComponent<EnemyBase>().MovePos = new Vector3(8, 0, 0);
    }
    public override IEnumerator wave4()
    {
        for (int i = 0; i < 2; i++)
        {
            var vecY = 2 - (i * 4);
            var enemy = PoolManager.Instance.GetObject("Turtle3", new Vector3(15, vecY, 0));
            GameManager.instance.curEnemys.Add(enemy);
            enemy.GetComponent<EnemyBase>().MovePos = new Vector3(8, vecY, 0);
        }
        yield return new WaitForSeconds(1);

        for (int i = 0; i < 2; i++)
        {
            var vecY = 2 - (i * 4);
            var enemy = PoolManager.Instance.GetObject("Turtle3", new Vector3(15, vecY, 0));
            GameManager.instance.curEnemys.Add(enemy);
            enemy.GetComponent<EnemyBase>().MovePos = new Vector3(8, vecY, 0);
        }
    }
    public override IEnumerator wave5()
    {
        var enemy = PoolManager.Instance.GetObject("Mage4", new Vector3(15, 0, 0));
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
            var y = Random.Range(-5f, 5f);
            var bat = PoolManager.Instance.GetObject("Bat3", new Vector3(15, y, 0));
            GameManager.instance.curEnemys.Add(bat);
            bat.GetComponent<Bat3>().movedir = (GameManager.instance.player.transform.position +
                (Vector3)Random.insideUnitCircle - bat.transform.position).normalized;
            yield return new WaitForSeconds(0.2f);
        }
    }

    public override IEnumerator wave7()
    {
        for (int i = 0; i < 2; i++)
        {
            var vecY = 4 - (i * 8);
            var e = PoolManager.Instance.GetObject("Mage1", new Vector3(15, vecY, 0));
            GameManager.instance.curEnemys.Add(e);
            e.GetComponent<EnemyBase>().MovePos = new Vector3(7, Random.Range(-6.5f, 3.5f), 0);
        }
        yield return new WaitForSeconds(1f);

        for (int k = 0; k < 3; k++)
        {
            float firstspawnY = 3.5f;
            int spawnPosNum = Random.Range(0, 4);

            for (int i = 0; i < 5; i++)
            {
                if (spawnPosNum != i)
                {
                    var enemy = PoolManager.Instance.GetObject("Bat5", new Vector3(13 + k * 8, firstspawnY, 0), Quaternion.identity);
                    enemy.GetComponent<Bat5>().HP = 100;
                    GameManager.instance.curEnemys.Add(enemy);
                }
                firstspawnY -= 2.5f;
            }
            yield return new WaitForSeconds(1f);
        }
    }

    public override IEnumerator wave8()
    {
        
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
        for (int i = 0; i < 10; i++)
        {
            var e1 = PoolManager.Instance.GetObject("Bat6", new Vector3(13, 0, 0)).GetComponent<Bat6>();
            var e2 = PoolManager.Instance.GetObject("Bat6", new Vector3(13, 0, 0)).GetComponent<Bat6>();
            e2.sinLine.y *= -1;
            e2.axisHorizon = height;
            e1.axisHorizon = height;
            GameManager.instance.curEnemys.Add(e1.gameObject);
            GameManager.instance.curEnemys.Add(e2.gameObject);
            yield return new WaitForSeconds(0.5f);
        }
    }
    public override IEnumerator wave10()
    {
        var enemy = PoolManager.Instance.GetObject("Turtle4", new Vector3(15, 0, 0));
        GameManager.instance.curEnemys.Add(enemy);
        yield return new WaitForSeconds(1.5f);
        for (int i = 0; i < 2; i++)
        {
            var enemy2 = PoolManager.Instance.GetObject("Turtle4", new Vector3(15, 4 - (i * 8), 0));
            GameManager.instance.curEnemys.Add(enemy2);
            yield return new WaitForSeconds(1.5f);
        }
    }

    public override IEnumerator wave11()
    {
        for (int i = 0; i < 2; i++)
        {
            var vecY = 4 - (i * 8);
            var e = PoolManager.Instance.GetObject("Mage1", new Vector3(15, vecY, 0));
            var enemy1 = PoolManager.Instance.GetObject("Bat6", new Vector3(13, 5, 0)).GetComponent<Bat6>();
            var enemy2 = PoolManager.Instance.GetObject("Bat6", new Vector3(13, -5, 0)).GetComponent<Bat6>();
            enemy1.sinLine.y *= -2;
            enemy2.sinLine.y *= 2;
            enemy2.axisHorizon = 0;
            enemy1.axisHorizon = 0;
            GameManager.instance.curEnemys.Add(e);
            GameManager.instance.curEnemys.Add(enemy1.gameObject);
            GameManager.instance.curEnemys.Add(enemy2.gameObject);
            e.GetComponent<EnemyBase>().MovePos = new Vector3(7, Random.Range(-6.5f, 3.5f), 0);
        }

        Vector2 vecPlayer = GameManager.instance.player.transform.position;
        var eo = PoolManager.Instance.GetObject("Spider2", new Vector3(15, vecPlayer.y, 0));
        GameManager.instance.curEnemys.Add(eo);
        yield return new WaitForSeconds(1f);
    }

    public override IEnumerator wave12()
    {
        PoolManager.Instance.GetObject("Mage2", new Vector3(15, 0, 0));
        //PoolManager.Instance.GetObject("Mage2", new Vector3(15, 0, 0));
        for (int i = 0; i < 3; i++)
        {
            var alert = PoolManager.Instance.GetObject("Alert", new Vector3(0, 0, 0),Quaternion.Euler(0,0,Random.Range(-150, -50))).GetComponent<Alert>();
            GameManager.instance.curEnemys.Add(alert.gameObject);
            yield return new WaitForSeconds(4f);
        }
        yield return new WaitForSeconds(1f);
        PoolManager.Instance.GetObject("Mage2", new Vector3(15, 0, 0));
        for (int i = 0; i < 2; i++)
        { 
            var alert = PoolManager.Instance.GetObject("Alert", new Vector3(0, 0, 0), Quaternion.Euler(0, 0, Random.Range(-150, -50))).GetComponent<Alert>();
            GameManager.instance.curEnemys.Add(alert.gameObject);
            yield return new WaitForSeconds(2f);
        }
        yield return new WaitForSeconds(1f);
    }

    public override IEnumerator wave13()
    {
        
        yield return new WaitForSeconds(0.5f);
    }
    public override IEnumerator wave14()
    {
        yield return new WaitForSeconds(1f);
    }

    public override IEnumerator wave15()
    {
        var enemy = PoolManager.Instance.GetObject("SpinTurtle", new Vector3(15, 0, 0)).GetComponent<SpinTurtle>();
        enemy.MovePos = Vector3.zero;
        GameManager.instance.curEnemys.Add(enemy.gameObject);
        yield return new WaitForSeconds(1f);
    }
}
