using UnityEngine;

public class Spell : MonoBehaviour
{
    [SerializeField] private Transform attackPoint;
    [SerializeField] private float attackRadius = 2f;
    [SerializeField] private int attackDamage;
    [SerializeField] private LayerMask targets;

    
    
    public void attack() {
        Collider2D[] hit = Physics2D.OverlapCircleAll(
            attackPoint.position, attackRadius,
            targets);
        foreach (Collider2D collider in hit) {
            collider.GetComponent<PlayerMove>().takeDamage(attackDamage, attackPoint.position);
        }
    }


    public void deleteThis() {
        Destroy(gameObject);
    }
    
    
    private void OnDrawGizmosSelected() {
        if (attackPoint == null) {
            return;
        }
        Gizmos.DrawWireSphere(attackPoint.position , attackRadius);
    }
}
