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
    public GameObject rightArrow;

    public GameObject parentObject;

    private int beatTempo;

    // TODO: FIX THE OTHER ARROWS SPAWNING
    void spawnLeftArrow()
    {
        //var currentArrow = Instantiate(leftArrow, leftSpawner.position, leftSpawner.rotation);

        //var currentArrow = Instantiate(leftArrow, new Vector3(leftSpawner.position.x, leftSpawner.position.y, leftSpawner.position.z - 1), leftSpawner.rotation);
        var currentArrow = Instantiate(leftArrow, new Vector2(leftSpawner.position.x, leftSpawner.position.y), leftSpawner.rotation);
        //currentArrow.transform.SetParent(parentObject.transform);
        currentArrow.transform.localScale = new Vector2(5f, 5f);

        currentArrow.GetComponent<NoteObject>().SetBeatTempo(beatTempo);
        //Debug.Log($"Left arrow: {currentArrow.transform.position.x} {currentArrow.transform.position.y} {currentArrow.transform.position.z}");
    }

    void spawnDownLeftArrow()
    {
        //var currentArrow = Instantiate(downLeftArrow, leftSpawner.position, leftSpawner.rotation);
        var currentArrow = Instantiate(downLeftArrow, new Vector3(leftSpawner.position.x, leftSpawner.position.y, 0f), leftSpawner.rotation);
        currentArrow.transform.SetParent(parentObject.transform);
        currentArrow.transform.localScale = new Vector3(1.15f, 0.85f, 0f);
    }

    void spawnUpLeftArrow()
    {
        //var currentArrow = Instantiate(upLeftArrow, leftSpawner.position, leftSpawner.rotation);
        var currentArrow = Instantiate(upLeftArrow, new Vector3(leftSpawner.position.x, leftSpawner.position.y, 0f), leftSpawner.rotation);
        currentArrow.transform.SetParent(parentObject.transform);
        currentArrow.transform.localScale = new Vector3(1.15f, 0.85f, 0f);
    }

    void spawnUpArrow()
    {
        // var currentArrow = Instantiate(upArrow, upSpawner.position, upSpawner.rotation);
        var currentArrow = Instantiate(upArrow, new Vector3(upSpawner.position.x, upSpawner.position.y, 0f), upSpawner.rotation);
        currentArrow.transform.SetParent(parentObject.transform);
        currentArrow.transform.localScale = new Vector3(5f, 5f, 5f);

        //Debug.Log($"Up arrow: {currentArrow.transform.position.x} {currentArrow.transform.position.y} {currentArrow.transform.position.z}");
    }

    void spawnDownArrow()
    {
        //var currentArrow = Instantiate(downArrow, downSpawner.position, downSpawner.rotation);
        var currentArrow = Instantiate(downArrow, new Vector3(downSpawner.position.x, downSpawner.position.y, 0f), downSpawner.rotation);
        currentArrow.transform.SetParent(parentObject.transform);
        currentArrow.transform.localScale = new Vector3(5f, 5f, 5f);

        //Debug.Log($"Down arrow: {currentArrow.transform.position.x} {currentArrow.transform.position.y} {currentArrow.transform.position.z}");
    }

    void spawnUpRightArrow()
    {
        //var currentArrow = Instantiate(upRightArrow, rightSpawner.position, rightSpawner.rotation);
        var currentArrow = Instantiate(upRightArrow, new Vector3(rightSpawner.position.x, rightSpawner.position.y, 0f), rightSpawner.rotation);
        currentArrow.transform.SetParent(parentObject.transform);
        currentArrow.transform.localScale = new Vector3(1.15f, 0.85f, 0f);

        //Debug.Log(currentArrow.transform.position);
    }

    void spawnDownRightArrow()
    {
        //var currentArrow = Instantiate(downRightArrow, rightSpawner.position, rightSpawner.rotation);
        var currentArrow = Instantiate(downRightArrow, new Vector3(rightSpawner.position.x, rightSpawner.position.y, 0f), rightSpawner.rotation);
        currentArrow.transform.SetParent(parentObject.transform);
        currentArrow.transform.localScale = new Vector3(1.15f, 0.85f, 0f);
    }

    void spawnRightArrow()
    {
        //var currentArrow = Instantiate(RightArrow, rightSpawner.position, rightSpawner.rotation);
        var currentArrow = Instantiate(rightArrow, new Vector3(rightSpawner.position.x, rightSpawner.position.y, 0f), rightSpawner.rotation);
        currentArrow.transform.SetParent(parentObject.transform);
        currentArrow.transform.localScale = new Vector3(5f, 5f, 5f);

        //Debug.Log($"Right arrow: {currentArrow.transform.position.x} {currentArrow.transform.position.y} {currentArrow.transform.position.z}");
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
