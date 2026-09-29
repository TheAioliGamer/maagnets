using UnityEngine;

public class gDetection : MonoBehaviour
{
    public Player player;


    //Triggers whenever the player is touching a surface from the top
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == ("Ground"))
        {
            player.grounded = true;
            Debug.Log("poo");
        }
    }
}
