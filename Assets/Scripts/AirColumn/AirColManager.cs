using UnityEngine;

[RequireComponent(typeof(AirColumn))]
public class AirColManager : MonoBehaviour
{

    AirColumn airColumn;
    [SerializeField]private float airTime = 5f;

    float timer;
    bool flip = true;


    void Start()
    {
        timer = 0;
        airColumn = this.GetComponent<AirColumn>();
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= airTime)
        {
            timer = 0;
            airColumn.Flip();
        }
    }
}
