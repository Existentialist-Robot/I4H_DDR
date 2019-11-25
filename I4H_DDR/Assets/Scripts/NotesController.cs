using System.Collections;
using UnityEngine;

public class NotesController : MonoBehaviour
{
    public Transform leftSpawner;
    public Transform upSpawner;
    public Transform downSpawner;
    public Transform rightSpawner;

    public GameObject leftArrow;
    public GameObject downLeftArrow;
    public GameObject upLeftArrow;
    public GameObject upArrow;
    public GameObject downArrow;
    public GameObject upRightArrow;
    public GameObject downRightArrow;
    public GameObject RightArrow;

    public GameObject parentObject;

    private int beatTempo;

    void spawnLeftArrow()
    {
        //var currentArrow = Instantiate(leftArrow, leftSpawner.position, leftSpawner.rotation);

        var currentArrow = Instantiate(leftArrow, new Vector3(leftSpawner.position.x, leftSpawner.position.y, 0f), leftSpawner.rotation);
        currentArrow.GetComponent<NoteObject>().SetBeatTempo(beatTempo);
        //currentArrow.transform.SetParent(parentObject.transform);
        currentArrow.transform.localScale = new Vector3(1.15f, 0.85f, 0f);
    }

    void spawnDownLeftArrow()
    {
        //var currentArrow = Instantiate(downLeftArrow, leftSpawner.position, leftSpawner.rotation);
        var currentArrow = Instantiate(downLeftArrow, new Vector3(leftSpawner.position.x, leftSpawner.position.y, 0f), leftSpawner.rotation);
        currentArrow.transform.SetParent(parentObject.transform);
        //currentArrow.transform.localScale = new Vector3(1.15f, 0.85f, 0f);
    }

    void spawnUpLeftArrow()
    {
        var currentArrow = Instantiate(upLeftArrow, leftSpawner.position, leftSpawner.rotation);
        currentArrow.transform.SetParent(parentObject.transform);
        //currentArrow.transform.localScale = new Vector3(1.15f, 0.85f, 0f);
    }

    void spawnUpArrow()
    {
        var currentArrow = Instantiate(upArrow, upSpawner.position, upSpawner.rotation);
        currentArrow.transform.SetParent(parentObject.transform);
        //currentArrow.transform.localScale = new Vector3(1.0f, 0.85f, 0f);
    }

    void spawnDownArrow()
    {
        var currentArrow = Instantiate(downArrow, downSpawner.position, downSpawner.rotation);
        currentArrow.transform.SetParent(parentObject.transform);
        //currentArrow.transform.localScale = new Vector3(1.0f, 0.85f, 0f);
    }

    void spawnUpRightArrow()
    {
        var currentArrow = Instantiate(upRightArrow, rightSpawner.position, rightSpawner.rotation);
        currentArrow.transform.SetParent(parentObject.transform);
        //currentArrow.transform.localScale = new Vector3(1.15f, 0.85f, 0f);
    }

    void spawnDownRightArrow()
    {
        var currentArrow = Instantiate(downRightArrow, rightSpawner.position, rightSpawner.rotation);
        currentArrow.transform.SetParent(parentObject.transform);
        //currentArrow.transform.localScale = new Vector3(1.15f, 0.85f, 0f);
    }

    void spawnRightArrow()
    {
        var currentArrow = Instantiate(RightArrow, rightSpawner.position, rightSpawner.rotation);
        currentArrow.transform.SetParent(parentObject.transform);
        //currentArrow.transform.localScale = new Vector3(1.15f, 0.85f, 0f);
    }

    public void spawnNotes(HitObject hitObject, string[] configuration, int beatTempo)
    {
        this.beatTempo = beatTempo;
        if (hitObject.GetX() == 36 && (((IList)configuration).Contains("5") || ((IList)configuration).Contains("4")))
        {
            spawnLeftArrow();
        }
        //else if (hitObject.GetX() == 109 && (((IList)configuration).Contains("0") || ((IList)configuration).Contains("4")))
        //{
        //    spawnDownLeftArrow();
        //}
        //else if (hitObject.GetX() == 182 && (((IList)configuration).Contains("6") || ((IList)configuration).Contains("4")))
        //{
        //    spawnUpLeftArrow();
        //}
        //else if (hitObject.GetX() == 224 && (((IList)configuration).Contains("7") || ((IList)configuration).Contains("4")))
        //{
        //    spawnUpArrow();
        //}
        //else if (hitObject.GetX() == 288 && (((IList)configuration).Contains("1") || ((IList)configuration).Contains("4")))
        //{
        //    spawnDownArrow();
        //}
        //else if (hitObject.GetX() == 329 && (((IList)configuration).Contains("8") || ((IList)configuration).Contains("4")))
        //{
        //    spawnUpRightArrow();
        //}
        //else if (hitObject.GetX() == 402 && (((IList)configuration).Contains("2") || ((IList)configuration).Contains("4")))
        //{
        //    spawnDownRightArrow();
        //}
        //else if (hitObject.GetX() == 475 && (((IList)configuration).Contains("3") || ((IList)configuration).Contains("4")))
        //{
        //    spawnRightArrow();
        //}
    }
}
