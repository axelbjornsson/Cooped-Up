using UnityEngine;

[RequireComponent(typeof(Player))]
public class Player2Input : MonoBehaviour
{
    private Player player;

    private void Start()
    {
        player = GetComponent<Player>();
    }

    private void Update()
    {
        Vector2 directionalInput = new Vector2(Input.GetAxisRaw("Horizontal2"), 0);
        player.SetDirectionalInput(directionalInput);

        if (Input.GetButtonDown("Jump2"))
        {
            player.OnJumpInputDown();
        }

        if (Input.GetButtonUp("Jump2"))
        {
            player.OnJumpInputUp();
        }

        if(Input.GetButtonDown("Jump2") && !player.controller.collisions.below)
        {
            player.Dash();
        }
    }
}
