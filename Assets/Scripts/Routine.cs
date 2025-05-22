using System;
using System.Collections;
using UnityEngine;

public class Routine : MonoBehaviour
{
    private IEnumerator _coroutine;

    void Start() { }

    void Update() { }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            _coroutine = WaitAndPrint(5f);
            StartCoroutine(_coroutine);
        }
    }

    private static IEnumerator WaitAndPrint(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        Debug.Log("Rodou após " + waitTime + " segundos");
    }
}
