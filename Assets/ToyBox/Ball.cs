using UnityEngine;

public class Ball : Toy
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
        if (!holding)
            return;
        Vector3 calculateSpeed = cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0));

        if (calculateSpeed != lastMousePos)
        {
            mouseVelocity = (calculateSpeed - lastMousePos) / Time.fixedDeltaTime;
            lastMousePos = calculateSpeed;
        }
        return;
    }
    public override void OnRelease()
    {
        base.OnRelease();
        rb.isKinematic = false;
        rb.AddForce(mouseVelocity * 0.5f, ForceMode.Impulse);
    }
    public override void OnClicked()
    {
        base.OnClicked();
        rb.isKinematic = true;
    }
}
