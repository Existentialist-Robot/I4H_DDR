using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using TMPro;
using System;
using UnityEngine.SceneManagement;

public class SongSelectParser : MonoBehaviour
{
    public static SongSelectParser Instance;

    public ScrollRect scrollView;
    public GameObject scrollContent;
    public GameObject scrollItemPrefab;
    public AudioSource audioSource;

    public GameObject infoContent;
    public GameObject songDetailPrefab;

    private List<Dictionary<string, string>> songs = new List<Dictionary<string, string>>();

    public Dictionary<string, string> selectedSong = new Dictionary<string, string>();

    static HashSet<string> metaInfo = new HashSet<string>
    {
        "AudioFilename",
        "PreviewTime",
        "Title",
        "Artist",
        "Creator",
        "Difficulty",
        "Source",
        "Tags",
        "LevelID",
        // "HPDrainRate",
        // "OverallDifficulty",
        "ApproachRate",

    };

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        Instance = this;
    }

    // start is called before the first frame update
    void Start()
    {
        CreateSongList();
        foreach (Dictionary<string, string> song in songs)
        {
            GenerateSongButton(song);
        }

        scrollView.verticalNormalizedPosition = 1;
    }

    void GenerateSongInfo(Dictionary<string, string> song)
    {
        try
        {
            GameObject.Destroy(infoContent.transform.GetChild(0).gameObject);
        }
        catch
        {

        }
        
        GameObject songInfo = Instantiate(songDetailPrefab);
        songInfo.transform.SetParent(infoContent.transform,false);
        
        songInfo.transform.Find("CoverPhoto").gameObject.GetComponent<RawImage>().texture = Resources.Load<Texture>(song["CoverPhoto"]);
        songInfo.transform.Find("SongLength").gameObject.GetComponent<TextMeshProUGUI>().text = "Song Length: " + song["SongLength"];
        songInfo.transform.Find("MapLength").gameObject.GetComponent<TextMeshProUGUI>().text = "Map Length: " + song["MapLength"];
        //songInfo.transform.Find("Bpm").gameObject.GetComponent<TextMeshProUGUI>().text = "Max BPM: " + song["MaxBpm"];
        // songInfo.transform.Find("MemorySegments").gameObject.GetComponent<TextMeshProUGUI>().text = "Memory Segments: " + song["MemorySegments"];
        // songInfo.transform.Find("HpDrain").gameObject.GetComponent<TextMeshProUGUI>().text = "HP Drain: " + song["HPDrainRate"];
        // songInfo.transform.Find("OverallDifficulty").gameObject.GetComponent<TextMeshProUGUI>().text = "Overall Difficulty: " + song["OverallDifficulty"];
        //songInfo.transform.Find("ApproachRate").gameObject.GetComponent<TextMeshProUGUI>().text = "Scroll Speed: " + song["ApproachRate"];


    }

    void GenerateSongButton(Dictionary<string, string> song)
    {
        GameObject songButton = Instantiate(scrollItemPrefab);

        songButton.transform.SetParent(scrollContent.transform, false);
        songButton.transform.Find("SongTitle").gameObject.GetComponent<TextMeshProUGUI>().text = song["Title"];
        songButton.transform.Find("ArtistCreator").gameObject.GetComponent<TextMeshProUGUI>().text = song["Artist"] + " // " + song["Creator"];

        songButton.GetComponent<Button>().onClick.AddListener(() => OnButtonClick(song));

    }

    void OnButtonClick(Dictionary<string, string> song)
    {
        audioSource.Stop();
        GenerateSongInfo(song);
        if (selectedSong.ContainsKey("AudioFilename") && selectedSong["AudioFilename"] == song["AudioFilename"])
        {
            SceneManager.LoadScene("Game");
        }
        else
        {
            audioSource.clip = Resources.Load<AudioClip>(song["SongFile"]);
            audioSource.Play();
            selectedSong = song;
        }
        
    }

    void CreateSongList()
    {
        foreach (string directory in Directory.GetDirectories("." + Path.DirectorySeparatorChar + "Assets" + Path.DirectorySeparatorChar + "Resources" + Path.DirectorySeparatorChar + "Songs"))
        {
            // Should only be one .ddr file in each song directory
            string songMap = Directory.GetFiles(directory, "*.ddr")[0];

            // Remove the file path for the mp3 and the extension
            string audioFile = Directory.GetFiles(directory, "*.mp3")[0].Replace("." + Path.DirectorySeparatorChar + "Assets" + Path.DirectorySeparatorChar + "Resources" + Path.DirectorySeparatorChar, "").Replace(".mp3", "");
            int songLength = (int) Resources.Load<AudioClip>(audioFile).length;
            string coverFile = Directory.GetFiles(directory, "*.png")[0].Replace("." + Path.DirectorySeparatorChar + "Assets" + Path.DirectorySeparatorChar + "Resources" + Path.DirectorySeparatorChar, "").Replace(".png", "").Replace(".jpeg", "");
            var songInfo = new Dictionary<string, string>
            {
                ["SongMap"] = songMap,
                ["Selected"] = "false",
                ["SongFile"] = audioFile,
                ["SongLength"] = (songLength / 60).ToString() + ":" + (songLength % 60).ToString(),
                ["CoverPhoto"] = coverFile
            };

            StreamReader file = new StreamReader(songMap);
            string line, prevLine = "";
            string[] parts;
            bool readHitObject = false;
            int startOffset = 0;
            while ((line = file.ReadLine()) != null)
            {
                parts = line.Split(':');
                if (metaInfo.Contains(parts[0]))
                {
                    songInfo[parts[0]] = parts[1];
                }
                else if (line == "#HitObjects")
                {
                    readHitObject = true;
                }
                else if (readHitObject)
                {
                    parts = line.Split(',');
                    Int32.TryParse(parts[1], out startOffset);
                    readHitObject = false;
                }
                prevLine = line;

            }
            parts = prevLine.Split(',');
            Int32.TryParse(parts[1], out int endOffset);

            int mapLength = endOffset - startOffset;
            songInfo["MapLength"] = ((mapLength / 1000) / 60).ToString() + ":" + ((mapLength % 1000) % 60);

            songs.Add(songInfo);
        }
    }
}
