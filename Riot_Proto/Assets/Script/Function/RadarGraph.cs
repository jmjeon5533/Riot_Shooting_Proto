using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class RadarGraph : MonoBehaviour
{
    [SerializeField] Mesh mesh;
    CanvasRenderer canvasRenderer;
    [SerializeField] Material material;
    [SerializeField] float size;
    [SerializeField, Range(0,1)] float[] stats;
    [SerializeField] float[] tempStats;



    public void InitRaderGraph()
    {
        
        canvasRenderer = GetComponent<CanvasRenderer>();
        tempStats = new float[stats.Length];
        for(int i = 0; i < stats.Length; i++)
        {
            tempStats[i] = 0;
        }
        mesh = new Mesh();
        int len = stats.Length;
        Vector3[] vertexs = new Vector3[len];

        float radius = 360 / len;

        for(int i = 0; i < len; i++)
        {
            vertexs[i] = Quaternion.Euler(0,0,i * radius) * Vector3.up * size * tempStats[i];
        }
        
        int[] triangles = new int[(len-2) * 3];

        for (int i = 0; i < (len-2); i++)
        {
            int verIndex = i * 3;
            triangles[verIndex] = 0;
            triangles[verIndex + 1] = 1+i;
            triangles[verIndex+2] = 2+i;
        }
        
        mesh.vertices = vertexs;
        mesh.triangles = triangles;
        canvasRenderer.SetMesh(mesh);
        canvasRenderer.SetMaterial(material,null);
        Sequence s = DOTween.Sequence();
        for(int i = 0; i < len;i++)
        {
            //s.Append(DOVirtual.Float(0, stats[i], 1f, v => SetFloat(i, v)));
            StartCoroutine(TowardsRader(i));
        }
        //s.Play();

    }

    IEnumerator RemoveRader(int index)
    {
        //float plus = stats[index] / 60;
        float plus = Time.deltaTime * 4;
        while (tempStats[index] > 0)
        {
            tempStats[index] -= plus;
            if (tempStats[index] < 0)
            {
                tempStats[index] = 0;
            }
            UpdateRadar();
            yield return null;
        }
        
    }

    public void ResetRadar()
    {
        canvasRenderer = GetComponent<CanvasRenderer>();
        tempStats = new float[stats.Length];
        for (int i = 0; i < stats.Length; i++)
        {
            tempStats[i] = stats[i];
        }
        mesh = new Mesh();
        int len = stats.Length;
        Vector3[] vertexs = new Vector3[len];

        float radius = 360 / len;

        for (int i = 0; i < len; i++)
        {
            vertexs[i] = Quaternion.Euler(0, 0, i * radius) * Vector3.up * 0 * tempStats[i];
        }

        int[] triangles = new int[(len - 2) * 3];

        for (int i = 0; i < (len - 2); i++)
        {
            int verIndex = i * 3;
            triangles[verIndex] = 0;
            triangles[verIndex + 1] = 1 + i;
            triangles[verIndex + 2] = 2 + i;
        }

        mesh.vertices = vertexs;
        mesh.triangles = triangles;
        canvasRenderer.SetMesh(mesh);
        canvasRenderer.SetMaterial(material, null);
        
    }

    public void DisableRadar()
    {
        canvasRenderer = GetComponent<CanvasRenderer>();
        tempStats = new float[stats.Length];
        for (int i = 0; i < stats.Length; i++)
        {
            tempStats[i] = stats[i];
        }
        mesh = new Mesh();
        int len = stats.Length;
        Vector3[] vertexs = new Vector3[len];

        float radius = 360 / len;

        for (int i = 0; i < len; i++)
        {
            vertexs[i] = Quaternion.Euler(0, 0, i * radius) * Vector3.up * size * tempStats[i];
        }

        int[] triangles = new int[(len - 2) * 3];

        for (int i = 0; i < (len - 2); i++)
        {
            int verIndex = i * 3;
            triangles[verIndex] = 0;
            triangles[verIndex + 1] = 1 + i;
            triangles[verIndex + 2] = 2 + i;
        }

        mesh.vertices = vertexs;
        mesh.triangles = triangles;
        canvasRenderer.SetMesh(mesh);
        canvasRenderer.SetMaterial(material, null);
        for (int i = 0; i < len; i++)
        {
            //s.Append(DOVirtual.Float(0, stats[i], 1f, v => SetFloat(i, v)));
            StartCoroutine(RemoveRader(i));
        }
       
        
    }


    IEnumerator TowardsRader(int index)
    {
        //float plus = stats[index] / 60;
        float plus = Time.deltaTime * 2;
        while (tempStats[index] <= stats[index])
        {
            tempStats[index] += plus;
            if (tempStats[index] > stats[index])
            {
                tempStats[index] = stats[index];
            }
            UpdateRadar();
            yield return null;
        }
    }

    void UpdateRadar()
    {
        mesh = new Mesh();
        
        int len = stats.Length;
        Vector3[] vertexs = new Vector3[len];

        float radius = 360 / len;

        for (int i = 0; i < len; i++)
        {
            vertexs[i] = Quaternion.Euler(0, 0, i * radius) * Vector3.up * size * tempStats[i];
        }

        int[] triangles = new int[(len - 2) * 3];

        for (int i = 0; i < (len - 2); i++)
        {
            int verIndex = i * 3;
            triangles[verIndex] = 0;
            triangles[verIndex + 1] = 1 + i;
            triangles[verIndex + 2] = 2 + i;
        }

        mesh.vertices = vertexs;
        mesh.triangles = triangles;
        canvasRenderer.SetMesh(mesh);
        canvasRenderer.SetMaterial(material, null);
    }

    private void Update()
    {
        //UpdateRadar();
    }

    private void Start()
    {
        //InitRaderGraph();
    }

}
