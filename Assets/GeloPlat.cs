using System.Collections;
using UnityEngine;

public class PlataformaGelo : MonoBehaviour
{
    [SerializeField] private float _tempoParaDerreter = 3f; // Tempo total até a plataforma derreter
    [SerializeField] private float _tempoVisivel = 0.1f; // Tempo que a plataforma permanece visível ao piscar
    [SerializeField] private float _tempoInvisivel = 0.1f; // Tempo que a plataforma permanece invisível ao piscar
    [SerializeField] private float _tempoAtrasoFinal = 0.5f; // Tempo extra após a última piscada antes de derreter
    [SerializeField] private float _tempoParaReaparecer = 3f; // Tempo para a plataforma reaparecer após derreter

    private bool _jogadorPisou = false;
    private SpriteRenderer _spriteRenderer;
    private BoxCollider2D _boxCollider;

    private void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _boxCollider = GetComponent<BoxCollider2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !_jogadorPisou)
        {
            _jogadorPisou = true;
            StartCoroutine(PlataformaPiscarEDerreter());
        }
    }

    private IEnumerator PlataformaPiscarEDerreter()
    {
        float _tempoDecorrido = 0f;

        // Enquanto o tempo não acabar, a plataforma pisca
        while (_tempoDecorrido < _tempoParaDerreter)
        {
            // Alterna entre visível e invisível
            _spriteRenderer.color = new Color(1f, 1f, 1f, 1f); // Visível
            _boxCollider.enabled = true; // Ativa o collider
            yield return new WaitForSeconds(_tempoVisivel);

            _spriteRenderer.color = new Color(1f, 1f, 1f, 0f); // Invisível
            _boxCollider.enabled = true; // Mantém collider ativo
            yield return new WaitForSeconds(_tempoInvisivel);

            // Aumenta o tempo decorrido
            _tempoDecorrido += (_tempoVisivel + _tempoInvisivel);
        }

        // Aguarda um pequeno atraso após a última piscada
        yield return new WaitForSeconds(_tempoAtrasoFinal);

        // Após a última piscada, a plataforma derrete
        _spriteRenderer.color = new Color(1f, 1f, 1f, 0f); // Torna a plataforma invisível
        _boxCollider.enabled = false; // Desativa o BoxCollider2D

        // Aguarda o tempo para a plataforma reaparecer
        yield return new WaitForSeconds(_tempoParaReaparecer);

        // Faz a plataforma reaparecer
        _spriteRenderer.color = new Color(1f, 1f, 1f, 1f); // Torna a plataforma visível novamente
        _boxCollider.enabled = true; // Reativa o BoxCollider2D

        _jogadorPisou = false; // Reseta o estado para permitir que o jogador pise novamente
    }
}
