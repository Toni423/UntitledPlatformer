using UnityEngine;

public class EnemySpriteHandler : MonoBehaviour {
    [SerializeField] private Transform attackPoint;
    [SerializeField] private float attackPointOffset;
    [SerializeField] private float attackRadius = 2f;
    [SerializeField] private int attackDamage;
    [SerializeField] private LayerMask targets;
    [SerializeField] private GameObject spell;
    [SerializeField] private int life = 10;
    [SerializeField] private float attackCooldown;

    [SerializeField] private float meleeViewDistance;
    [SerializeField] private float spellViewDistance;
    [SerializeField] private SpriteRenderer spriteRenderer;
    
    
    private float timeSinceAttack;
    private int currentDirection = -1;
    private Animator animator;

    private void Start() {
        animator = GetComponent<Animator>();
    }
    
    private void Update() {
        timeSinceAttack += Time.deltaTime;
        Collider2D[] temp = Physics2D.OverlapCircleAll(attackPoint.position, spellViewDistance, targets);
        if(temp.Length != 0) {
            if (temp[0].transform.position.x < attackPoint.position.x) {
                spriteRenderer.flipX = false;
                currentDirection = -1;
            }
            else {
                spriteRenderer.flipX = true;
                currentDirection = 1;
            }
        }
        
        
        if (timeSinceAttack > attackCooldown) {
            if (Physics2D.OverlapCircleAll(attackPoint.position, meleeViewDistance, targets).Length != 0) {
                attack("Attack");
            }
            else if(Physics2D.OverlapCircleAll(attackPoint.position, spellViewDistance, targets).Length != 0){
                attack("Cast");
            }
        }
    }
    

    public void killEnemy() {
        Destroy(transform.parent.gameObject);
    }

    public void spawnMagic() {
        Instantiate(spell, (Vector2)GameObject.FindWithTag("Player").transform.position + Vector2.up * 2, Quaternion.identity);
    }



    public void attacked() {
        Collider2D[] hit = Physics2D.OverlapCircleAll(
            attackPoint.position + (Vector3)(Vector2.right * attackPointOffset ) * currentDirection, attackRadius,
            targets);
        foreach (Collider2D collider in hit) {
            collider.GetComponent<PlayerMove>().takeDamage(attackDamage, attackPoint.position);
        }
    }
    
    
    private void OnDrawGizmosSelected() {
        if (attackPoint == null) {
            return;
        }
        Gizmos.DrawWireSphere(attackPoint.position + (Vector3) (Vector2.right * attackPointOffset) * currentDirection, attackRadius);
        
        Gizmos.DrawWireSphere(attackPoint.position , meleeViewDistance);
        Gizmos.DrawWireSphere(attackPoint.position , spellViewDistance);

    }
    

    public void takeDamage(int damage) {
        life -= damage;
        animator.SetTrigger("Hurt");

        if (life <= 0) {
            animator.SetBool("Dead", true);
            
        }
    }

    
    private void attack(string type) {
        if (timeSinceAttack < attackCooldown) {
            return;
        }
        
        timeSinceAttack = 0;
        animator.SetTrigger(type);
    }
}
