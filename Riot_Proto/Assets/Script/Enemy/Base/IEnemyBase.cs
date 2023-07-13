using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEnemyBase
{
    public void Attack();
    public void Damaged(int value);
    public void Death();
    public void Movement();
    public void Initialize();

    public int GetHP();
    public void SetHP(int value);

}
