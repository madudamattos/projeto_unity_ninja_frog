using System.Collections;
using UnityEngine;

public class ElevadorGelo : MonoBehaviour
{
    [SerializeField] private Transform _pontoFinal; // Ponto final para onde a plataforma vai
    [SerializeField] private float _velocidade = 2f; // Velocidade de movimento da plataforma

    private bool _jogadorEmCima = false;
    private bool _subindo = false;
    private Vector2 _posicaoOriginal; // Posi��o original da plataforma

    private void Start()
    {
        _posicaoOriginal = transform.position; // Guarda a posi��o original
    }

    private void Update()
    {
        if (_jogadorEmCima && !_subindo)
        {
            _subindo = true;
            StartCoroutine(MoverParaCima());
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            _jogadorEmCima = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            _jogadorEmCima = false;
            StartCoroutine(MoverParaBaixo());
        }
    }

    private IEnumerator MoverParaCima()
    {
        while (transform.position.y < _pontoFinal.position.y)
        {
            transform.position = Vector2.MoveTowards(transform.position, _pontoFinal.position, _velocidade * Time.deltaTime);
            yield return null;
        }

        // Reseta o estado ap�s subir
        _subindo = false;
    }

    private IEnumerator MoverParaBaixo()
    {
        while (transform.position != (Vector3)_posicaoOriginal)
        {
            transform.position = Vector2.MoveTowards(transform.position, _posicaoOriginal, _velocidade * Time.deltaTime);
            yield return null;
        }
    }
}
