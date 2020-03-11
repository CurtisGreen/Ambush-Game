using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : Character
{
    /* Variables */
    const float SIGHT_DIRECT_ANGLE = 360.0f, SIGHT_MIN_DISTANCE = 0.2f, SIGHT_MAX_DISTANCE = 2000.0f;
    
    public LayerMask hitTestMask;
     
	public float invulCount;
    public float knockback = 5;
    private float knockbackCount;
    private GameObject enemy;
    public GameObject player;
	public Player playerClass;
    public AudioSource EnemyAudio;
    public AudioClip damageClip;
    public AudioClip shieldClip;
    public AudioClip dieClip;
    public AudioClip attackClip;

    private Vector3 targetPos, startingPos, dir;
    float height = 2.0f;
    private bool targetFound = false;
    private Vector3 agentVelocity;
    private Vector3 knockdirection;
    private NavMeshAgent navMeshAgent;

    private float maxAttackDistance = 100;

	public const int points = 100;

    /// <summary>
    /// The time when the enemy will spawn. This is generated in ArenaManager
    /// </summary>
    public int spawnTime;

    /// <summary>
    /// Boolean to check if the enemy has already spawned 
    /// </summary>
    public bool hasSpawned = false;

    // Use this for initialization
    void Start()
    {
        
        navMeshAgent = this.gameObject.GetComponent<NavMeshAgent>();
        startingPos = transform.position;
        this.anim = this.gameObject.GetComponentInChildren<Animator>();
        this.body = this.gameObject.GetComponent<Rigidbody>();
        this.enemy = this.gameObject;
        this.player = GameObject.FindGameObjectWithTag("Player");
        this.playerClass = this.player.GetComponent<Player>();
        this.ScaleDifficulty();
        this.EnemyAudio = this.GetComponent<AudioSource>();
    }


    // Update is called once per frame
    void Update()
    {
        UpdateDetector();
        this.agentVelocity = this.gameObject.GetComponent<NavMeshAgent>().velocity;
		//updating player ever tim
		this.playerClass = this.player.GetComponent<Player> ();
        this.SetDestination();
        this.CheckKnockback();
        this.CheckAttack();
        this.AdvanceTimeVariables();
        if (this.isAlive == false)
        {
            Destroy(this.enemy, 5);
        }
    }

    void SetDestination()
    {
        navMeshAgent.SetDestination(targetPos);
        navMeshAgent.updateRotation = false;
        //RaycastHit hit = new RaycastHit();
        //Physics.Raycast (transform.position,transform.forward, out hit,weaponRange,hitTestLayer);
    }

    void AdvanceTimeVariables()
    {
        this.knockbackCount -= Time.deltaTime;
        this.attackCount -= Time.deltaTime;
		this.invulCount -= Time.deltaTime;
    }

    bool CheckKnockback()
    {
        // Move the character
        if (this.knockbackCount > 0)
        {
            this.transform.Translate(this.knockback * this.knockdirection);
            return true;
        }
        return false;
    }

    private void CheckAttack()
    {
        var distanceToPlayer = Vector3.Distance(this.enemy.transform.position, this.player.transform.position);
        if(distanceToPlayer <= this.maxAttackDistance && this.isAlive)
        {
            this.anim.SetBool("attacking", true);
            this.attackCount = .2f;
            EnemyAudio.PlayOneShot(attackClip);
        }
        else
        {
            this.anim.SetBool("attacking", false);
            this.attackCount = 0;
        }
        // Checking if attacking is done
        this.isAttacking = this.attackCount > 0;
    }

    void FixedUpdate()
    {
        float lastDirectionX = this.agentVelocity.x;
        float lastDirectionY = this.agentVelocity.y;

        if (lastDirectionX != 0f || lastDirectionY != 0f)
        {
            this.anim.SetBool("walking", true);
            if (lastDirectionX > 0)
            {
                this.anim.SetFloat("LastDirectionX", 1f);
            }
            else if (lastDirectionX < 0)
            {
                this.anim.SetFloat("LastDirectionX", -1f);
            }
            else
            {
                this.anim.SetFloat("LastDirectionX", 0.0f);
            }

            if (lastDirectionY > 0)
            {
                this.anim.SetFloat("LastDirectionY", 1f);
            }
            else if (lastDirectionY < 0)
            {
                this.anim.SetFloat("LastDirectionY", -1f);
            }
            else
            {
                this.anim.SetFloat("LastDirectionY", 0);
            }
        }
        else
        {
            this.anim.SetBool("walking", false);
        }
    }

    protected void UpdateDetector()
    {
        Collider[] overlapedObjects = Physics.OverlapSphere(transform.position, SIGHT_MAX_DISTANCE);
        SetAnimation();
        for (int i = 0; i < overlapedObjects.Length; i++)
        {
            Vector3 direction = overlapedObjects[i].transform.position - transform.position;
            float objAngle = Vector3.Angle(direction, transform.forward);
            if (overlapedObjects[i].tag == "Player")
            {
                if (objAngle < SIGHT_DIRECT_ANGLE && TargetInSight(overlapedObjects[i].transform, SIGHT_MAX_DISTANCE))
                {
                    targetFound = true;
                    this.SetTargetPos(overlapedObjects[i].transform.position);
                }
            }
        }
    }

    private void SetAnimation()
    {
        float directionX = 0;
        float directionY = 0;

        if ((this.agentVelocity.x > 0.1 || this.agentVelocity.x < -0.1) || (this.agentVelocity.y > 0.1 || this.agentVelocity.y < -0.1))
        {
            this.anim.SetBool("walking", true);
            if (this.agentVelocity.x > 0)
            {
                directionX = 1f;
            }
            else if (this.agentVelocity.x < 0)
            {
                directionX = -1f;
            }
            if (this.agentVelocity.z > 0)
            {
                directionY = 1f;
            }
            else if (this.agentVelocity.z < 0)
            {
                directionY = -1f;
            }
        }
        else
        {
            this.anim.SetBool("walking", false);
        }
        this.anim.SetFloat("DirectionX", directionX);
        this.anim.SetFloat("DirectionY", directionY);
    }

    // Check this out to know how to deal with colliders: 
    // https://docs.unity3d.com/Manual/CollidersOverview.html
    void OnTriggerEnter(Collider collider) 
    {
        if (this.isAlive && this.invulCount <= 0)
        {
            //Player player1 = collider.gameObject.GetComponent<Player>();
            if (collider.gameObject.tag == "Weapon" && playerClass.isAttacking)
            {
                EnemyAudio.PlayOneShot(damageClip);
                Debug.Log("Collided with enemy 1");
                this.invulCount = 0.4f;
                Damage(playerClass.damagePoint);
                if (this.playerClass.isAlive)
                {
                    this.Knockback(collider);
                }
                if (!isAlive)
                {
                    ArenaManager.points += points * ArenaManager.multiplier;
                    ArenaManager.multiplier *= 2;

                    this.navMeshAgent.Stop();
                    EnemyAudio.PlayOneShot(dieClip);
                }
            }
            else if (collider.gameObject.tag == "Shield" && this.playerClass.isShielding)
            {
                if (this.playerClass.isAlive)
                {
                    this.EnemyAudio.PlayOneShot(shieldClip);
                    this.Knockback(collider);
                }
            }
        }
    }


    void OnTriggerStay(Collider collider) 
    {
        if (this.isAlive && this.invulCount <= 0)
        {
            //Player player1 = collider.gameObject.GetComponent<Player>();
            if (collider.gameObject.tag == "Weapon" && playerClass.isAttacking)
            {
                EnemyAudio.PlayOneShot(damageClip);
                Debug.Log("Collided with enemy 1");
                this.invulCount = 0.4f;
                Damage(playerClass.damagePoint);
                if (this.playerClass.isAlive)
                {
                    this.Knockback(collider);
                }
                if (!isAlive)
                {
                    ArenaManager.points += points * ArenaManager.multiplier;
                    ArenaManager.multiplier *= 2;

                    this.navMeshAgent.Stop();
                    EnemyAudio.PlayOneShot(dieClip);
                }
            }
            else if (collider.gameObject.tag == "Shield" && this.playerClass.isShielding)
            {
                if (this.playerClass.isAlive)
                {
                    this.EnemyAudio.PlayOneShot(shieldClip);
                    this.Knockback(collider);
                }
            }
        }
    }

    void ScaleDifficulty()
    {
        switch (PlayerPrefs.GetInt("Difficulty"))
        {
            case 1:
                this.startHP = 50;
                this.damagePoint = 5;
                break;
            case 2:
                this.startHP = 100;
                this.damagePoint = 10;
                break;
            case 3:
                this.startHP = 120;
                this.damagePoint = 15;
                break;
        }
        this.currentHP = this.startHP;
    }


    /// <summary>
    /// Pushback animation
    /// </summary>
    protected void Knockback(Collider collider)
    {
        this.knockbackCount = .1f;
        this.knockdirection = (this.gameObject.GetComponent<Rigidbody>().position
                                  - collider.gameObject.GetComponent<Rigidbody>().position).normalized;
    }

    protected bool TargetInSight(Transform target, float distance)
    {
        Vector3 sightPosition = transform.position;
        sightPosition.y += height;
        RaycastHit hit = new RaycastHit();
        Vector3 dir = target.position - sightPosition;
        Physics.Raycast(sightPosition, dir, out hit, distance, hitTestMask);
        return true;
    }

    protected void SetTargetPos(Vector3 newPos)
    {
        targetPos = newPos;
        // TODO: Different states
        //		if (currentState != NPC_EnemyState.ATTACK ) {
        //			SetState (NPC_EnemyState.INSPECT);
        //		}
    }
}
