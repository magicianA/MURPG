using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.ImageEffects;

public class VignetteController : MonoBehaviour
{
    public VignetteAndChromaticAberration vignette;
    public float frequency;
    public float intensity;
    public float waveRange;
    private float seed;

    private void Awake()
    {
        seed = Random.value;
    }

    // Start is called before the first frame update
    private void Start()
    {
        vignette = GetComponent<VignetteAndChromaticAberration>();
    }

    // Update is called once per frame
    private void Update()
    {
        vignette.intensity = Mathf.PerlinNoise(0, Time.time * frequency) * waveRange + intensity;
    }
}