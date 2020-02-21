using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SongManager : MonoBehaviour
{
    public static SongManager Instance;
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
    
    // Keep track of score and number of hits
    public double score;
    private double noteBaseScore;
    public int numGoodHit;
    public int numGreatHit;
    public int numPerfectHit;

    private int difficulty;
    private int difficultyIndex;
    private string[] configuration;

    private int beatTempo;

    void LoadConfiguration()
    {
        // Get difficulty and configuration here
        difficulty = 4;
        configuration = new string[] { "1", "5", "3", "7" };
        //ArduinoController.Instance.SendConfiguration(configuration);

        // Change GameState from "Loading" to "Game on"
        //ArduinoController.Instance.UpdateGameState();
    }

    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
        // Change GameState from "Idle" to "Loading"
        //ArduinoController.Instance.UpdateGameState();
        LoadConfiguration();
	
        LoadSong();

        startTime = 0;
        timer = 0.0f;
        score = 0;
        numGoodHit = 0;
        numGreatHit = 0;
        numPerfectHit = 0;
        noteBaseScore = 10;

        difficultyIndex = 1;

        beatTempo = 180;
        
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
                if (difficultyIndex != difficulty)
                {
                    noteController.GetComponent<NotesController>().spawnNotes(hitObject, configuration, beatTempo);
                    hitObjectIndex++;
                    difficultyIndex++;
                }
                else
                {
                    difficultyIndex = 1;
                }
                
            }
            else // If the next hitObject does not need to be spawned then break the loop
            {
                break;
            }
        }

        // If the song is over then go to the RankingPanel
        if (timer >= songLength)
        {
            // Set the game state from "Game on" to "Idle"
            //ArduinoController.Instance.UpdateGameState();
            SceneManager.LoadScene("Results");
        }

    }

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

    public void NoteHit(int hitType)
    {
        if (hitType == 1)
        {
            numGoodHit++;
            score += 100;
        }
        else if (hitType == 2)
        {
            numGreatHit++;
            score += 300;
        }
        else if (hitType == 3)
        {
            numPerfectHit++;
            score += 500;
        }
        
    }

}
