using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb1 : MonoBehaviour
{
    public string EnemyTag;
    [SerializeField] Color warningColor;
    MeshRenderer mesh;
    private void Awake()
    {
        mesh = GetComponent<MeshRenderer>();   
    }

    public void Bomb()
    {
        StartCoroutine(BombCoroutine());
    }

    private IEnumerator BombCoroutine()
    {
        yield return new WaitForSeconds(2f);

        for (int i = 0; i < 3; i++)
        {
            mesh.material.SetColor("_Color", warningColor);
            yield return new WaitForSeconds(.2f);
            mesh.material.SetColor("_Color", new Color(1, 1, 1, 1));
            yield return new WaitForSeconds(.2f);
        }

        for(int i = 0; i < 8; i++)
        {
            var b = PoolManager.Instance.GetObject("EnemyBullet", transform.position, Quaternion.identity).GetComponent<BulletBase>();
            float angle = i * Mathf.Deg2Rad;
            Vector3 direction = new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0); // 라디안 각도로 방향 벡터 생성
            b.dir = direction;
            b.SetMoveSpeed(5f);
        }

        PoolManager.Instance.PoolObject(EnemyTag, gameObject);
        GameManager.instance.curEnemys.Remove(gameObject);
    }
}
