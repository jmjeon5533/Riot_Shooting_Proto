using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _ElectricBullet : BulletBase
{
    // Start is called before the first frame update
    [SerializeField] Vector3 offset;

    [SerializeField] Vector3 pointA;
    [SerializeField] Vector3 pointB;
    [SerializeField] Vector3 pointC;

    [SerializeField] float lateRadius;

    void Start()
    {
        dir = transform.right;
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        var hit = Physics.OverlapSphere(transform.position, radius);
        foreach (var h in hit)
        {
            if (h.CompareTag("Enemy"))
            {

                h.GetComponent<EnemyBase>().Damage((Random.Range(0, 100f) <= CritRate)
                    ? (int)(Damage * CritDamage) : Damage);
                RadiusDamage();
            }
        }
    }

    void RadiusDamage()
    {
        pointA = transform.position;
        pointB = transform.position + pointB;
        pointC = transform.position + pointC;
        List<GameObject> hits = DetectObjectsInTriangle(pointA, pointB, pointC);
        if(hits != null)
        {
            foreach(var h in hits)
            {
                if (h.CompareTag("Enemy"))
                {

                    h.GetComponent<EnemyBase>().Damage((Random.Range(0, 100f) <= CritRate)
                        ? (int)(Damage * CritDamage) : Damage);
                }
            } 
        }
        Destroy(gameObject);
    }

    List<GameObject> DetectObjectsInTriangle(Vector3 pointA, Vector3 pointB, Vector3 pointC)
    {
        Collider[] colliders = Physics.OverlapBox(transform.position + new Vector3(offset.x ,0,0), offset, Quaternion.identity); // 더미 값, 적절한 크기로 수정
        Debug.Log(colliders.Length);
        List<GameObject> detectedObjects = new List<GameObject>();

        foreach (Collider collider in colliders)
        {
            // 만약 감지된 오브젝트의 위치가 세모의 영역 내에 있는지 검사
            if (IsPointInTriangle(collider.transform.position, pointA, pointB, pointC))
            {
                detectedObjects.Add(collider.gameObject);
                Debug.Log(collider.name);
            }
        }
        Debug.Log(detectedObjects.Count);
        return detectedObjects;
    }

    // 점이 삼각형 안에 있는지 검사하는 함수
    bool IsPointInTriangle(Vector3 point, Vector3 a, Vector3 b, Vector3 c)
    {
        point = new Vector3 (point.x, point.y, 0);
        Vector3 v0 = c - a;
        Vector3 v1 = b - a;
        Vector3 v2 = point - a;
        v0.z = 0;
        v1.z = 0;
        v2.z = 0;
        float dot00 = Vector3.Dot(v0, v0);
        float dot01 = Vector3.Dot(v0, v1);
        float dot02 = Vector3.Dot(v0, v2);
        float dot11 = Vector3.Dot(v1, v1);
        float dot12 = Vector3.Dot(v1, v2);

        float invDenom = 1 / (dot00 * dot11 - dot01 * dot01);
        float u = (dot11 * dot02 - dot01 * dot12) * invDenom;
        float v = (dot00 * dot12 - dot01 * dot02) * invDenom;

        return (u >= 0) && (v >= 0) && (u + v <= 1);
    }
}
