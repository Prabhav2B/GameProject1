using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class DrillLightsEvent : TriggeredEvent
{

    [SerializeField]Light2D[] lightsMain;
    [SerializeField] Light2D[] lightsHalo;
    [SerializeField]float lightsIntensity = 1.9f;
    [SerializeField]float haloIntensity = 0.2f;

    [SerializeField]float timeToFinish = 0.5f;

    [SerializeField] AudioSource au;

    float timer;

    private void Start()
    {
        timer = 0;
        foreach (var light in lightsMain)
        {
            light.intensity = 0f;
        }

        foreach (var light in lightsHalo)
        {
            light.intensity = 0f;
        }
    }

    public override void OnEvent()
    {
        //turn lights on
        timer = 0;
        au.loop = true;
        au.Play();
        StartCoroutine(nameof(LightOn));

    }

    public override void OnEventExit()
    {
        //turn lights off
        au.Stop();
        StartCoroutine(nameof(LightOff));
    }

   IEnumerator LightOn()
   {
        while (true)
        {
            if (timer / timeToFinish >= 1.0f)
            {
                break;
            }

            foreach (var light in lightsMain)
            {
                light.intensity = Mathf.Lerp(0f, lightsIntensity, timer / timeToFinish);
            }

            foreach (var light in lightsHalo)
            {
                light.intensity = Mathf.Lerp(0f, haloIntensity, timer / timeToFinish);
            }

            timer += Time.deltaTime;
            Mathf.Clamp01(timer);

            yield return new WaitForEndOfFrame();

        }

        yield return null;
   }

    IEnumerator LightOff()
    {
        while (true)
        {
            if (timer / timeToFinish <= 0.0f)
            {
                break;
            }

            foreach (var light in lightsMain)
            {
                light.intensity = Mathf.Lerp(0f, lightsIntensity, timer / timeToFinish);
            }

            foreach (var light in lightsHalo)
            {
                light.intensity = Mathf.Lerp(0f, haloIntensity, timer / timeToFinish);
            }

            timer -= Time.deltaTime;

            yield return new WaitForEndOfFrame();

        }

        yield return null;
    }
}
