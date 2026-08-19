using System.Collections.Generic;
using UnityEngine;

public class Snake : MonoBehaviour
{
    private Rigidbody RB;

    [SerializeField] private float vel = 5f;
    [SerializeField] private int TamInicial = 5;
    [SerializeField] private GameObject Prefabs;
    [SerializeField] private float DistanciaDosSegmentos = 1.0f;

    private List<GameObject> corpo = new List<GameObject>();
    private List<Vector3> HistoricoPos = new List<Vector3>();

    private Vector3 direcaoMover;

    void Start()
    {
        RB = GetComponent<Rigidbody>();

        // Configurações essenciais do Rigidbody para não capotar nem voar
        RB.constraints = RigidbodyConstraints.FreezePositionY |
                         RigidbodyConstraints.FreezeRotationX |
                         RigidbodyConstraints.FreezeRotationY |
                         RigidbodyConstraints.FreezeRotationZ;
        RB.collisionDetectionMode = CollisionDetectionMode.Continuous;

        HistoricoPos.Add(transform.position);

        for (int i = 0; i < TamInicial; i++)
        {
            AumentaTamanho();
        }
    }

    void Update()
    {
        // 1. Apenas lê as teclas no Update
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        direcaoMover = new Vector3(h, 0, v).normalized;

        if (direcaoMover != Vector3.zero)
        {
            transform.forward = direcaoMover;
        }

        TamanhoPlayer();
        MoveCorpo();
    }

    void FixedUpdate()
    {
        // 2. Movel a física com MovePosition dentro do FixedUpdate
        if (direcaoMover != Vector3.zero)
        {
            Vector3 novaPosicao = RB.position + direcaoMover * vel * Time.fixedDeltaTime;
            RB.MovePosition(novaPosicao);
        }
    }

    private void TamanhoPlayer()
    {
        if (HistoricoPos.Count == 0 || Vector3.Distance(HistoricoPos[0], transform.position) > 0.01f)
        {
            HistoricoPos.Insert(0, transform.position);
        }

        int limiteHistorico = (corpo.Count + 1) * 100;
        if (HistoricoPos.Count > limiteHistorico)
        {
            HistoricoPos.RemoveAt(HistoricoPos.Count - 1);
        }
    }

    private void MoveCorpo()
    {
        float distanciaAcumulada = 0f;

        for (int i = 0; i < corpo.Count; i++)
        {
            distanciaAcumulada += DistanciaDosSegmentos;

            Vector3 posicaoAlvo = PosicaoNoHistorico(distanciaAcumulada);
            posicaoAlvo.y = transform.position.y;

            corpo[i].transform.position = posicaoAlvo;
        }
    }

    private Vector3 PosicaoNoHistorico(float distanciaDesejada)
    {
        float distanciaAtual = 0f;

        for (int i = 0; i < HistoricoPos.Count - 1; i++)
        {
            float d = Vector3.Distance(HistoricoPos[i], HistoricoPos[i + 1]);

            if (distanciaAtual + d >= distanciaDesejada)
            {
                float t = (distanciaDesejada - distanciaAtual) / d;
                return Vector3.Lerp(HistoricoPos[i], HistoricoPos[i + 1], t);
            }

            distanciaAtual += d;
        }

        return HistoricoPos[HistoricoPos.Count - 1];
    }

    public void AumentaTamanho()
    {
        if (Prefabs == null)
        {
            Debug.LogError("O 'Prefab Corpo' não foi atribuído no Inspector da Snake!");
            return;
        }

        Vector3 posInicial = corpo.Count > 0
            ? corpo[corpo.Count - 1].transform.position
            : transform.position;

        GameObject novoSegmento = Instantiate(Prefabs, posInicial, Quaternion.identity);
        corpo.Add(novoSegmento);
    }
}
