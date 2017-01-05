using UnityEngine;
using System.Collections;
using System;

public class Combat : MonoBehaviour, I_Health {

    [SerializeField]
    private float curHealth = 100;
    [SerializeField]
    private float maxHealth = 200;

    void Start () {
        HUD.SetHealth(curHealth, maxHealth);
	}
	

    void I_Health.Attack(float damageAmount)
    {
        curHealth = Mathf.Clamp(curHealth - damageAmount, 0, maxHealth);
        HUD.SetHealth(curHealth, maxHealth);
    }

    void I_Health.Heal(float healAmount)
    {
        curHealth = Mathf.Clamp(curHealth + healAmount, 0, maxHealth);
        HUD.SetHealth(curHealth, maxHealth);
    }
}
