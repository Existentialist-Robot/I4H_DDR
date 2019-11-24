using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public GameObject noteController;
    public AudioSource audioSource;

    // Used to determine song end
    private float songLength;
    private float timer;
    
    // Save when the song started
    private long startTime;

    // List of hit objects
    public List<HitObject> hitObjectsList = new List<HitObject>();
    int hitObjectIndex;
    
    // Keep track o
    public double score;
    private double noteBaseScore;
    
    public int numGoodHit;
    public int numGreatHit;
    public int numPerfectHit;

    void LoadSong()
    {
        Dictionary<string, string> song = SongSelectParser.Instance.selectedSong;
        string filename = song["SongMap"];
        AudioClip audio = Resources.Load<AudioClip>(song["SongFile"]);
        songLength = audio.length;
        audioSource.PlayOneShot(audio);

        System.IO.StreamReader beatmap = new System.IO.StreamReader(filename);
        string line;
        bool hitObjectsStart = false;
        string[] splitLine;

        while ((line = beatmap.ReadLine()) != null)
        {
            // System.Console.WriteLine(line);
            line = line.Trim();

            if (line.Length == 0 || line[0] == '/')
            {
                continue;
            }

            if (hitObjectsStart)
            {
                //Debug.Log(line);
                splitLine = line.Split(',');
                HitObject hitObject = new HitObject();
                hitObject.SetX(splitLine[0]);
                hitObject.SetOffset(splitLine[1]);
                hitObject.SetEndOffset(splitLine[2]);
                hitObjectsList.Add(hitObject);
            }

            if (line == "#HitObjects")
            {
                hitObjectsStart = true;
            }
        }
        beatmap.Close();
    }

    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
	
        LoadSong();

        startTime = 0;
        timer = 0.0f;
        score = 0;
        numGoodHit = 0;
        numGreatHit = 0;
        numPerfectHit = 0;
        noteBaseScore = 10;
        
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (startTime == 0)
        {
            startTime = DateTime.Now.Ticks;
        }

        long currentTime;
        while (hitObjectIndex < hitObjectsList.Count)
        {
            currentTime = (DateTime.Now.Ticks - startTime) / TimeSpan.TicksPerMillisecond;
            HitObject hitObject = hitObjectsList[hitObjectIndex];

            if (currentTime >= hitObject.GetOffset())
            {
                noteController.GetComponent<NotesController>().spawnNotes(hitObject);
                hitObjectIndex++;
            }
            else // If the next hitObject does not need to be spawned then break the loop
            {
                break;
            }
        }

        // If the song is over then go to the RankingPanel
        if (timer >= songLength)
        {
            SceneManager.LoadScene("RankingPanel");
        }

    }
}
