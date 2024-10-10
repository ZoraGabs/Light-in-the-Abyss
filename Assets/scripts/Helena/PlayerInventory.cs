using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PlayerInventory : MonoBehaviour
{

    [SerializeField] int Hp = 6;
    [SerializeField] int Coins;
    private BoxCollider coll;
    private bool Invencibility;
    private int lifelimit = 6;
    public GameObject[] coracoes;

    void Start()
    {
        coracoes = coracoes.OrderBy(c => c.transform.position.x).ToArray(); // encontra os "corações" na hud por ordem do eixo X e cria o array :D
        coll = GetComponent<BoxCollider>();
        ChangeLife(0);
    }

    public void ChangeLife(int amount)
    {
        Hp += amount;
        if(Hp > lifelimit)
        {
            Hp = lifelimit;
        } // limita a vida

        for (int i = 0; i < coracoes.Length; i++) // 
        {
            int coracaoIndice = i * 2;

            if (Hp >= coracaoIndice + 2)
            {
                DefinirEstadoCoracao(coracoes[i], 2);
            }
            else if (Hp == coracaoIndice + 1)
            {
                DefinirEstadoCoracao(coracoes[i], 1);
            }
            else
            {
                DefinirEstadoCoracao(coracoes[i], 0);
            }
        }//chama a funcao para definir os coracoes apos a mudança na variavel
    }

    void DefinirEstadoCoracao(GameObject coracao, int estado)
    {
        coracao.transform.GetChild(0).gameObject.SetActive(estado == 2); 
        coracao.transform.GetChild(1).gameObject.SetActive(estado == 1); 
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
