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

    void spawnLeftArrow()
    {
        var currentRing = Instantiate(leftArrow, leftSpawner.position, leftSpawner.rotation);
        currentRing.transform.SetParent(parentObject.transform);
        currentRing.transform.localScale = new Vector2(1.15f, 0.85f);
    }

    void spawnDownLeftArrow()
    {
        var currentRing = Instantiate(downLeftArrow, leftSpawner.position, leftSpawner.rotation);
        currentRing.transform.SetParent(parentObject.transform);
        currentRing.transform.localScale = new Vector2(1.15f, 0.85f);
    }

    void spawnUpLeftArrow()
    {
        var currentRing = Instantiate(upLeftArrow, leftSpawner.position, leftSpawner.rotation);
        currentRing.transform.SetParent(parentObject.transform);
        currentRing.transform.localScale = new Vector2(1.15f, 0.85f);
    }

    void spawnUpArrow()
    {
        var currentRing = Instantiate(upArrow, upSpawner.position, upSpawner.rotation);
        currentRing.transform.SetParent(parentObject.transform);
        currentRing.transform.localScale = new Vector2(1.0f, 0.85f);
    }

    void spawnDownArrow()
    {
        var currentRing = Instantiate(downArrow, downSpawner.position, downSpawner.rotation);
        currentRing.transform.SetParent(parentObject.transform);
        currentRing.transform.localScale = new Vector2(1.0f, 0.85f);
    }

    void spawnUpRightArrow()
    {
        var currentRing = Instantiate(upRightArrow, rightSpawner.position, rightSpawner.rotation);
        currentRing.transform.SetParent(parentObject.transform);
        currentRing.transform.localScale = new Vector2(1.15f, 0.85f);
    }

    void spawnDownRightArrow()
    {
        var currentRing = Instantiate(downRightArrow, rightSpawner.position, rightSpawner.rotation);
        currentRing.transform.SetParent(parentObject.transform);
        currentRing.transform.localScale = new Vector2(1.15f, 0.85f);
    }

    void spawnRightArrow()
    {
        var currentRing = Instantiate(RightArrow, rightSpawner.position, rightSpawner.rotation);
        currentRing.transform.SetParent(parentObject.transform);
        currentRing.transform.localScale = new Vector2(1.15f, 0.85f);
    }

    public void spawnNotes(HitObject hitObject)
    {
        if (hitObject.GetX() == 36)
        {
            spawnLeftArrow();
        }
        else if (hitObject.GetX() == 109)
        {
            spawnDownLeftArrow();
        }
        else if (hitObject.GetX() == 182)
        {
            spawnUpLeftArrow();
        }
        else if (hitObject.GetX() == 224)
        {
            spawnUpArrow();
        }
        else if (hitObject.GetX() == 288)
        {
            spawnDownArrow();
        }
        else if (hitObject.GetX() == 329)
        {
            spawnUpRightArrow();
        }
        else if (hitObject.GetX() == 402)
        {
            spawnDownRightArrow();
        }
        else if (hitObject.GetX() == 475)
        {
            spawnRightArrow();
        }
    }
}
