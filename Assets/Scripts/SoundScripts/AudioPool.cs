using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPool : MonoBehaviour
{
    [SerializeField] private int poolSize = 10;
    [SerializeField] GameObject AudioSourcePrefab;


    
    void Start()
    {
        if (this.transform.childCount != 0)
        {
            foreach (AudioSource tr in this.transform.GetComponentsInChildren<AudioSource>())
            {
                Destroy(tr.gameObject);
            }
        }

        for (int i = 0; i < poolSize; i++)
        {
            Instantiate(AudioSourcePrefab, this.transform);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
