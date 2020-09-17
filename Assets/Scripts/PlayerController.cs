using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{

    #region Variables

    [Space(15)]

    //Visual Representation of player Velocity
    public AnimationCurve movementCurve = new AnimationCurve();
    [Space(5)]
    public AnimationCurve movementDecayCurve = new AnimationCurve();

    [Space(15)]


    [Range(.1f, 2f)]
    public float movementSpeed = 10;

    [Range(0.0f, 5.0f)]
    public float timeToReachFullSpeed = .5f;

    [Range(0.0f, 5.0f)]
    public float timeToFullyStop = .5f;



    Rigidbody2D rb;
    float dirX;
    float dirY;
    float movementTimerX;
    float movementTimerY;

    float previousX;
    float previousY;
    #endregion

    public float InputX { get; private set; }
    public float InputY { get; private set; }



    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        dirX = 0f;
        dirY = 0f;
        movementTimerX = 0f;
        movementTimerY = 0f;
        previousX = 0f;
        previousY = 0f;
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

            dirX = curveValue * InputX;


        }
        //Come to rest after a decay
        else
        {
            previousX = InputX;

            movementTimerX -= Time.fixedDeltaTime;
            movementTimerX = Mathf.Clamp(movementTimerX, 0f, timeToFullyStop);
            curveValue = movementDecayCurve.Evaluate(movementTimerX / timeToFullyStop);

            dirX = 
                rb.velocity.x != 0 ? curveValue * (rb.velocity.x / Mathf.Abs(rb.velocity.x)) : InputX;

        }

        if (InputY != 0)
        {

            movementTimerY += Time.fixedDeltaTime;
            movementTimerY = Mathf.Clamp(movementTimerY, 0f, timeToReachFullSpeed);

            curveValue = movementCurve.Evaluate(movementTimerY / timeToReachFullSpeed);

            dirY =  curveValue * InputY;

        }
        //Come to rest after a decay
        else
        {

            movementTimerY -= Time.fixedDeltaTime;
            movementTimerY = Mathf.Clamp(movementTimerY, 0f, timeToFullyStop);
            curveValue = movementDecayCurve.Evaluate(movementTimerY / timeToFullyStop);

            dirY =
                rb.velocity.y != 0 ? curveValue * (rb.velocity.y / Mathf.Abs(rb.velocity.y)) : InputY;

        }

        Walk(new Vector2(dirX, dirY));

    }

    private void Walk(Vector2 dir)
    {
        Vector3.ClampMagnitude(dir, 1f);
        // Vector3.Normalize(dir);
        rb.velocity = new Vector2(dir.x * movementSpeed, dir.y * movementSpeed);
    }
}
