using UnityEngine;

/// <summary>
/// Parallax scrolling script that should be assigned to a layer
/// </summary>
public class ParallaxScrolling : MonoBehaviour
{
    Camera mainCam;
    Vector2 lastCamPos;
    private void Start()
    {
        mainCam = Camera.main;
        lastCamPos = mainCam.transform.position;
    }

    /// <summary>
    /// Scrolling speed
    /// </summary>
    public Vector2 speed = new Vector2(2, 2);

    /// <summary>
    /// Moving direction
    /// </summary>
    public Vector2 direction = new Vector2(-1, 0);

    /// <summary>
    /// Movement should be applied to camera
    /// </summary>
    public bool isLinkedToCamera = false;

    void Update()
    {

        Vector2 deltaCamPos = (Vector2)mainCam.transform.position - lastCamPos;

        // Movement
        Vector3 movement = new Vector3(
          speed.x/20f * direction.x * deltaCamPos.x,
          speed.y/20f * direction.y * deltaCamPos.y,
          0);

        //movement *= Time.deltaTime;
        transform.Translate(movement);

        lastCamPos = mainCam.transform.position;

        // Move the camera
        if (isLinkedToCamera)
        {
            Camera.main.transform.Translate(movement);
        }
    }
}