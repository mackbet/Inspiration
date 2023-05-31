using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    void Start()
    {
        Destroy(gameObject);
    }

    private void OnDestroy()
    {
        Test[] leftCharacters = FindObjectsOfType<Test>();
        Debug.Log(leftCharacters.Length);
    }
}
