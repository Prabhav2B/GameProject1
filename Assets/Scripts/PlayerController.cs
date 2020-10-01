using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{

    #region Variables

    [Space(20)]

    //Visual Representation of player Velocity
    public AnimationCurve movementCurve = new AnimationCurve();
    [Space(8)]
    public AnimationCurve movementDecayCurve = new AnimationCurve();

    [Space(20)]

    public PlayerMovementSettings movementSettings;

    private Rigidbody2D rb;
    private Vector2 dir;

    private Vector2 movementTimer;

    private Vector2 previousDir;
    private bool thisWillFixShit; //thisWillFixShit

    public Vector2 PlayerInput { get; private set; }

    DeathController dc;

    #endregion

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        dc = GetComponent<DeathController>();

        //Subscribing KillVelocity function to OnDeath Event
        dc.OnDeath += KillVelocity;

        dir = Vector2.zero;

        movementTimer = Vector2.zero;
        previousDir = Vector2.zero;

        thisWillFixShit = false;

        if (rb.gravityScale != 0f)
        {
            Debug.Break();
            throw new System.Exception("Kindly change gravity scale on Movement script only! Contact Pr0b0b for help.");
        }
    }

    private void Update()
    {
        //Gets Raw Axis Values : 1, 0, or -1
        PlayerInput = new Vector2 (Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
    }

    void FixedUpdate()
    {
        //Value evaluated from Movement Curves
        float curveValue = 0;

        for (int i = 0; i < 2; i++)
        {
            if (PlayerInput[i] != 0)
            {
                if (previousDir[i] != PlayerInput[i])
                {
                    float tempDir = rb.velocity[i] > 0 ? 1 : -1;

                    if (tempDir == PlayerInput[i])
                        movementTimer[i] = (movementTimer[i] / movementSettings.timeToFullyStop) * movementSettings.timeToReachFullSpeed;
                    else
                        movementTimer[i] = 0f;

                    previousDir[i] = PlayerInput[i];
                }

                movementTimer[i] += Time.fixedDeltaTime;
                movementTimer[i] = Mathf.Clamp(movementTimer[i], 0f, movementSettings.timeToReachFullSpeed);

                curveValue = movementCurve.Evaluate(movementTimer[i] / movementSettings.timeToReachFullSpeed);

                dir[i] += curveValue * PlayerInput[i];
                dir[i] = Mathf.Clamp(dir[i], -1f, 1f);


            }
            //Come to rest after a decay
            else
            {
                if (previousDir[i] != 0)
                {
                    movementTimer[i] = (movementTimer[i] / movementSettings.timeToReachFullSpeed) * movementSettings.timeToFullyStop;
                    previousDir[i] = PlayerInput[i];
                   
                    //hacky fix
                    if(i == 1)
                        thisWillFixShit = rb.velocity[i] > 0;
                }

                movementTimer[i] -= Time.fixedDeltaTime;
                movementTimer[i] = Mathf.Clamp(movementTimer[i], 0f, movementSettings.timeToFullyStop);

                curveValue = movementDecayCurve.Evaluate(movementTimer[i] / movementSettings.timeToFullyStop);

                //hacky fix
                if (i == 1)
                    dir[i] = !Mathf.Approximately(curveValue, 0f) ? curveValue * (thisWillFixShit ? 1 : -1) : 0;
                else
                    dir[i] = !Mathf.Approximately(curveValue, 0f) ? curveValue * (rb.velocity.x > 0 ? 1 : -1) : 0;

                


                dir[i] = Mathf.Clamp(dir[i], -1f, 1f);

            }

        }

        rb.velocity = (Walk(dir) * movementSettings.movementSpeed + ApplyGravity()) ;
       
    }

    private Vector2 ApplyGravity()
    { 
        return( Vector2.down * movementSettings.gravityScale );
    }

    private Vector2 Walk(Vector2 dir)
    {
        dir = Vector2.ClampMagnitude(dir, 1f);
        return(dir);
    }

    private void KillVelocity()
    {
        rb.velocity = Vector2.zero;
        rb.angularVelocity = 0f;

        dir = Vector2.zero;
        movementTimer = Vector2.zero;
        previousDir = Vector2.zero;

        thisWillFixShit = false; //FIX THIS
    }
    
}
