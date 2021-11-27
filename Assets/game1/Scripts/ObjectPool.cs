using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ObjectPool : MonoBehaviour
{

    [SerializeField] GameObject enemy;
    [SerializeField][Range(0, 50)]int poolsize;
    [SerializeField][Range(0.1f,30f)]float spawnGap;
    [SerializeField] float timeToNextWave=2;
    [SerializeField] Text timerText;

    GameObject[] objectPool;
    public int numberOfEnemy=0;
    float countDownTimer;
    
    void Awake()
    {
        GeneratePool();
        countDownTimer = timeToNextWave;
    }
    private void Update()
    {
        timerText.text =string.Format("{0:00.00}", countDownTimer) ;
        if (numberOfEnemy >= poolsize)
        {
            //Debug.Log("Last Round");
            return;

        }
        if (GameManager.Instance.enemyCount > 0)
        {
            countDownTimer = timeToNextWave;
            return;
        }
        if (countDownTimer <= 0)
        {
            numberOfEnemy++;
            GameManager.Instance.rounds++;
            countDownTimer = timeToNextWave;
            StartCoroutine(CreateEnemy());
        }
        countDownTimer -= Time.deltaTime;
    }
    IEnumerator CreateEnemy()
    {
        for (int i = 0; i < numberOfEnemy; i++)
        {
            objectPool[i].SetActive(true);
            GameManager.Instance.enemyCount++;
            yield return new WaitForSeconds(spawnGap);
        }
       
    }
    void GeneratePool()
    {
        objectPool = new GameObject[poolsize];
        for(int i = 0; i < poolsize; i++)
        {
            objectPool[i] = Instantiate(enemy,transform);
            objectPool[i].SetActive(false);
        }
    }
    public void notifyReceivers()
    {
        BroadcastMessage("RecalculatePath", false, SendMessageOptions.DontRequireReceiver);
    }
   

}
