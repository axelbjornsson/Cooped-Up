using UnityEngine;

[RequireComponent(typeof(Controller2D))]
public class Player : MonoBehaviour
{

    public float maxJumpHeight = 4f;
    public float minJumpHeight = 1f;
    public float timeToJumpApex = .4f;
    private float accelerationTimeAirborne = .2f;
    private float accelerationTimeGrounded = .1f;
    private float moveSpeed = 6f;

    public Vector2 wallJumpClimb;
    public Vector2 wallJumpOff;
    public Vector2 wallLeap;

    public bool canDoubleJump;
    private bool isDoubleJumping = false;

    public float wallSlideSpeedMax = 3f;
    public float wallStickTime = .25f;

    public enum PlayerType
    {
        digger,
        jumper
    }
    public PlayerType playerType;

    private float timeToWallUnstick;

    private float gravity;
    private float maxJumpVelocity;
    private float minJumpVelocity;
    private Vector3 velocity;
    private float velocityXSmoothing;

    [HideInInspector]
    public Controller2D controller;

    private Vector2 directionalInput;
    private bool wallSliding;
    private int wallDirX;

    [Tooltip("How fast should the character dash. Note: this only works on digging player type")]
    public float dashSpeed;
    private bool dashing;
    private bool hasDashed;

    [Tooltip("to ignore the other player's rigidbody, we keep each other's colliders to ignore")]
    public BoxCollider2D otherPlayerCollider;

    private void Start()
    {
        controller = GetComponent<Controller2D>();
        gravity = -(2 * maxJumpHeight) / Mathf.Pow(timeToJumpApex, 2);
        maxJumpVelocity = Mathf.Abs(gravity) * timeToJumpApex;
        minJumpVelocity = Mathf.Sqrt(2 * Mathf.Abs(gravity) * minJumpHeight);

        Physics2D.IgnoreCollision(GetComponent<BoxCollider2D>(), otherPlayerCollider);
    }

    private void Update()
    {
        CalculateVelocity();
        if(playerType == PlayerType.jumper)
        {
            HandleWallSliding();
        }
        if(dashing)
        {
            for (int i = 0; i < controller.horizontalRayCount; i++)
            {
                Vector2 rayOrigin = directionalInput.x == -1 ? controller.raycastOrigins.bottomLeft : controller.raycastOrigins.bottomRight;
                rayOrigin += Vector2.up * (controller.horizontalRaySpacing * i);
                RaycastHit2D hit = Physics2D.Raycast(rayOrigin, directionalInput, 0.5f);

                Debug.DrawRay(rayOrigin, directionalInput*5, Color.red);

                if (hit.collider != null && hit.collider.gameObject.tag == "CompoundBlock")
                {
                    Destroy(hit.collider.gameObject);
                }
            }
            /*RaycastHit2D hit = Physics2D.Raycast(this.transform.position, directionalInput * 10f, 0.5f);
            Debug.DrawRay(this.transform.position, directionalInput * 10f);
            if (hit.collider != null && hit.collider.gameObject.tag == "CompoundBlock")
            {
                Destroy(hit.collider.gameObject);
            }*/
        }

        controller.Move(velocity * Time.deltaTime, directionalInput);

        if (controller.collisions.above || controller.collisions.below)
        {
            dashing = false;
            hasDashed = false;
            velocity.y = 0f;
        }
    }

    public void SetDirectionalInput(Vector2 input)
    {
        directionalInput = input;
    }

    public void OnJumpInputDown()
    {
        if (wallSliding)
        {
            if (wallDirX == directionalInput.x)
            {
                velocity.x = -wallDirX * wallJumpClimb.x;
                velocity.y = wallJumpClimb.y;
            }
            else if (directionalInput.x == 0)
            {
                velocity.x = -wallDirX * wallJumpOff.x;
                velocity.y = wallJumpOff.y;
            }
            else
            {
                velocity.x = -wallDirX * wallLeap.x;
                velocity.y = wallLeap.y;
            }
            isDoubleJumping = false;
        }

        if (controller.collisions.below)
        {
            velocity.y = maxJumpVelocity;
            isDoubleJumping = false;
        }
        if (canDoubleJump && !controller.collisions.below && !isDoubleJumping && !wallSliding)
        {
            velocity.y = maxJumpVelocity;
            isDoubleJumping = true;
        }
    }

    public void Bounce(float bounciness)
    {
        velocity.y = maxJumpVelocity * bounciness;
    }

    public void OnJumpInputUp()
    {
        if (velocity.y > minJumpVelocity)
        {
            velocity.y = minJumpVelocity;
        }
    }

    private void HandleWallSliding()
    {
        wallDirX = (controller.collisions.left) ? -1 : 1;
        wallSliding = false;
        if ((controller.collisions.left || controller.collisions.right) && !controller.collisions.below && velocity.y < 0)
        {
            wallSliding = true;

            if (velocity.y < -wallSlideSpeedMax)
            {
                velocity.y = -wallSlideSpeedMax;
            }

            if (timeToWallUnstick > 0f)
            {
                velocityXSmoothing = 0f;
                velocity.x = 0f;
                if (directionalInput.x != wallDirX && directionalInput.x != 0f)
                {
                    timeToWallUnstick -= Time.deltaTime;
                }
                else
                {
                    timeToWallUnstick = wallStickTime;
                }
            }
            else
            {
                timeToWallUnstick = wallStickTime;
            }
        }
    }

    public void Dash()
    {
        if(!hasDashed)
        {
            hasDashed = true;
            dashing = true;
            velocity.y = 0f;
            StartCoroutine(DashWait());
        }
    }

    System.Collections.IEnumerator DashWait()
    {
        yield return new WaitForSeconds(0.15f);
        dashing = false;
    }
    

    private void CalculateVelocity()
    {
        if(dashing)
        {
            velocity.x = directionalInput.x * dashSpeed;
        }
        else
        {
            float targetVelocityX = directionalInput.x * moveSpeed;
            velocity.x = Mathf.SmoothDamp(velocity.x, targetVelocityX, ref velocityXSmoothing, (controller.collisions.below ? accelerationTimeGrounded : accelerationTimeAirborne));
            velocity.y += gravity * Time.deltaTime;
        }
    }
}
