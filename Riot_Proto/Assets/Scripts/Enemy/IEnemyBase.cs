using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEnemyBase
{
    void Attack();

    public void Damaged(int damage);

    public void Death();

    public void Movement();

    public void Initalize();
}
