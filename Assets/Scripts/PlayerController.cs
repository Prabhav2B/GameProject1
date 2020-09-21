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


    [Range(.1f, 10f)]
    public float movementSpeed = 10;

    [Range(0.0f, 5.0f)]
    public float timeToReachFullSpeed = .5f;

    [Range(0.0f, 5.0f)]
    public float timeToFullyStop = .5f;

    [Range(0.0f, 5.0f)]
    public float gravityScale = .5f;



    private Rigidbody2D rb;
    private Vector2 dir;
    private float movementTimerX;
    private float movementTimerY;
    
    private float previousX;
    private float previousY;
    private bool thisWillFixShit; //thisWillFixShit
    #endregion

    public float InputX { get; private set; }
    public float InputY { get; private set; }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
            
        dir.x = 0f;
        dir.y = 0f;
        movementTimerX = 0f;
        movementTimerY = 0f;
        
        previousX = 0f;
        previousY = 0f;

        if (rb.gravityScale != 0f)
        {
            Debug.Break();
            throw new System.Exception("Kindly change gravity scale on Movement script only! Contact Pr0b0b for help.");
        }
    }

    private void Update()
    {
        //Gets Raw Axis Values : 1, 0, or -1
        InputX = Input.GetAxisRaw("Horizontal");
        InputY = Input.GetAxisRaw("Vertical");
    }

    void FixedUpdate()
    {
        //Value evaluated from Movement Curves
        float curveValue = 0;

        if (InputX != 0)
        {
            if (previousX != InputX)
            {
                movementTimerX = 0f;
                previousX = InputX;
            }

            movementTimerX += Time.fixedDeltaTime;
            movementTimerX = Mathf.Clamp(movementTimerX, 0f, timeToReachFullSpeed);

            curveValue = movementCurve.Evaluate(movementTimerX / timeToReachFullSpeed);

            dir.x += curveValue * InputX;
            dir.x = Mathf.Clamp(dir.x, -1f, 1f);


        }
        //Come to rest after a decay
        else
        {
            if (previousX != 0)
            {
                movementTimerX = (movementTimerX / timeToReachFullSpeed) * timeToFullyStop;
                previousX = InputX;
            }

            movementTimerX -= Time.fixedDeltaTime;
            movementTimerX = Mathf.Clamp(movementTimerX, 0f, timeToFullyStop);
            
            curveValue = movementDecayCurve.Evaluate(movementTimerX / timeToFullyStop);

            dir.x = !Mathf.Approximately(curveValue, 0f) ? curveValue * (rb.velocity.x > 0 ? 1 : -1) : 0;
            dir.x = Mathf.Clamp(dir.x, -1f, 1f);

        }

        if (InputY != 0)
        {
            if (previousY != InputY)
            {
                dir.y = 0f;
                movementTimerY = 0f;
                previousY = InputY;
                
            }

            movementTimerY += Time.fixedDeltaTime;
            movementTimerY = Mathf.Clamp(movementTimerY, 0f, timeToReachFullSpeed);

            curveValue = movementCurve.Evaluate(movementTimerY / timeToReachFullSpeed);

            dir.y +=  curveValue * InputY;
            dir.y = Mathf.Clamp(dir.y, -1f, 1f);

        }
        //Come to rest after a decay
        else
        {
            if (previousY != 0)
            {
                movementTimerY = (movementTimerY / timeToReachFullSpeed) * timeToFullyStop;
                previousY = InputY;
                thisWillFixShit = rb.velocity.y > 0;
            }

            movementTimerY -= Time.fixedDeltaTime;
            movementTimerY = Mathf.Clamp(movementTimerY, 0f, timeToFullyStop);
            curveValue =  movementDecayCurve.Evaluate(movementTimerY / timeToFullyStop);

            dir.y = !Mathf.Approximately(curveValue, 0f) ? curveValue * ( thisWillFixShit ? 1 : -1) : 0;
            dir.y = Mathf.Clamp(dir.y, -1f, 1f);

            //make add flags to check whem movement stops and adjust gravity accordingly

        }

        rb.velocity = (Walk(dir) * movementSpeed + ApplyGravity()) ;
        

    }

    private Vector2 ApplyGravity()
    { 
        return( Vector2.down * gravityScale );
    }

    private Vector2 Walk(Vector2 dir)
    {
        dir = Vector2.ClampMagnitude(dir, 1f);
        return(dir);
    }
}
