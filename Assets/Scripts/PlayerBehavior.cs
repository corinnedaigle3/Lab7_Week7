using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehavior : MonoBehaviour
{
    public float moveSpeed = 10f;
    public float rotateSpeed = 5f;
    public float jumpForce = 7f;
    public float mouseSensitivity = 3f;
    public float groundDrag = 4f; //prevent sliding on the ground with friction

    private float _vInput;
    private float _hInput;
    private Rigidbody _rb;
    private Transform _cameraTransform;
    private float _mouseX;
    private float _mouseY;
    private float _cameraRotationLimit = 85f;//limits camera rotation to avoid user overdoing it
    private bool _isGrounded;

    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _cameraTransform = Camera.main.transform; //get main camera location
        _rb.drag = groundDrag; //apply grounding to reduce sliding
        Cursor.lockState = CursorLockMode.Locked; // lock cursor for better mouse control
        Cursor.visible = false;

    }

    // Update is called once per frame
    void Update()
    {
        _isGrounded = Physics.Raycast(transform.position, Vector3.down, 1.1f);//ground status updates
        _vInput = Input.GetAxis("Vertical"); //W & S (forward & back)
        _hInput = Input.GetAxis("Horizontal"); //A & D (left & right)

        if (Input.GetKeyDown(KeyCode.Space) && _isGrounded)
        {
            _rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
        // Get mouse movement for camera rotation (freely without click)
        _mouseX += Input.GetAxis("Mouse X") * mouseSensitivity;
        _mouseY -= Input.GetAxis("Mouse Y") * mouseSensitivity;

        // Clamp vertical rotation to avoid unnatural upside-down rotations
        _mouseY = Mathf.Clamp(_mouseY, -_cameraRotationLimit, _cameraRotationLimit);
    }

    void FixedUpdate()
    {
        Vector3 moveDirection = (transform.forward * _vInput + transform.right * _hInput).normalized * moveSpeed * Time.fixedDeltaTime;
        if (_vInput != 0 || _hInput != 0)
        {
            _rb.AddForce(moveDirection, ForceMode.VelocityChange);
        }
        // Apply player rotation (yaw - left/right)
        transform.rotation = Quaternion.Euler(0, _mouseX, 0);

        // Apply camera rotation (pitch - up/down)
        _cameraTransform.localRotation = Quaternion.Euler(_mouseY, 0, 0);
    }
}
