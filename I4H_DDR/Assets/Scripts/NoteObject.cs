using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteObject : MonoBehaviour
{
    public bool hit;
    public KeyCode keyCode;
    public int hitType;
    public int beatTempo;

    public int timer= 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void SetBeatTempo(int beatTempo)
    {
        this.beatTempo = beatTempo;
    }

    // Update is called once per frame
    void Update()
    {

        Debug.Log($"val: {hit} : {Input.GetKeyDown(keyCode)}");
        if (hit && Input.GetKeyDown(KeyCode.F))
        {
            Debug.Log("Success");
            GameManager.Instance.NoteHit(hitType);
            gameObject.SetActive(false);
            Destroy(gameObject);
        }

        transform.position -= new Vector3(0f, beatTempo * Time.deltaTime, 0f);

        timer++;

        if (timer == 1000)
        {
            //Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.tag);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Collistion tag: " + collision.tag);
        if (collision.tag == "PerfectHitBox")
        {
            hitType = 3;
            hit = true;
        }
        else if (collision.tag == "GreatHitBox")
        {
            hitType = 2;
            hit = true;
        }
        else if (collision.tag == "GoodHitbox")
        {
            hitType = 1;
            hit = true;
        }
        else if(collision.tag == "despawner")
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "GoodHitBox")
        {
            hit = false;
            hitType = 0;
        }
    }
}
