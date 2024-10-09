using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactions : MonoBehaviour
{
    private GameObject Luz;
    private bool isOnLightRange;
    [SerializeField] LayerMask algasLayer;
    bool isAlreadyCuring = false;

    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.CompareTag("Algas"))
        {
            Luz = col.gameObject;
            isOnLightRange = true;
        }
    }
    void Update()
    {
        if (Input.GetKey(KeyCode.Z) && Luz != null)
        {

            Luz.GetComponent<Lights>().LigarLuz();
        }
        CheckForAlgas();
    }


    void CheckForAlgas()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, 4f, algasLayer);

        foreach (Collider hitCollider in hitColliders)
        {
            if (hitCollider.CompareTag("Algas") && hitCollider.GetComponent<Light>().intensity >= 1.5)
            {
                StartCoroutine(CurarPersonagem(1));
            }
        }
    }
    private IEnumerator CurarPersonagem(int amount_)
    {
        if (!isAlreadyCuring)
        {
            isAlreadyCuring = true;
            GetComponent<PlayerInventory>().ChangeLife(amount_);
            yield return new WaitForSeconds(5f);
            isAlreadyCuring = false;
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;

        Gizmos.DrawWireSphere(transform.position, 4f);
    }
}
