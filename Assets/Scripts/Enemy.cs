using System;
using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour {
    
    [SerializeField] private int life = 10;
    [SerializeField] private GameObject animatorHandler;
    [SerializeField] private float attackCooldown;
    



    private float timeSinceAttack;
    private Animator animator;


    private void Start() {
        animator = animatorHandler.GetComponent<Animator>();
    }

    private void Update() {
        timeSinceAttack += Time.deltaTime;
        if (timeSinceAttack > attackCooldown) {
            attack("Cast");
        }
        
        if (Input.GetKeyDown(KeyCode.K)) {
            animator.SetTrigger("test");
        }
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
