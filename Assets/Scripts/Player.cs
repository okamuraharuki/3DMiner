using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] bool _isLookAtTarget = false;
    [SerializeField] Transform _target;
    [SerializeField] Transform _headTra;
    [SerializeField] float _moveSpeed = 5;
    [SerializeField] float _angSpeedIndex = 1;
    Rigidbody _rb;
    Transform _tra;
    void Start()
    {
        _rb = this.GetComponent<Rigidbody>();
        _tra = this.GetComponent<Transform>();
    }
    void Update()
    {
        if(_isLookAtTarget)
        {
            _tra.forward = _target.position - _tra.position;
        }
    }
    void FixedUpdate()
    {
        Vector3 cameraForward = Vector3.Scale(Camera.main.transform.forward, new Vector3(1, 0, 1)).normalized;
        Vector3 direction = cameraForward * Input.GetAxis("Vertical") + Camera.main.transform.right * Input.GetAxis("Horizontal");
        _rb.velocity = direction * _moveSpeed;
        _headTra.forward = Camera.main.transform.forward; 
    }
}