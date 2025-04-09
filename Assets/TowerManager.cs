using UnityEngine;
using UnityEngine.InputSystem;

public class TowerManager : MonoBehaviour
{
    public GameObject towerPrefab;

    public void OnClick(InputAction.CallbackContext context)
    {
        if(context.phase != InputActionPhase.Performed)
            return;

        Vector3 clickPos = Input.mousePosition ;
        var pos = Camera.main.ScreenToWorldPoint(clickPos);
        pos.z = 0;
        Instantiate(towerPrefab, pos, Quaternion.identity);
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       
    }
}
