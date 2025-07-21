using UnityEngine;

public class Bin : MonoBehaviour
{
    [SerializeField] private Camera cam;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        float screenHeight = 2f * cam.orthographicSize;
        float screenWidth = screenHeight * cam.aspect;
        transform.position = new Vector3(-1 + screenWidth / 2, -1 + screenHeight / 2, 0f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
