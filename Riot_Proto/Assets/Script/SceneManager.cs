using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManager : MonoBehaviour
{
    public static SceneManager instance {get; private set;}
    public int CharIndex; //캐릭터 번호
    public int StageIndex; //스테이지 번호

    private void Awake() {
        if(instance == null) instance = this;
        else Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
    }
    public void StageStart()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Main");
    }
}
