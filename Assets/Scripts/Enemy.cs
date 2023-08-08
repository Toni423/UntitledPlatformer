using System;
using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour {
    
    [SerializeField] private int life = 10;
    [SerializeField] private GameObject animatorHandler;
    
    private Animator animator;

    private void Start() {
        animator = animatorHandler.GetComponent<Animator>();
    }

    private void Update() {
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

    public void die() {
        Destroy(gameObject);
    }

}
