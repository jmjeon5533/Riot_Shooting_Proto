using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Dependencies.NCalc;
using UnityEngine;

[CreateAssetMenu(fileName = "Stage1", menuName = "WaveScript/wave1", order = 0)]
public class Stage1 : WaveScript
{
    public override IEnumerator wave1()
    {
        Debug.Log(1);
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
        Debug.Log(2);
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
        Debug.Log(3);
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
        Debug.Log(4);
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
        Debug.Log(5);
        var enemy = PoolManager.Instance.GetObject("Mage4", new Vector3(15, 0, 0));
        GameManager.instance.curEnemys.Add(enemy);
        enemy.GetComponent<EnemyBase>().MovePos = new Vector3(8, 0, 0);
        enemy.GetComponent<Mage4>().batSpawn();
        yield return null;
    }
    public override IEnumerator wave6()
    {
        Debug.Log(6);
        var enemy = PoolManager.Instance.GetObject("Golem1", new Vector3(15, 0, 0));
        GameManager.instance.curEnemys.Add(enemy);
        enemy.GetComponent<EnemyBase>().MovePos = new Vector3(8, 0, 0);
        enemy.GetComponent<Golem1>().IsShield = true;
        yield return new WaitForSeconds(3);
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
        Debug.Log(7);
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
                    var enemy = PoolManager.Instance.GetObject("Bat5", new Vector3(15 + k * 1.5f, firstspawnY, 0), Quaternion.identity);
                    enemy.GetComponent<Bat5>().HP = 100;
                    enemy.GetComponent<Bat5>().MoveSpeed = 8;
                    enemy.GetComponent<Bat5>().WaitAttack();
                    GameManager.instance.curEnemys.Add(enemy);
                }
                firstspawnY -= 2.5f;
            }
            yield return new WaitForSeconds(0.2f);
        }
    }

    public override IEnumerator wave8()
    {
        Debug.Log(8);
        var enemy1 = PoolManager.Instance.GetObject("Spider3", new Vector3(15, -1, 0));
        var enemy2 = PoolManager.Instance.GetObject("Spider3", new Vector3(15, -1, 0));
        enemy2.GetComponent<Spider3>().sinLine.y *= -1;
        //var enemy = PoolManager.Instance.GetObject("Turtle4", new Vector3(15, -1, 0));
        //enemy.GetComponent<Turtle4>().MovePos = new Vector3(3, -1, 0);
        GameManager.instance.curEnemys.Add(enemy1);
        yield return new WaitForSeconds(1f);
    }
    public override IEnumerator wave9()
    {
        Debug.Log(9);
        for (int i = 0; i < 2; i++)
        {
            var vecY = 4 - (i * 8);
            var e = PoolManager.Instance.GetObject("Mage1", new Vector3(15, vecY, 0));
            e.GetComponent<Mage1>().HP = 40;
            GameManager.instance.curEnemys.Add(e);
            e.GetComponent<EnemyBase>().MovePos = new Vector3(7, Random.Range(-6.5f, 3.5f), 0);
            yield return new WaitForSeconds(1f);
        }
        
        float height = -1.5f;
        for (int i = 0; i < 10; i++)
        {
            var e1 = PoolManager.Instance.GetObject("Bat6", new Vector3(13, 0, 0)).GetComponent<Bat6>();
            var e2 = PoolManager.Instance.GetObject("Bat6", new Vector3(13, 0, 0)).GetComponent<Bat6>();
            e2.sinLine.y *= -1;
            e2.axisHorizon = height;
            e1.axisHorizon = height;
            e1.MoveSpeed = e2.MoveSpeed = 8f;
            GameManager.instance.curEnemys.Add(e1.gameObject);
            GameManager.instance.curEnemys.Add(e2.gameObject);
            yield return new WaitForSeconds(0.5f);
        }
    }
    public override IEnumerator wave10()
    {
        Debug.Log(10);
        var enemy = PoolManager.Instance.GetObject("Turtle5", new Vector3(15, 0, 0));
        GameManager.instance.curEnemys.Add(enemy);
        yield return new WaitForSeconds(1.5f);
        for (int i = 0; i < 2; i++)
        {
            var enemy2 = PoolManager.Instance.GetObject("Turtle5", new Vector3(15, 4 - (i * 8), 0));
            GameManager.instance.curEnemys.Add(enemy2);
            yield return new WaitForSeconds(1.5f);
        }
    }

    public override IEnumerator wave11()
    {
        Debug.Log(11);
        for (int i = 0; i < 2; i++)
        {
            var vecY = 4 - (i * 8);
            var e = PoolManager.Instance.GetObject("Mage1", new Vector3(15, vecY, 0));
            var enemy1 = PoolManager.Instance.GetObject("Bat6", new Vector3(13, 5, 0)).GetComponent<Bat6>();
            var enemy2 = PoolManager.Instance.GetObject("Bat6", new Vector3(13, -5, 0)).GetComponent<Bat6>();
            enemy1.sinLine.y *= -0.75f;
            enemy2.sinLine.y *= 0.75f;
            enemy2.axisHorizon = 0;
            enemy1.axisHorizon = 0;
            GameManager.instance.curEnemys.Add(e);
            GameManager.instance.curEnemys.Add(enemy1.gameObject);
            GameManager.instance.curEnemys.Add(enemy2.gameObject);
            e.GetComponent<EnemyBase>().MovePos = new Vector3(7, Random.Range(-6.5f, 3.5f), 0);
            yield return new WaitForSeconds(1f);
        }

        Vector2 vecPlayer = GameManager.instance.player.transform.position;
        var eo = PoolManager.Instance.GetObject("Spider2", new Vector3(15, vecPlayer.y, 0));
        GameManager.instance.curEnemys.Add(eo);
        yield return new WaitForSeconds(1f);
    }

    public override IEnumerator wave12()
    {
        Debug.Log(12);
        GameManager.instance.curEnemys.Add(PoolManager.Instance.GetObject("Mage2", new Vector3(15, 0, 0)));
        //PoolManager.Instance.GetObject("Mage2", new Vector3(15, 0, 0));
        for (int i = 0; i < 1; i++)
        {
            var alert = PoolManager.Instance.GetObject("Alert", new Vector3(0, 0, 0),Quaternion.Euler(0,0,Random.Range(-150, -50))).GetComponent<Alert>();
            //GameManager.instance.curEnemys.Add(alert.gameObject);
            yield return new WaitForSeconds(4f);
        }
        yield return new WaitForSeconds(1f);
        GameManager.instance.curEnemys.Add(PoolManager.Instance.GetObject("Mage2", new Vector3(15, 0, 0)));
        for (int i = 0; i < 2; i++)
        { 
            var alert = PoolManager.Instance.GetObject("Alert", new Vector3(0, 0, 0), Quaternion.Euler(0, 0, Random.Range(-150, -50))).GetComponent<Alert>();
            //GameManager.instance.curEnemys.Add(alert.gameObject);
            yield return new WaitForSeconds(2f);
        }
        yield return new WaitForSeconds(6f);
    }

    public override IEnumerator wave13()
    {
        Debug.Log(13);
        for (int i = 0; i < 3; i++)
        {
            var vecY = 4 - (i * 8);
            var enemy = PoolManager.Instance.GetObject("Mage3", new Vector3(15, vecY, 0));
            GameManager.instance.curEnemys.Add(enemy);
            enemy.GetComponent<EnemyBase>().MovePos = new Vector3(8, vecY, 0);
        }
        Vector2 vecPlayer = GameManager.instance.player.transform.position;
        var eo = PoolManager.Instance.GetObject("Spider2", new Vector3(15, vecPlayer.y+1, 0));
        GameManager.instance.curEnemys.Add(eo);
        eo = PoolManager.Instance.GetObject("Spider2", new Vector3(15, vecPlayer.y-1, 0));
        GameManager.instance.curEnemys.Add(eo);
        yield return new WaitForSeconds(1f);
    }
    public override IEnumerator wave14()
    {
        Debug.Log(14);
        var enemy = PoolManager.Instance.GetObject("Golem4", new Vector3(15, 0, 0));
        enemy.GetComponent<Golem4>().MovePos = new Vector3(7, -1, 0);
        enemy.GetComponent<Golem4>().batSpawn();
        GameManager.instance.curEnemys.Add(enemy);
        yield return new WaitForSeconds(1f);
    }

    public override IEnumerator wave15()
    {
        Debug.Log(15);
        var enemy = PoolManager.Instance.GetObject("SpinTurtle", new Vector3(15, 0, 0)).GetComponent<SpinTurtle>();
        enemy.MovePos = Vector3.zero;
        GameManager.instance.curEnemys.Add(enemy.gameObject);
        yield return new WaitForSeconds(1f);
    }
}
