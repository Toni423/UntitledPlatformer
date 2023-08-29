using UnityEngine;

public class CameraTracker : MonoBehaviour {
    
    [SerializeField] private Transform player;
    [SerializeField] private float divider = 2;

    private void Update() {
        
        Vector2 playerPosition = player.position;
        Vector3 mousePos = Input.mousePosition;   
        mousePos.z=Camera.main.nearClipPlane;
        Vector3 Worldpos=Camera.main.ScreenToWorldPoint(mousePos);
        Vector2 mousePosition = new Vector2(Worldpos.x, Worldpos.y);
        
        transform.position =playerPosition + ((mousePosition - playerPosition) / divider);

    }
}
