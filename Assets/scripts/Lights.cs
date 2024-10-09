using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lights : MonoBehaviour
{
    private Light luz;

    void Start()
    {
        luz = GetComponent<Light>();
    }

    public void LigarLuz()
    { 
        StartCoroutine(RotinaLuz());
    }
    private IEnumerator RotinaLuz()
    {
        while(luz.intensity < 1.8)
        {
            luz.intensity += 0.05f * Time.deltaTime;
            yield return null;
        }
    }
}
