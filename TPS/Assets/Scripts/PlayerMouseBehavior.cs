using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMouseBehavior : MonoBehaviour
{

    public enum RotationAxes { 
        MouseX = 0,
        MouseY = 1
    }

    public RotationAxes axes = RotationAxes.MouseX;

    public float sensitivityHor = 9.0f;
    public float sensitivityVert = 9.0f;

    public float minimumVert = -45.0f;
    public float maximumVert = 45.0f;

    private float _rotationX = 0;
    private Rigidbody _body;

    void Start()
    {
        _body = GetComponent<Rigidbody>();
        if (_body != null) {
            _body.freezeRotation = true;
        }
    }

    void Update()
    {
        RotationLogic();
    }

    void RotationLogic() {
        if (Time.timeScale != 0.0f)
        {
            if (axes == RotationAxes.MouseX)
            {
                Quaternion angleRot = Quaternion.Euler(Vector3.up * Input.GetAxis("Mouse X") * sensitivityHor);
                _body.MoveRotation(_body.rotation * angleRot);
            }
            else if (axes == RotationAxes.MouseY)
            {
                _rotationX -= Input.GetAxis("Mouse Y") * sensitivityVert;
                _rotationX = Mathf.Clamp(_rotationX, minimumVert, maximumVert);

                float rotationY = transform.localEulerAngles.y;

                transform.localEulerAngles = new Vector3(_rotationX, rotationY, 0);
            }
        }
    }
}
