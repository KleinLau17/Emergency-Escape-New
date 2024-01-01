using UnityEngine;

public class AimController : MonoBehaviour
{
    private Camera _mainCamera;

    private void Awake()
    {
        _mainCamera = Camera.main;
        if( _mainCamera == null )
        {
            Debug.LogError("Can't find main camera, please check it!!!");
        }
    }

    private void Update()
    {
        Vector3 position = _mainCamera.WorldToScreenPoint(transform.position);
        Vector3 direction = Input.mousePosition - position;
        float angle = Mathf.Atan2(direction.x,direction.y) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(-angle, Vector3.forward);
    }
}
