using System;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;
using System.Diagnostics;

using Debug = UnityEngine.Debug;

public class Player : Character
{

    public float flashSpeed = 5;
    public Color color = new Color(1, 0, 0);
    public Image damageImage;
    public Slider slider;

    public AudioSource PlayerAudio;
    public AudioClip attackClip;
    public AudioClip damageClip;
    public AudioClip dieClip;

    public int Score;

    private float deathTimer = 3;

    void Start()
    {
        anim = this.gameObject.GetComponentInChildren<Animator>();
        PlayerAudio = this.GetComponent<AudioSource>();

        damagePoint = 10;
        this.currentHP = this.startHP;
        this.Score = PlayerPrefs.GetInt("Score");
    }

    // Update is called once per frame
    void Update()
    {
        this.CheckAttack();
        this.CheckShielding();
        slider.value = this.currentHP;
        if (isDamaged == true)
        {
            this.damageImage.color = color;
        }
        else
        {
            this.damageImage.color = Color.Lerp(damageImage.color, Color.clear, flashSpeed * Time.deltaTime);
        }

        isDamaged = false;
        this.AdvanceTimeVariables();
        if (this.deathTimer < 0)
        {
            SceneManager.LoadScene("GameOver");
        }
    }

    void AdvanceTimeVariables()
    {
        this.attackCount -= Time.deltaTime;
        if (!this.isAlive)
        {
            this.deathTimer -= Time.deltaTime;
        }
    }

    void CheckAttack()
    {
        if (this.isAlive)
        {
            this.isAttacking = this.attackCount > 0;
            if (Input.GetKeyDown("space") || Input.GetKeyDown("joystick button 0") && !this.isShielding)
            {
                if (this.attackCount <= 0)
                {
                    PlayerAudio.PlayOneShot(attackClip);
                    this.attackCount = .2f;
                }
            }
            
        }
        // Checking if attacking is done
        this.anim.SetFloat("attackCount", this.attackCount);
    }

    /// <summary>Uses right trigger to check if character is shielding.</summary>
    void CheckShielding()
    {
        if (this.isAlive)
        {
            var rightTriggerPressed = Input.GetAxis("Shield");
            if (rightTriggerPressed > 0 || Input.GetKeyDown(KeyCode.LeftShift) && !this.isAttacking)
            {
                this.isShielding = true;
                this.anim.SetBool("shielding", true);
            }
            else
            {
                this.isShielding = false;
                this.anim.SetBool("shielding", false);
            }
        }
    }

    void OnTriggerEnter(Collider collider)
    {
        Dragon dragon = collider.gameObject.GetComponent<Dragon>();
        Enemy enemy1 = collider.gameObject.GetComponent<Enemy>();
        if (collider.gameObject.tag == "Enemy" && this.isAlive && enemy1.isAlive)
        {
            Debug.Log("Enemy");
            this.PlayerAudio.PlayOneShot(this.damageClip);
            this.isDamaged = true;
            this.Damage(enemy1.damagePoint);
            if (!this.isAlive)
            {
                this.attackCount = 0;
                this.PlayerAudio.PlayOneShot(this.dieClip);
                this.gameObject.GetComponent<PlayerMoveControl>().enabled = false;
            }

			ArenaManager.multiplier = 1;
        }
        else if ((collider.gameObject.tag == "Fireball" || collider.gameObject.tag == "Dragon") && this.isAlive && dragon.isAlive)
        {
            Debug.Log("Dragon");
            this.PlayerAudio.PlayOneShot(this.damageClip);
            this.isDamaged = true;
            this.Damage(dragon.damagePoint);
            if (!this.isAlive)
            {
                this.attackCount = 0;
                this.PlayerAudio.PlayOneShot(this.dieClip);
                this.gameObject.GetComponent<PlayerMoveControl>().enabled = false;
            }
        }
    }
}