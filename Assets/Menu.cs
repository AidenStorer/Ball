using UnityEngine;
using DentedPixel;

public class Menu : MonoBehaviour
{
    public bool open;
    [SerializeField] private Camera cam;
    [SerializeField] private Transform arrow;
    private float startX; 

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        float screenHeight = 2f * cam.orthographicSize;
        float screenWidth = screenHeight * cam.aspect;
        startX = 0.32778f - screenWidth / 2;
        transform.position = new Vector3(startX, 0, -1f);
        open = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OpenMenu()
    {
        if (!open)
        {
            arrow.rotation = Quaternion.Euler(0, 0, 180);
            LeanTween.move(this.gameObject, new Vector3(startX + 7.45F, 0, -1), 0.5f);
            open = true;
        }
        else
        {
            arrow.rotation = Quaternion.Euler(0, 0, 0);
            LeanTween.move(this.gameObject, new Vector3(startX, 0, -1), 0.5f);
            open = false;
        }
    }
}
