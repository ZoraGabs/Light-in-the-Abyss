using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    [SerializeField] int Hp;
    [SerializeField] int Coins;
    private BoxCollider coll;
    private bool Invencibility;

    void Start()
    {
        coll = GetComponent<BoxCollider>();
    }

    public void ChangeLife(int amount)
    {
        Hp += amount;
    }

    public void ChangeMoney(int amnt)
    {
        Coins += amnt;
    }

    void OnCollisionEnter(Collision colli)
    {
        if (colli.gameObject.CompareTag("Enemy") && !Invencibility)
        {
            ChangeLife(-1);
        }
        else if (colli.gameObject.CompareTag("Coin"))
        {
            GameObject objeto = colli.gameObject;
            Destroy(objeto);
            ChangeMoney(1);
        }
    }
}
