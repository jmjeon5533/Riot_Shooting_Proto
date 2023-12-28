using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using DG.Tweening;
using Unity.VisualScripting;

[System.Serializable]
class RisePattern
{
    public float[] riseYs;
    public float[] riseDelay;
    bool isDuplicate = false;
}
public class Boss1 : BossBase
{

    [SerializeField] int pattern = 0;

    [SerializeField] Vector3 AttackPivot;
    [SerializeField] Vector2 AttackRange;
    //[SerializeField] Transform breathPos;

    [SerializeField] Transform r_ShootPos;
    [SerializeField] Transform l_ShootPos;

    [SerializeField] float patternDuration;
    [SerializeField] float limitY;

    Coroutine coroutine;

    [SerializeField] List<RisePattern> risePatterns = new List<RisePattern>();

    bool isDeadMotionPlay = false;

    float maxHP;

    private void OnEnable()
    {
        isDeadMotionPlay = false;
    }
    public override void Init()
    {
        base.Init();
        maxHP = HP;
    }

    protected override void Attack()
    {
        if (isDeadMotionPlay) return;
        var AState = pattern;
        anim.SetInteger("AttackState", (AState + 1) % 2);
        anim.SetTrigger("Attack");
        print(AState);
        isAttack = true;
        switch (AState)
        {
            case 0:
                {
                    coroutine = StartCoroutine(Attack1());
                    break;
                }
            case 1:
                {
                    coroutine = StartCoroutine(Attack2());
                    break;
                }
            case 2:
                {
                    coroutine = StartCoroutine(Attack3());
                    break;
                }
            case 3:
                {
                    coroutine = StartCoroutine(Attack4());
                    break;
                }
            case 4:
                {
                    coroutine = StartCoroutine(Attack5());
                    break;
                }
        }
        pattern++;
        if (pattern > 4) pattern = 0;
        AttackCooltime = Random.Range(0.5f, 1);
    }

    void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.localPosition + AttackPivot, AttackRange);
    }
    IEnumerator Attack1()
    {
        yield return new WaitForSeconds(0.5f);
        for (int i = 0; i < Random.Range(45, 60); i++)
        {
            yield return new WaitForSeconds(0.05f);
            Vector2 rand = Vector2.zero;
            while (rand.x >= 0)
            {
                rand = Random.insideUnitCircle;
            }
            Vector3 spawnPos = l_ShootPos.position;
            spawnPos.z = 0;
            var b = PoolManager.Instance.GetObject("EnemyBullet", spawnPos, Quaternion.identity).GetComponent<BulletBase>();
            b.dir = rand.normalized;
            var b2 = PoolManager.Instance.GetObject("EnemyBullet", spawnPos, Quaternion.identity).GetComponent<BulletBase>();
            b2.dir = -rand.normalized;

        }
        isAttack = false;
    }
    IEnumerator Attack2()
    {
        yield return new WaitForSeconds(0.5f);
        Vector3 spawnPos = r_ShootPos.position;
        spawnPos.z = 0;
        var b = PoolManager.Instance.GetObject("EnemyBullet", spawnPos + (Vector3.left * 0.3f), Quaternion.identity).GetComponent<EnemyBullet>();
        b.dir = Vector3.zero;
        b.transform.localScale = Vector3.one;
        Vector3 originPos = b.transform.position;
        yield return b.transform.DOMove(originPos + (Vector3.left * 3.5f), 1.5f).WaitForCompletion();
        
        yield return b.transform.DOScale(Vector3.zero, 0.1f).WaitForCompletion();
        if(!b.gameObject.activeSelf)
        {
            isAttack = false;
            yield break;
        }
        for (int i = 0; i < 15; i++)
        {
            for (int j = 0; j < 7; j++)
            {

                var tempB = PoolManager.Instance.GetObject("EnemyBullet", b.transform.position, Quaternion.identity).GetComponent<EnemyBullet>();
                tempB.SetMoveSpeed(Random.Range(4f, 5.5f));
                tempB.dir = Vector3.left;
                float _angle = (j % 2 == 0 ? Random.Range(-60, 240f) : Random.Range(120, 420f)) * Mathf.Deg2Rad; // ������ �������� ��ȯ
                Vector3 direction = new Vector3(Mathf.Cos(_angle), Mathf.Sin(_angle), 0); // ���� ������ ���� ���� ����
                tempB.dir = -direction;
            }
            yield return new WaitForSeconds(0.25f);
            //tempB.transform.rotation = Quaternion.Euler(0, Random.Range(0, 360f), 0);
        }
        PoolManager.Instance.PoolObject("EnemyBullet", b.gameObject);

        isAttack = false;
    }

    IEnumerator Attack3()
    {
        yield return new WaitForSeconds(1f);
        int angle = 0;
        for (int i = 0; i < 21; i++)
        {

            for (int j = angle; j < (360 + angle); j += 360 / 20)
            {
                var b = PoolManager.Instance.GetObject("EnemyBullet", transform.position, Quaternion.identity).GetComponent<BulletBase>();
                b.SetMoveSpeed(4.5f);
                float _angle = j * Mathf.Deg2Rad; // ������ �������� ��ȯ
                Vector3 direction = new Vector3(Mathf.Cos(_angle), Mathf.Sin(_angle), 0); // ���� ������ ���� ���� ����
                b.dir = direction;
            }
            angle += 15;
            if (angle > 360) angle = angle - 360;
            Debug.Log(angle);
            if (i > 0 && i % 10 == 0) StartCoroutine(Attack3_2());
            yield return new WaitForSeconds(0.7f);

        }
        isAttack = false;

    }

    IEnumerator Attack3_2()
    {
        Vector3 shootPos1 = transform.position + (Vector3.up * 1.2f);
        Vector3 shootPos2 = transform.position;
        Vector3 shootPos3 = transform.position + (Vector3.down * 1.2f);
        var p = GameManager.instance.player;
        for (int i = 0; i < 10; i++)
        {
            var b1 = PoolManager.Instance.GetObject("EnemyBullet", shootPos1, Quaternion.identity).GetComponent<BulletBase>();
            b1.dir = GetTargetDir(shootPos1, p.transform.position);
            var b2 = PoolManager.Instance.GetObject("EnemyBullet", shootPos2, Quaternion.identity).GetComponent<BulletBase>();
            b2.dir = GetTargetDir(shootPos2, p.transform.position);
            var b3 = PoolManager.Instance.GetObject("EnemyBullet", shootPos3, Quaternion.identity).GetComponent<BulletBase>();
            b3.dir = GetTargetDir(shootPos3, p.transform.position);
            if(HP <= maxHP)
            {
                b1.SetMoveSpeed(12);
                b2.SetMoveSpeed(12);
                b3.SetMoveSpeed(12);
            }
            yield return new WaitForSeconds(0.1f);
        }

    }

    IEnumerator Attack4_2()
    {
        Vector3 shootPos1 = transform.position + (Vector3.up * 1.7f);
        Vector3 shootPos2 = transform.position;
        Vector3 shootPos3 = transform.position + (Vector3.down * 1.7f);
        var p = GameManager.instance.player;
        for (int i = 0; i < 10; i++)
        {
            var b1 = PoolManager.Instance.GetObject("EnemyBullet", shootPos1, Quaternion.identity).GetComponent<BulletBase>();
            b1.dir = GetTargetDir(shootPos1, p.transform.position);
            var b2 = PoolManager.Instance.GetObject("EnemyBullet", shootPos2, Quaternion.identity).GetComponent<BulletBase>();
            b2.dir = GetTargetDir(shootPos2, p.transform.position);
            var b3 = PoolManager.Instance.GetObject("EnemyBullet", shootPos3, Quaternion.identity).GetComponent<BulletBase>();
            b3.dir = GetTargetDir(shootPos3, p.transform.position);
            yield return new WaitForSeconds(0.1f);
        }

    }

    IEnumerator Attack4()
    {
        var g = GameManager.instance;
        //var breath = PoolManager.Instance.GetObject("Breath", breathPos);

        int flip = 1;
        for (int i = 0; i < (int)patternDuration; i++)
        {
            if (Mathf.Abs(g.player.transform.position.x) >= 9.5f && !g.player.IsShield)
            {
                StartCoroutine(Attack4_2());
            }
            if (i % 2 == 0)
            {
                //transform.DOMoveY(limitY * flip, 1.5f);
                flip *= -1;
            }
            if (i % 5 == 0 && i > 0)
            {
                if ((maxHP / 2) >= HP)
                {
                    StartCoroutine(SpawnFireRise(Random.Range(4, 8)));

                }
                else
                {
                    StartCoroutine(SpawnFireRise(Random.Range(0, 4)));
                }

            }
            yield return new WaitForSeconds(1f);

        }
        yield return transform.DOMoveY(0, 1).WaitForCompletion();
        isAttack = false;
    }

    IEnumerator SpawnFireRise(int index)
    {
        bool isDupe = false;
        var g = GameManager.instance;
        for (int i = 0; i < risePatterns[index].riseYs.Length; i++)
        {
            if(risePatterns[index].riseDelay[i] != 0)
            {
                isDupe = false;
            }
            yield return new WaitForSeconds(risePatterns[index].riseDelay[i]);
            Debug.Log(risePatterns[index].riseDelay[i]);
            var b = PoolManager.Instance.GetObject("FireRise", new Vector3(risePatterns[index].riseYs[i], -g.MoveRange.y, 0), Quaternion.Euler(270,0,0)).GetComponent<BossSkillBullet>();
            if(!isDupe) {
                b.isSound = true;
                isDupe = true;
            }
        }
    }
    IEnumerator Attack5()
    {
        for (int i = 0; i < 720; i += 720 / 65)
        {
            var b1 = PoolManager.Instance.GetObject("EnemyBullet", transform.position, Quaternion.identity).GetComponent<BulletBase>();
            var b2 = PoolManager.Instance.GetObject("EnemyBullet2", transform.position, Quaternion.identity).GetComponent<BulletBase>();
            float angle = i * Mathf.Deg2Rad; // 각도를 라디안으로 변환
            Vector3 direction1 = new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0); // 라디안 각도로 방향 벡터 생성
            Vector3 direction2 = new Vector3(Mathf.Cos(angle), -Mathf.Sin(angle), 0);
            b1.dir = direction1; // 방향을 총알에 할당
            b2.dir = direction2; // 방향을 총알에 할당
            b1.SetMoveSpeed(6f);
            b2.SetMoveSpeed(6f);
            yield return new WaitForSeconds(0.01f);
        }
        yield return new WaitForSeconds(0.5f);
        var count = 30;
        for (int j = 0; j < 3; j++)
        {
            string bulletTag;
            if (j % 2 == 0) bulletTag = "EnemyBullet";
            else bulletTag = "EnemyBullet2";

            for (int i = 5; i < 365; i += 360 / count)
            {
                var b = PoolManager.Instance.GetObject(bulletTag, transform.position, Quaternion.identity).GetComponent<BulletBase>();
                float angle = i * Mathf.Deg2Rad; // 각도를 라디안으로 변환
                Vector3 direction = new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0); // 라디안 각도로 방향 벡터 생성
                b.dir = direction; // 방향을 총알에 할당
                b.SetMoveSpeed(7f);
            }
            yield return new WaitForSeconds(0.3f);
            count += 5;
            Debug.Log(count); 
        }
        isAttack = false;
    }

    public override void Damage(int damage, bool isCrit, string hitTag = null)
    {
        if (IsSpawning() || !GameManager.instance.IsGame) return;
        HP -= damage * damagedMultiplier;
        if (HP <= 0 && !isDeadMotionPlay)
        {
            GameManager.instance.curEnemys.Remove(this.gameObject);
            for (int i = 0; i < XPRate; i++)
            {
                Dead();
            }
            GameManager.instance.GetMoney += (int)(XPRate * 25);
            UIManager.instance.InitRate();
            StopAllCoroutines();
            StartCoroutine(DeadMotion());
        }
        GameManager.instance.GetMoney += Mathf.RoundToInt(damage * 0.5f);
        SoundManager.instance.SetAudio("Hit", SoundManager.SoundState.SFX, false, Random.Range(0.3f, 0.7f));

        var x = Random.Range(-collider.bounds.size.x / 4, collider.bounds.size.x / 4);
        var y = Random.Range(-collider.bounds.size.y / 4, collider.bounds.size.y / 4);

        var rand = new Vector3(x, y, 0);
        if(isCrit) PoolManager.Instance.GetObject("CritHit", transform.position + rand, Quaternion.identity);
            else PoolManager.Instance.GetObject("Hit", transform.position + rand, Quaternion.identity);
        // var DamageTextPos = (Vector2)transform.position + (Random.insideUnitCircle * 2);
        // var DmgText = PoolManager.Instance.GetObject("DamageText", UIManager.instance.canvas)
        //     .GetComponent<DamageText>();
        // DmgText.rect.position = DamageTextPos;
        // DmgText.text.text = damage.ToString();
        // DmgText.timeCount = 1 + (damage * 0.01f);
        // DmgText.text.color = Color.white;

    }



    IEnumerator DeadMotion()
    {

        isDeadMotionPlay = true;

        GameManager.instance.SetCameraShake(7, 0.09f);
        for (int i = 0; i < 9; i++)
        {
            Vector3 exPos = Random.insideUnitSphere;
            exPos.z = -3;
            PoolManager.Instance.GetObject("Explosion", transform.position + (exPos * 1.3f), Quaternion.identity);
            if (i == 5) transform.DOMoveX(15, 4);
            yield return new WaitForSeconds(0.75f);
        }
        PoolManager.Instance.PoolObject(EnemyTag, gameObject);
    }

    Vector3 GetTargetDir(Vector3 origin, Vector3 target)
    {
        Vector3 dir = target - origin;
        dir = dir.normalized;
        return dir;
    }
}
