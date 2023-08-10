using UnityEngine;

public abstract class HealthListener : MonoBehaviour {

    [SerializeField] protected ScriptableHealthManager healthManager;
     
    public abstract void notify(int newHealth);

    private void OnEnable() {
        healthManager.addHealthListener(this);
    }

    private void OnDisable() {
        healthManager.removeHealthListener(this);
    }
    
}
