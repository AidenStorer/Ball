using System.Collections;
using UnityEngine;
using DG.Tweening;

public class RemoveMatt : MonoBehaviour
{
    private float interval = 1f;
    public Transform parent;

    void Start()
    {
        StartCoroutine(RunFunctionAtIntervals());
    }

    Vector3 worldPosition;

void Update()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = Camera.main.nearClipPlane;
        worldPosition = Camera.main.ScreenToWorldPoint(mousePos);
        if (Vector3.Distance(worldPosition, this.transform.position) < 10 ) 
        {
            Remove();
        }
    }

    IEnumerator RunFunctionAtIntervals()
    {
        yield return new WaitForSeconds(5);
        ChangeParentLocation();
            yield return new WaitForSeconds(interval * 1);
            Appear();

            if (interval > 10f)
            {
                interval -= 10f;  
            }
            else if (interval > 1)
            {
            interval -= 1f;
            }
            else
            {
                interval = 1f; 
            }
    }
    void Remove()
    {
        transform.DOLocalMoveY(0, 2);
        StartCoroutine(RunFunctionAtIntervals());
        
    }
    void Appear()
    {
        transform.DOLocalMoveY(10, 2);
    }
    void ChangeParentLocation()
    {
        int random = Random.Range(1, 6);
        if (random == 1)
        {
            parent.position = new Vector3(10f, -18.8f, 0f);
            parent.eulerAngles = new Vector3(0, 0, 0);
            parent.localScale = new Vector3(1, 1, 1);
        }
        if (random == 2)
        {
            parent.position = new Vector3(-11.3f, -18.8f, 0f);
            parent.eulerAngles = new Vector3(0, 0, 0);
            parent.localScale = new Vector3(-1, 1, 1);
        }
        if (random == 3)
        {
            parent.position = new Vector3(0f, 18.65f, 0f);
            parent.eulerAngles = new Vector3(0, 0,0);
            parent.localScale = new Vector3(1, -1, 1);
        }
        if (random == 4)
        {
            parent.position = new Vector3(20.24f, 17f, 0f);
            parent.eulerAngles = new Vector3(0, 0, -35.637f);
            parent.localScale = new Vector3(1, -1, 1);
        }
        if (random == 5)
        {
            parent.position = new Vector3(-20.24f, 17f, 0f);
            parent.eulerAngles = new Vector3(0, 0, 35.637f);
            parent.localScale = new Vector3(-1, -1, 1);
        }
    }
}
