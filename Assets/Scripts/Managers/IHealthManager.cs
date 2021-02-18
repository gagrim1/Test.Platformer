using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHealthManager
{
    void ChangeHealth(float deltaHealth);

    void Damage(float damageValue);

    void Heal(float healValue);

    IEnumerator Death();
}
