using UnityEngine;

public class Toy : MonoBehaviour
{
    public bool holding = false;
    public float screenPaddingX = 50f; // How far inside the screen horizontally
    public float screenPaddingY = 50f;
    public Camera cam;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected void Awake()
    {
        cam = FindAnyObjectByType<Camera>();
    }

    // Update is called once per frame
    protected void FixedUpdate()
    {
        if (!holding)
            return;
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = cam.WorldToScreenPoint(transform.position).z;
        mousePos.x = Mathf.Clamp(mousePos.x, screenPaddingX, Screen.width - screenPaddingX);
        mousePos.y = Mathf.Clamp(mousePos.y, screenPaddingY, Screen.height - screenPaddingY);
        Vector3 currentMousePos = cam.ScreenToWorldPoint(mousePos);
        transform.position = currentMousePos;
    }
    public virtual void OnClicked()
    {
        holding = true;
    }
    public virtual void OnRelease()
    {
        if (!holding)
            return;
        holding = false;
    }
}
