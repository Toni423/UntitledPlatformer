using System;
using UnityEngine;

public class PlayerMove : MonoBehaviour {

    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float jumpForce = 5f;
    [SerializeField] private int jumpAmountMax = 2;
    [SerializeField] private float attackRadius = 2f;
    [SerializeField] private LayerMask enemies;
    [SerializeField] private Transform attackPoint;
    [SerializeField] private float attackPointOffset;
    [SerializeField] private int damage = 2;
    [SerializeField] private LayerMask getsDamageBackFrom;
    [SerializeField] private float throwBackForceX;
    [SerializeField] private float throwBackForceY;
    [SerializeField] private int life = 10;
    [SerializeField] private GameObject animationHandler;
    [SerializeField] private float rollTime;


   
    private int maxLife;
    private int currenDirection = 1;
    private int curJumpAmount;
    private bool grounded;
    private float timeToIdle;
    private float timeSinceAttacked;
    private int currAttack;
    private bool isPushed;
    private float currentRollTime;
    private bool rolling;
    

    private Sensor_HeroKnight groundSensor;
    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rb;
    private Animator animator;
    

    private void Start() {
        maxLife = life;
        curJumpAmount = jumpAmountMax;
        spriteRenderer = animationHandler.GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        animator = animationHandler.GetComponent<Animator>();
        groundSensor = GameObject.Find("GroundSensor").GetComponent<Sensor_HeroKnight>();
    }

    // Update is called once per frame
    void Update() {
        timeSinceAttacked += Time.deltaTime;
        currentRollTime -= Time.deltaTime;

        
        if (rolling && currentRollTime < 0) {
            rolling = false;
        }
        
        //Check if character just landed on the ground
        if (!grounded && groundSensor.State())
        {
            grounded = true;
            animator.SetBool("Grounded", grounded);
        }

        //Check if character just started falling
        if (grounded && !groundSensor.State())
        {
            grounded = false;
            animator.SetBool("Grounded", grounded);
        }

        if (grounded) {
            curJumpAmount = jumpAmountMax;
        }

        if ( Mathf.Abs(rb.velocity.x) < Mathf.Epsilon) {
            isPushed = false;
        }
        
        
        float direct = Input.GetAxis("Horizontal");
        float directY = Mathf.Min(Input.GetAxis("Vertical") * 0.1f, 0f); 
        
        if (direct < 0) {
            spriteRenderer.flipX = true;
            currenDirection = -1;
        }
        else if (direct > 0) {
            spriteRenderer.flipX = false;
            currenDirection = 1;
        }

        if (isPushed) {
            return;
        }

        if(!rolling)
            rb.velocity = new Vector2(direct * moveSpeed, rb.velocity.y + directY);
        
        
        
        animator.SetFloat("AirSpeedY", rb.velocity.y);

        // Roll
        if (Input.GetKeyDown(KeyCode.LeftAlt) && !rolling) {
            animator.SetTrigger("Roll");
            currentRollTime = rollTime;
            rb.velocity = new Vector2(rb.velocity.x * 2, rb.velocity.y);
            rolling = true;
        }
        //Attack
        else if(Input.GetMouseButtonDown(0) && timeSinceAttacked > 0.25f && !rolling)
        {
            attack();
        } 
        //Jump
        else if ((Input.GetKeyDown("space") || Input.GetKeyDown(KeyCode.W)) && curJumpAmount > 0) {
            rolling = false;
            curJumpAmount--;
            animator.SetTrigger("Jump");
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }
        else if (Mathf.Abs(direct) > Mathf.Epsilon)
        {
            // Reset timer
            timeToIdle = 0.05f;
            animator.SetInteger("AnimState", 1);
        }
        else
        {
            // Prevents flickering transitions to idle
            timeToIdle -= Time.deltaTime;
            if(timeToIdle < 0) 
                animator.SetInteger("AnimState", 0);
        }
    }


    private void attack() {
        
        currAttack++;

        // Loop back to one after third attack
        if (currAttack > 3)
            currAttack = 1;

        // Reset Attack combo if time since last attack is too large
        if (timeSinceAttacked > 1.0f)
            currAttack = 1;

        // Call one of three attack animations "Attack1", "Attack2", "Attack3"
        animator.SetTrigger("Attack" + currAttack);


        Collider2D[] hit = Physics2D.OverlapCircleAll(attackPoint.position + (Vector3) (Vector2.right * attackPointOffset) * currenDirection, attackRadius, enemies);
        foreach (Collider2D collider in hit) {
            
            collider.GetComponent<Enemy>().takeDamage(damage);
        }

        // Reset timer
        timeSinceAttacked = 0.0f;
    }

    private void OnDrawGizmosSelected() {
        if (attackPoint == null) {
            return;
        }
        Gizmos.DrawWireSphere(attackPoint.position + (Vector3) (Vector2.right * attackPointOffset) * currenDirection, attackRadius);
    }


    private void OnCollisionEnter2D(Collision2D other) {
        if (getsDamageBackFrom == (getsDamageBackFrom | (1 << other.gameObject.layer))) {
            animator.SetTrigger("Hurt");
            
            // calculate pushBack direction
            Vector2 myPos = transform.position;
            Vector2 enemyPos = other.gameObject.transform.position;
            float xForce = myPos.x < enemyPos.x ? -throwBackForceX : throwBackForceX;
            float yForce = myPos.y + 1f < enemyPos.y ? -throwBackForceY : throwBackForceY;
            
            // apply force
            rb.velocity = new Vector2(xForce, yForce);
            isPushed = true;
            
            
            // life loss
            life -= 2;

            if (life <= 0) {
                animator.SetTrigger("Death");
                this.enabled = false;
            }
        }
    }

   
}