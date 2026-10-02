using UnityEditor;
using UnityEngine;

public class surface : MonoBehaviour
{
    public bool seen;

    public float lastTimeSeen;
    public float unSeenTime = 0.1f;


    public bool Seen
    {
        get { return seen; }
        set
        {
            seen = value;

            if(seen)
            {
                lastTimeSeen = Time.time + unSeenTime;
            }
            else
            {
                lastTimeSeen = 0;
            }
        }
    }


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > lastTimeSeen) // timer
        {
            Seen = false;
        }
        Renderer redner = GetComponent<Renderer>();
        if (redner != null && seen == true)
        {
            if (redner.CompareTag("Ground") || redner.CompareTag("Wall") || redner.CompareTag("Ceiling"))
            {
                if (seen)
                {
                   // if (lastSeen != null)
                    {
                        Color c = redner.material.color;
                        c.r = 1;
                        c.g = 1;
                        redner.material.color = c;
                    }
                    Color heehee = redner.material.color;
                    heehee.r = 0.6f;
                    heehee.g = 0.6f;
                    redner.material.color = heehee;
                }
                /*Color heehee = redner.material.color;
                heehee.r = 0.6f;
                heehee.g = 0.6f;
                redner.material.color = heehee;
                lastSeen = redner;*/
            }
        }
        else
        {
            if (redner.CompareTag("Ground") || redner.CompareTag("Wall") || redner.CompareTag("Ceiling"))
            {
                Color c = redner.material.color;
                c.r = 1;
                c.g = 1;
                redner.material.color = c;
            }
        }
    }
}
