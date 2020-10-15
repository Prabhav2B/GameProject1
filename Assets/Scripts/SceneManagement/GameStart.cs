using System.Collections;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
using System.Xml.Linq;

public class GameStart : MonoBehaviour
{
    [SerializeField] PlayerController pc;
    Light2D[] lights;
    float[] lightIntensities;

    [SerializeField]Light2D lightBeam;
    [SerializeField] Text startText;

    [SerializeField]float timeToFinish = 10f;
    float timer = 0;
    float beamInstensity;

    [SerializeField] float timeToFinishLightBeam = 10f;
    float timerBeam = 0;
    bool lockThis = false;

    private void Start()
    {
        pc.enabled = false;
        lights = pc.gameObject.GetComponentsInChildren<Light2D>();
        beamInstensity = lightBeam.intensity;

        lightIntensities = new float[lights.Length];
        int i = 0;
        foreach (var light in lights)
        {
            lightIntensities[i++] = light.intensity;
            light.intensity = 0f;
        }
        
    }

    private void Update()
    {
        if (Input.anyKey && lockThis == false)
        {
            lockThis = true;
            StartCoroutine(nameof(LightOff));
            
        }
    }

    IEnumerator LightOn()
    {
        float lastFrame = Time.time;
        while (true)
        {
            if (timer / timeToFinish >= 1.0f)
            {
                
                break;
            }


            lights[0].intensity = Mathf.Lerp(0f, lightIntensities[0], timer / timeToFinish);
            lights[1].intensity = Mathf.Lerp(0f, lightIntensities[1], timer / timeToFinish);

            timer += Time.time - lastFrame;
            lastFrame = Time.time;
            Mathf.Clamp01(timer);

            yield return new WaitForSeconds(.0167f);

        }
        pc.enabled = true;
        yield return null;
    }

    IEnumerator LightOff()
    {
        float lastFrame = Time.time;
        while (true)
        {
            if ( timerBeam / timeToFinishLightBeam >= 1.0f)
            {
                foreach (var item in lightBeam.GetComponentsInChildren<ParticleSystem>())
                {
                    item.gameObject.SetActive(false);
                }

                StartCoroutine(nameof(LightOn));
                break;
            }


            lightBeam.intensity = Mathf.Lerp(beamInstensity, 1f, timerBeam / timeToFinishLightBeam);
            startText.color = new Color(.85f, .85f, .85f, Mathf.Lerp(1f, 0f, timerBeam / timeToFinishLightBeam));

            timerBeam += Time.time - lastFrame;
            lastFrame = Time.time;

            Debug.Log(timerBeam / timeToFinishLightBeam);

            yield return new WaitForSeconds(.0167f);

        }
        yield return null;
    }
}
