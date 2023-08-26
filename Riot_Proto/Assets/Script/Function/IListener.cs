using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    public enum Event_Type
    {
        PlayerAttack, PlayerDeath
    }
public interface IListener
{

    public void OnEvent(Event_Type type, Component sender, object param = null);
}
