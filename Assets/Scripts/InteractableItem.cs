using UnityEngine;

public class InteractableItem : MonoBehaviour
{
     
    private float _power=500;
    
    [SerializeField]
    private int _highlightIntensity = 4;
    
    private Outline _outline;
    private Rigidbody _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _outline = GetComponent<Outline>();
    }

    public void SetFocus()
    {
        _outline.OutlineWidth = _highlightIntensity;
    }
    
    public void RemoveFocus()
    {
        _outline.OutlineWidth = 0;
    }

    public void PickUp(Transform inventory)
    {
        transform.SetParent(inventory);  
        transform.localPosition=Vector3.zero;
        transform.localRotation = Quaternion.identity;
        _rigidbody.isKinematic = true;

    }

    public void ThrowAway(Vector3 direction)
    {
        transform.SetParent(null);
        _rigidbody.isKinematic = false;
        _rigidbody.AddForce(direction*_power);
    }

    public void Drop()
    {
        transform.SetParent(null);
        _rigidbody.isKinematic = false;
    }
}