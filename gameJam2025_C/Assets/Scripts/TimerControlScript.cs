using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class TimerControlScript : MonoBehaviour
{
    // Start is called before the first frame update
    [Header("制限時間")]
    [Range(1, 10)]
    [SerializeField] int timeLimit_min;
    [Tooltip("タイマーのテキスト")]
    [SerializeField] TMPro.TMP_Text timerText_sec;
    float time = 0;
    [SerializeField] string sceneName;

    void Start()
    {
        Time.timeScale = 1.0f;
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if (time > timeLimit_min*60)
        {
            Time.timeScale = 0;
            SceneManager.LoadScene(sceneName);
        }
       
        float limitTime_sec= (timeLimit_min * 60 - time);
        timerText_sec.text=limitTime_sec.ToString("F0");
        //Debug.Log((timeLimit_min * 60 - time) / 60);
    }

}
