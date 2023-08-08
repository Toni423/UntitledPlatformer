using UnityEngine;

public class EnemySpriteHandler : MonoBehaviour
{

    public void killEnemy() {
        Destroy(transform.parent.gameObject);
    }
}
