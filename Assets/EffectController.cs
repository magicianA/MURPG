using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectController : MonoBehaviour
{
    public static EffectController instance;

    public Texture[] textures;
    public Material Beam_Music;
    public ParticleSystem[] perfabs;

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    private void Start()
    {
    }

    // Update is called once per frame
    private void Update()
    {
        //foreach (ParticleSystem gameObject in perfabs)
        //{
        //    if (gameObject.isPlaying)
        //    {
        //        TextureRandom();
        //    }
        //}
    }

    public void TextureRandom()
    {
        Beam_Music.SetTexture("MainTexture", textures[Random.Range(0, textures.Length)]);
    }
}