using System;
using UnityEngine;

public class Door : MonoBehaviour
{
    private Animator _animator;
    private bool _isOpen;
    
    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }


    private void Update()
    {
        
    }

    public void SwitchDoorState()
    {
        _isOpen = !_isOpen;
        _animator.SetBool("isOpen", _isOpen);
    }
}