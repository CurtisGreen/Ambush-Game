using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour {

    public int health = 100;
    public int curHealth = 100;
	public int points = 100;
    public Slider enemyHealthSlider;
    bool isDead = false;
    Enemy enemyMovement;
    Animator enemyAnimator;

	// Use this for initialization
	void Start () {
        enemyMovement = GetComponent<Enemy>();
	}
	
	// Update is called once per frame
	void Update () {
	}

    public void Damage(int amount)
    {
        curHealth -= amount;

        enemyHealthSlider.value = curHealth;

        if (!isDead && curHealth <= 0)
        {
            Death();
        }
    }

    public void Death()
    {
        isDead = true;

        enemyAnimator.SetTrigger("Die");
    }
}
