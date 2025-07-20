using UnityEngine;
using System.Collections;
using DG.Tweening;

public class TweenTest : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(RunFunctionAtIntervals());
    }
    IEnumerator RunFunctionAtIntervals()
    {
        yield return new WaitForSeconds(5);
        Test();

    }

    // Update is called once per frame
    void Update()
    {

    }
    void Test()
    {
        transform.DOMove(new Vector3(0, 0, 0), 2);
    }
}
