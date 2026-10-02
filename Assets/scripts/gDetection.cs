using UnityEngine;

public class gDetection : MonoBehaviour
{
    public Player player;


    //Triggers whenever the player is touching a surface from the top
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == ("Ground") || other.tag == ("Nonground"))
        {
            player.grounded = true;
            player.rb.linearVelocity = Vector3.zero;
        }
        else if (other.tag == ("Wall") || other.tag == ("Nonwall"))
        {
            player.walled = true;
            player.wallJump = other.transform.up - other.transform.right;
            player.rb.linearVelocity = Vector3.zero;
        }
        else if (other.tag == ("Ceiling") || other.tag == ("Nonceiling"))
        {
            player.ceilinged = true;
            player.rb.linearVelocity = Vector3.zero;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == ("Wall") || other.tag == ("Nonwall"))
        {
            player.walled = false;
        }
        else if (other.tag == ("Ceiling") || other.tag == ("Nonceiling"))
        {
            player.ceilinged = false;
        }
    }
}
