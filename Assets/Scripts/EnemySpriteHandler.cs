using UnityEngine;

public class EnemySpriteHandler : MonoBehaviour {
    [SerializeField] private Transform attackPoint;
    [SerializeField] private float attackPointOffset;
    [SerializeField] private float attackRadius = 2f;
    [SerializeField] private int attackDamage;
    [SerializeField] private LayerMask targets;

    
    [SerializeField] private GameObject spell;

    private int currentDirection = -1;
    
    public void killEnemy() {
        Destroy(transform.parent.gameObject);
    }

    public void spawnMagic() {
        Instantiate(spell, (Vector2)GameObject.FindWithTag("Player").transform.position + Vector2.up * 2, Quaternion.identity);
    }



    public void attacked() {
        Collider2D[] hit = Physics2D.OverlapCircleAll(
            attackPoint.position + (Vector3)(Vector2.right * attackPointOffset) * currentDirection, attackRadius,
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
    }
    
}
