using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Bars : MonoBehaviour
{
    public GameObject bar;
    private int rando;
    public List<GameObject> list;
    private Vector3 pos;
    private bool active = true;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        pos = new Vector3(-17.6f, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (active)
        {
            rando = Random.Range(1, 3);
            Debug.Log(rando);
            if (rando == 1)
            {
                GameObject newobj = Instantiate(bar);
                newobj.transform.position = pos;
                pos.x += 0.5f;
                list.Add(newobj);
                if (list.Count >= 71)
                {
                    active = false;
                }
            }
            if (rando == 2)
            {
                foreach (GameObject obj in list)
                {
                    Destroy(obj);
                }
                list.Clear();
                pos = new Vector3(-17.6f, 0, 0);
            }
        }
            
    }
}
