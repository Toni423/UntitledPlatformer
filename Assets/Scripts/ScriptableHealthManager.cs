using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "HealthManager", menuName = "ScriptableObjects/ScriptableHealthManager", order = 1)]
public class ScriptableHealthManager : ScriptableObject, ISerializationCallbackReceiver {
    private readonly List<HealthListener> healthListeners = new List<HealthListener>();

    private int life;
    [SerializeField] private int maxLife;


    private readonly object locker = new();
    public void setLife(int newLife) {
        lock (locker) {
            life = newLife;
            notifyListeners();
        }
    }

    public int getLife() {
        return life;
    }
    
    
    public void addHealthListener(HealthListener listener) {
        healthListeners.Add(listener);
    }

    public void removeHealthListener(HealthListener listener) {
        healthListeners.Remove(listener);
    }

    private void notifyListeners() {
        foreach (HealthListener listener in healthListeners) {
            listener.notify(life);
        }
    }


    public void OnBeforeSerialize() {
        
    }

    public void OnAfterDeserialize() {
        life = maxLife;
        notifyListeners();
    }
}
