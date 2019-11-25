using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteObject : MonoBehaviour
{
    public bool hit;
    public KeyCode keyCode;
    public int hitType;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (hit && Input.GetKeyDown(keyCode))
        {
            GameManager.Instance.NoteHit(hitType);
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "goodHitbox")
        {
            hitType = 1;
            hit = true;
        }
        else if (collision.tag == "greatHitBox")
        {
            hitType = 2;
            hit = true;
        }
        else if (collision.tag == "perfectHitBox")
        {
            hitType = 3;
            hit = true;
        }
        else if(collision.tag == "despawner")
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "goodHitBox")
        {
            hit = false;
            hitType = 0;
        }
    }
}
