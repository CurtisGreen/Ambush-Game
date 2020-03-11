using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

/// <summary>
/// For any character in game. i.e. player or enemy
/// </summary>
public class Character : MonoBehaviour {
    /// <summary>
    /// Starting health for character
    /// </summary>
    public int startHP;

    /// <summary>
    /// Current health points of character. In subclass, change this to starting 
    /// points in Start()
    /// </summary>
    public int currentHP;

    /// <summary>
    /// State of character's life. True = alive. False = dead.
    /// </summary>
    public bool isAlive = true;

    /// <summary>State of character shielding (for player).</summary>
    public bool isShielding = false;

    /// <summary>
    /// State of character attacking.
    /// </summary>
    public bool isAttacking = false;

    /// <summary>The is damaged.</summary>
    public bool isDamaged = false;

    //public Slider slider;
    /// <summary>
    /// The weapon the player is currently holding
    /// </summary>
    public Weapon weapon;

    protected Animator anim;

    protected Rigidbody body;

    public float attackCount;

    public int damagePoint;
    // Use this for initialization
    void Start () {
        
    }
    
    // Update is called once per frame
    void Update () {
        
    }

    /// <summary>
    /// Call whenever character is moving
    /// </summary>
    /// <param name="">Not sure</param>
    public void Movements() {
        // TODO: check what params we need for this.
        // Does all charactere have the same velocity moving in a direction?
        // Do we just need to direction?
        // This function probably will stay in here if there's copy and paste code.
        // 	i.e. if code in Player is the same in Enemy
    }

    /// <summary>
    /// Call whenever character is dealing damage from weapon
    /// </summary>
    /// <param name="amount">Amount of damage character is going to take</param>
    public void Damage(int amount)
    {
        //TODO: isDamaged = true; // BRIAN: What is this for?
        currentHP -= amount;
        //Debug.Log ("Damaged");

        //slider.value = currentHP;
        // TODO:
        if (currentHP <= 0)
         {
            Debug.Log("health is 0");
            Death();
        }
    }

    /// <summary>
    /// Attack animation
    /// </summary>
    protected void Attack() {

    }

    /// <summary>
    /// Die animation
    /// </summary>
    public void Death()
    {
        Debug.Log("death");
        anim.SetTrigger("Die");
        isAlive = false;
    }
}
