using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class FallObjectControlScript : MonoBehaviour
{
    [Header("出現位置")]
    [SerializeField] GameObject point0;
    [SerializeField] GameObject point1;
    [SerializeField] GameObject point2;
    [SerializeField] GameObject point3;
    [SerializeField] GameObject point4;
    [SerializeField] GameObject point5;
    [SerializeField] GameObject point6;
    [SerializeField] GameObject point7;

    [Header("オブジェクトモデル")]
    [SerializeField] GameObject human;
    [SerializeField] GameObject rubble;

    GameObject[] spawnPoint = new GameObject[8];

    [Header("生成数")]
    public int spawnLimit = 12;
    public int spawnCount = 0;
    [Header("生成間隔")]
    [Range(0.1f,3f)]
    [SerializeField] float waitSec = 0.2f;

    public int count_spawnRubble;
    // Start is called before the first frame update
    void Start()
    {
        spawnPoint[0] = point0;
        spawnPoint[1] = point1;
        spawnPoint[2] = point2;
        spawnPoint[3] = point3;
        spawnPoint[4] = point4;
        spawnPoint[5] = point5;
        spawnPoint[6] = point6;
        spawnPoint[7] = point7;

        WaitForSeconds wait = new WaitForSeconds(waitSec);
        StartCoroutine(FallingCoroutine(wait));
    }

    // Update is called once per frame
    void Update()
    {

    }
    void GenerateFallingObjectFunc()
    {
        if (spawnCount < spawnLimit)
        {
            int n = Random.Range(0, spawnPoint.Length);
            int objectCategory = Random.Range(0, 10);

            if (objectCategory%2 == 0)
            {
                Instantiate(human, spawnPoint[n].transform.position, Quaternion.identity);
            }
            else if (objectCategory%2 == 1)
            {
                Instantiate(rubble, spawnPoint[n].transform.position, Quaternion.identity);
                count_spawnRubble++;
            }
            spawnCount++;
        }
    }
    IEnumerator FallingCoroutine(WaitForSeconds wait)
    {
        while (true)
        {
            GenerateFallingObjectFunc();
            yield return wait;
        }
    }
    
    public void OnTriggerStay(Collider other)
    {

    }
    public void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("FallingHuman")||other.CompareTag("FallingRubble"))
        {
            Destroy(other.gameObject);
            spawnCount--;
        }
    }
}
