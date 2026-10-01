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
        }
        else if (other.tag == ("Wall"))
        {
            player.walled = true;
            player.wallJump = other.transform.up - other.transform.right;
        }
        else if ( other.tag == ("Ceiling"))
        {
            player.ceilinged = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        
    }
}
