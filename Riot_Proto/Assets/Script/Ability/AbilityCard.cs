using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class AbilityCard : MonoBehaviour
{
    public static AbilityCard Instance { get; private set; }

    public List<AbilityBase> abilities;

    public List<AbilityBase> curAbilityList;

    public Transform[] cards;

    [SerializeField] Transform targetPos;

    [SerializeField] float width;

    public bool isSelect = false;

    // Start is called before the first frame update
    void Start()
    {
        if(Instance == null) Instance = this;
        else Destroy(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Select();
        }
    }

    private void Select()
    {
        StartCoroutine(ISelect(false));
    }

    IEnumerator ISelect(bool end)
    {
        for (int i = 0; i < cards.Length; i++)
        {
            if (!end)
            {
                cards[i].GetComponent<Select>().SetAbility(abilities[Random.Range(0, abilities.Count)]);
                StartCoroutine(ICardSpawn(cards[i], cards[i].position, new Vector3(cards[i].position.x, 200, 0), 1f));
            }
            else StartCoroutine(ICardEnd(cards[i], cards[i].position, new Vector3(cards[i].position.x, -550, 0), 1f));
            yield return new WaitForSeconds(0.15f);
        }
        yield return new WaitForSeconds(1.5f);
        isSelect = true;
    }

    IEnumerator ICardSpawn(Transform t, Vector3 startPos, Vector3 endPos, float duration)
    {
        t.position = startPos;
        yield return t.DOMove(endPos + transform.up * 70, duration).WaitForCompletion();
        t.DOMove(endPos, duration / 2);

    }

    IEnumerator ICardEnd(Transform t, Vector3 startPos, Vector3 endPos, float duration)
    {
        t.position = startPos;
        yield return t.DOMove(startPos + Vector3.up * 70, duration / 2).WaitForCompletion();
        t.DOMove(endPos, duration).WaitForCompletion();


    }

    public void SelectEnd()
    {
        isSelect = false;
        StartCoroutine(ISelect(true));
    }

}




