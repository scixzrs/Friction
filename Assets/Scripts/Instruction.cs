using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Instruction : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(HideInstructions());
    }
    IEnumerator HideInstructions()
    {
        yield return new WaitForSecondsRealtime(5f);
        gameObject.SetActive(false);
    }

}
