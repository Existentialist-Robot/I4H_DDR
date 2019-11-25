﻿using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Results : MonoBehaviour
{
    public GameObject Score;
    public GameObject Accuracy;
    public GameObject GoodHits;
    public GameObject GreatHits;
    public GameObject PerfectHits;

    // Start is called before the first frame update
    void Start()
    {
        Score.GetComponent<TextMeshProUGUI>().text = "Score             : " + GameManager.Instance.score.ToString();
        Accuracy.GetComponent<TextMeshProUGUI>().text = "Accuracy      : %" +
            (((GameManager.Instance.numGoodHit + GameManager.Instance.numGreatHit + GameManager.Instance.numPerfectHit) 
                / GameManager.Instance.hitObjectsList.Count) * 100).ToString();
        GoodHits.GetComponent<TextMeshProUGUI>().text = "Good Hits     : " + GameManager.Instance.numGoodHit.ToString();
        GreatHits.GetComponent<TextMeshProUGUI>().text = "Great Hits    : " + GameManager.Instance.numGreatHit.ToString();
        PerfectHits.GetComponent<TextMeshProUGUI>().text = "Perfect Hits : " + GameManager.Instance.numPerfectHit.ToString();

    }

    // When the Retry button is clicked
    public void Retry()
    {
        SceneManager.LoadScene("Game");
    }

    // When the Back button is clicked
    public void Back()
    {
        SceneManager.LoadScene("SongSelect");
    }
}