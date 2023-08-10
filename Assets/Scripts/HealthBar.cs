using UnityEngine;
using UnityEngine.UI;

public class HealthBar : HealthListener {
    [SerializeField] private Image health;
    
    public override void notify(int newHealth) {
        health.fillAmount = newHealth / 6f;
    }
}
