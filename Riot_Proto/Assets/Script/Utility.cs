using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Utility
{
    public static void Invoke(this MonoBehaviour mb, System.Action action, float duration, bool setUpdate = false)
        => mb.StartCoroutine(InvokeAction(action, duration, setUpdate));

    private static IEnumerator InvokeAction(System.Action action, float duration, bool setUpdate)
    {
        yield return setUpdate ? new WaitForSecondsRealtime(duration) : new WaitForSeconds(duration);
        action?.Invoke();
    }
}
