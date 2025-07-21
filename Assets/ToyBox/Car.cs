using UnityEngine;

public class Car : Toy
{
    private Vector3 lastMousePos;
    private Vector3 mouseVelocity;
    private Rigidbody rb;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private new void Awake()
    {
        base.Awake();
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    private new void FixedUpdate()
    {
        base.FixedUpdate();
        if (Input.GetKey(KeyCode.UpArrow)) 
        {
            Debug.Log("Up");
            Quaternion rotation = Quaternion.Euler(0, 0, transform.eulerAngles.z);
            Vector3 rotatedUp = rotation * Vector3.up;
            rb.AddForce(rotatedUp * 200f, ForceMode.Acceleration);
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            Debug.Log("Up");
            Quaternion rotation = Quaternion.Euler(0, 0, transform.eulerAngles.z);
            Vector3 rotatedUp = rotation * Vector3.up;
            rb.AddForce(rotatedUp * -200f, ForceMode.Acceleration);
        }
        if (Input.GetKey(KeyCode.LeftArrow)) 
        {
            transform.Rotate(0, 0, 2f);
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.Rotate(0, 0, -2f);
        }
    }
    public override void OnRelease()
    {
        base.OnRelease();
    }
    public override void OnClicked()
    {
        base.OnClicked();
    }
}
