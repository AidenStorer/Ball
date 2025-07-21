using UnityEngine;

public class UIElement : MonoBehaviour
{
    public GameObject element;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Spawn()
    {
        Instantiate(element);
    }
}
