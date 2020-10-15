using System.Collections;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
using Cinemachine;

public class GameEnd : MonoBehaviour
{
    [SerializeField] PlayerController pc;
    Light2D[] lights;
    float[] lightIntensities;

    [SerializeField] float timeToFinish = 10f;
    float timer = 0;

    [SerializeField] float timeToRest = 10f;
    float restTimer = 0;
    Vector2 restPos;

    [SerializeField] Transform finalPosition;
    [SerializeField] Text endText;

    Rigidbody2D rb;

    private void Start()
    {
  
        lights = pc.gameObject.GetComponentsInChildren<Light2D>();

        lightIntensities = new float[lights.Length];
        int i = 0;
        foreach (var light in lights)
        {
            lightIntensities[i++] = light.intensity;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
       
        rb = pc.GetComponent<Rigidbody2D>();
        restPos = pc.transform.position;
        pc.enabled = false;

        StartCoroutine(nameof(Rest));
        StartCoroutine(nameof(LightOff));
    }

    IEnumerator Rest()
    {
        float lastFrame = Time.time;
        while (true)
        {
            if (restTimer / timeToRest >= 1.0f)
            {
                break;
            }


            rb.MovePosition(Vector2.Lerp(restPos, finalPosition.position, restTimer / timeToRest));

            restTimer += Time.time - lastFrame;
            lastFrame = Time.time;

            yield return new WaitForSeconds(.0167f);

        }
        yield return null;
    }

    IEnumerator LightOff()
    {
        float lastFrame = Time.time;
        while (true)
        {
            if (timer / timeToFinish >= 1.0f)
            {
                break;
            }


            lights[0].intensity = Mathf.Lerp(lightIntensities[0], 0f, timer / timeToFinish);
            lights[1].intensity = Mathf.Lerp(lightIntensities[1], 0f, timer / timeToFinish);
            endText.color = new Color(.85f, .85f, .85f, Mathf.Lerp(0f, 1f, timer / timeToFinish));

            timer += Time.time - lastFrame;
            lastFrame = Time.time;

            yield return new WaitForSeconds(.0167f);

        }
        yield return null;
    }
}
