using TMPro;
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
        Score.GetComponent<TextMeshProUGUI>().text = "Score             : " + SongManager.Instance.score.ToString();
        Accuracy.GetComponent<TextMeshProUGUI>().text = "Accuracy      : %" +
            (((SongManager.Instance.numGoodHit + SongManager.Instance.numGreatHit + SongManager.Instance.numPerfectHit) 
                / SongManager.Instance.hitObjectsList.Count) * 100).ToString();
        GoodHits.GetComponent<TextMeshProUGUI>().text = "Good Hits     : " + SongManager.Instance.numGoodHit.ToString();
        GreatHits.GetComponent<TextMeshProUGUI>().text = "Great Hits    : " + SongManager.Instance.numGreatHit.ToString();
        PerfectHits.GetComponent<TextMeshProUGUI>().text = "Perfect Hits : " + SongManager.Instance.numPerfectHit.ToString();

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
