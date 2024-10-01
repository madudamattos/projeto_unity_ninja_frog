using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrutaEspecial : MonoBehaviour
{
    [SerializeField]
    GameObject prefab;

    [SerializeField] StompDeath scriptJogador;
    [SerializeField] SpriteRenderer _spriteRenderer;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            scriptJogador.imortal = true;
            MudarCorJogador();

            Instantiate(prefab, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }

    private void MudarCorJogador()
    {
        Color corAtual = _spriteRenderer.color;

        corAtual.r = 0f;

        _spriteRenderer.color = corAtual;
    }
}
