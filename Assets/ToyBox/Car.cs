using UnityEngine;

public class Car : Toy
{
    private Vector3 lastMousePos;
    private Vector3 mouseVelocity;
    private Rigidbody rb;
    private bool activeCheck = false;
    [SerializeField] private GameObject green;
    [SerializeField] private GameObject red;

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
        if (activeCheck != active)
            SwitchActive();
        if (!active)
            return;
        if (Input.GetKey(KeyCode.UpArrow)) 
        {
            Quaternion rotation = Quaternion.Euler(0, 0, transform.eulerAngles.z);
            Vector3 rotatedUp = rotation * Vector3.up;
            rb.AddForce(rotatedUp * 200f, ForceMode.Acceleration);
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            Quaternion rotation = Quaternion.Euler(0, 0, transform.eulerAngles.z);
            Vector3 rotatedUp = rotation * Vector3.up;
            rb.AddForce(rotatedUp * -200f, ForceMode.Acceleration);
        }
        if (Input.GetKey(KeyCode.LeftArrow)) 
        {
            transform.Rotate(0, 0, 4f);
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.Rotate(0, 0, -4f);
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
    private void SwitchActive()
    {
        if (active)
        {
            green.SetActive(true);
            red.SetActive(false);
            activeCheck = active;
        }
        else
        {
            green.SetActive(false);
            red.SetActive(true);
            activeCheck = active;
        }
    }
}
