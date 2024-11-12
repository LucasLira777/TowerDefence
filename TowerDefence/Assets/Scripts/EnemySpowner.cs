using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

 
public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] enemyPrefabs; // Array de prefabs de inimigos que podem ser instanciados
    [SerializeField] private int baseEnemies = 8; // Número base de inimigos em cada onda
    [SerializeField] private float enemiesPerSecond = 0.5f; // Número de inimigos gerados por segundo no início do jogo
    [SerializeField] private float timeBetweenWaves = 5f; // Tempo entre ondas de inimigos
    [SerializeField] private float difficultyScalingFactor = 0.75f; // Fator de escala para aumentar a dificuldade em cada onda
    [SerializeField] private float enemiesPerSecondCap = 15f; // Limite máximo para inimigos gerados por segundo

    public static UnityEvent onEnemyDestroy = new UnityEvent(); // Evento estático chamado quando um inimigo é destruído

    private int currentWave = 1; // Número da onda atual
    private float timeSinceLastSpawn; // Tempo decorrido desde o último spawn de inimigo
    private int enemiesAlive; // Contador de inimigos vivos
    private int enemiesLeftToSpawn; // Contador de inimigos restantes para spawn na onda atual
    private float eps; // Inimigos por segundo ajustado para a onda atual
    private bool isSpawning = false; // Indica se os inimigos estão sendo gerados

    private void Awake()
    {
        // Adiciona um listener para reduzir o contador de inimigos quando um inimigo é destruído
        onEnemyDestroy.AddListener(EnemyDestroyed);
    }

    private void Start()
    {
        // Inicia a primeira onda
        StartCoroutine(StartWave());
    }

    private void EnemyDestroyed()
    {
        // Reduz o número de inimigos vivos quando um inimigo é destruído
        enemiesAlive--;
    }

    private IEnumerator StartWave()
    {
        // Espera o tempo especificado entre as ondas
        yield return new WaitForSeconds(timeBetweenWaves);

        isSpawning = true; // Inicia o spawn de inimigos
        enemiesLeftToSpawn = EnemiesPerWave(); // Calcula o número de inimigos para spawn nesta onda
        eps = EnemiesPerSecond(); // Calcula a taxa de spawn para esta onda
    }

    private int EnemiesPerWave()
    {
        // Calcula o número de inimigos para spawn com base na dificuldade e no número da onda
        return Mathf.RoundToInt(baseEnemies * Mathf.Pow(currentWave, difficultyScalingFactor));
    }

    // Update é chamado a cada frame
    void Update()
    {
        if (!isSpawning) return; // Se não estiver gerando, retorna

        timeSinceLastSpawn += Time.deltaTime; // Atualiza o tempo desde o último spawn

        if (timeSinceLastSpawn >= (1f / eps) && enemiesLeftToSpawn > 0)
        {
            // Se o tempo desde o último spawn for suficiente, gera um novo inimigo
            SpawnEnemy();
            enemiesLeftToSpawn--; // Reduz o número de inimigos restantes para spawn
            enemiesAlive++; // Aumenta o contador de inimigos vivos
            timeSinceLastSpawn = 0f; // Reinicia o tempo desde o último spawn
        }

        if (enemiesAlive == 0 && enemiesLeftToSpawn == 0)
        {
            // Se não houver mais inimigos vivos e para spawn, termina a onda
            EndWave();
        }
    }

    private void EndWave()
    {
        // Reseta variáveis e inicia a próxima onda
        isSpawning = false;
        timeSinceLastSpawn = 0f;
        currentWave++; // Incrementa o número da onda
        StartCoroutine(StartWave()); // Inicia a próxima onda
    }

    private void SpawnEnemy()
    {
        // Escolhe aleatoriamente um inimigo para spawn e o instancia no ponto de partida
        int index = Random.Range(0, enemyPrefabs.Length);
        GameObject prefabToSpawn = enemyPrefabs[index];
        Instantiate(prefabToSpawn, LevelManager.instance.startPoint.position, Quaternion.identity);
    }

    private float EnemiesPerSecond()
    {
        // Calcula a taxa de spawn de inimigos, aumentando com a dificuldade e limitando ao cap
        return Mathf.Clamp(enemiesPerSecond * Mathf.Pow(currentWave, difficultyScalingFactor), 0f, enemiesPerSecondCap);
    }
}

 