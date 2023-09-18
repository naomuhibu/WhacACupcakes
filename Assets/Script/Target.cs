using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Target : MonoBehaviour
{
    private Rigidbody targetRb;
    private GameManager gameManager;
    public float timeOnScreen = 1.0f;

    private float minValueX = 300f;　
    private float minValueY = 1000f;
    private float spaceBetweenSquares = 500f; 
    public float moveSpeed = 1.0f;
    private float spawnAreaWidth = 800.0f;

    public ParticleSystem explosionParticle;
    public int pointValue;

    private Vector3 randomDirection;
    private float moveTimer;
    private float moveDuration = 100.0f;

    bool isOnPause;

    // Start is called before the first frame update
    void Start()
    {
        targetRb = GetComponent<Rigidbody>();
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        transform.position = RandomSpawnPosition();
        StartCoroutine(RemoveObjectRoutine());

        randomDirection = Random.insideUnitSphere.normalized;
        moveTimer = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        moveTimer += Time.deltaTime;

        if (moveTimer >= moveDuration)
        {
            randomDirection = Random.insideUnitSphere.normalized;　//new randam movement
            moveTimer = 0.0f;
        }

        Vector3 movement = randomDirection * moveSpeed * Time.deltaTime;
        transform.Translate(movement);
    }
    
    private void OnMouseDown()
    {
        Destroy(gameObject);
        Instantiate(explosionParticle, transform.position, explosionParticle.transform.rotation);
        gameManager.UpdateScore(pointValue);
    }

    Vector3 RandomSpawnPosition()
    {

        int randomIndex = RandomCupcakesIndex();
        float spawnPosX = minValueX + (randomIndex % 3 * spaceBetweenSquares);
        float spawnPosY = minValueY + (randomIndex / 3 * spaceBetweenSquares);
        Vector3 spawnPosition = new Vector3(spawnPosX, spawnPosY, 0);
        spawnPosition.x += Random.Range(-spawnAreaWidth / 3.0f, spawnAreaWidth / 3.0f);
        return spawnPosition;
    }

    int RandomCupcakesIndex()
    {
        return Random.Range(0, 4);
    }

    IEnumerator RemoveObjectRoutine()
    {
        yield return new WaitForSeconds(timeOnScreen);
        Destroy(gameObject);
    }

}
