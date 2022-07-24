using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayShooting : MonoBehaviour
{
    private Camera _camera;
    [SerializeField] private GameObject _bullet;
    [SerializeField] private EnemyBehavior _enemy;
    [SerializeField] private SphereCollider _enemyRange;
 
    void Start()
    {
        _camera = GetComponentInChildren<Camera>();
    }
  
    void Update()
    {
        ShootingLogic();    
    }

    void ShootingLogic()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 point = new Vector3(_camera.pixelWidth / 2, _camera.pixelHeight / 2, 0);
            Ray ray = _camera.ScreenPointToRay(point);
            RaycastHit hit;
            if (_enemy.EnemyLives > 0)
            {
                _enemyRange.enabled = false;
            }
            if (Physics.Raycast(ray, out hit)) {
                GameObject hitObject = hit.transform.gameObject;
                EnemyBehavior target = hitObject.GetComponent<EnemyBehavior>();
                if (target != null)
                {
                    target.EnemyLives--;
                }
                else if (!(hit.transform.gameObject.GetComponent<PlayerKeybBehavior>()) && 
                    !(hit.transform.gameObject.GetComponent<BulletBehavior>()))
                {
                    StartCoroutine(BulletIndicator(hit.point));
                }
            }
            if (_enemy.EnemyLives > 0)
            {
                _enemyRange.enabled = true;
            }
        }
    }

    private IEnumerator BulletIndicator(Vector3 point) {
        GameObject bullet = Instantiate(_bullet, point, Quaternion.Euler(new Vector3 (0, 0, 0)));
        yield return new WaitForSeconds(1.0f);
        Destroy(bullet);
    }
}
