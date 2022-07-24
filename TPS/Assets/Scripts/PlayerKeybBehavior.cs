using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerKeybBehavior : MonoBehaviour
{

    public float moveSpeed = 10f;
    public float jumpForce = 5f;
    public float sprintSpeed = 4f;

    [SerializeField] private GameBehavior _gameManager;

    private bool isGrounded;
    private Rigidbody _body;
    private float vertInput;
    private float horInput;

    void Start()
    {
        _body = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        SprintLogic();
    }

    void FixedUpdate()
    {
        MovementLogic();
        JumpLogic();
    }

    private void OnCollisionEnter(Collision collision)
    {
        IsGroundedUpdate(collision, true);
        if (collision.gameObject.CompareTag("enemy")) {
            _gameManager.HP -= 1;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        IsGroundedUpdate(collision, false);
    }

    private void MovementLogic() {
        horInput = Input.GetAxis("Horizontal") * moveSpeed;
        vertInput = Input.GetAxis("Vertical") * moveSpeed;
        _body.MovePosition(transform.position + transform.forward * vertInput * Time.fixedDeltaTime +
           transform.right * horInput * Time.fixedDeltaTime);
    }

    private void JumpLogic() {
        if (Input.GetAxis("Jump") > 0 && isGrounded)
                _body.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }

    private void SprintLogic() {
        if (Input.GetKeyDown(KeyCode.LeftShift)) {
            this.moveSpeed += sprintSpeed;
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift)) {
            this.moveSpeed -= sprintSpeed;   
        }
    }

    private void IsGroundedUpdate(Collision collision, bool value) {
        if (collision.gameObject.CompareTag("ground")) {
            isGrounded = value;
        }
    }
}
