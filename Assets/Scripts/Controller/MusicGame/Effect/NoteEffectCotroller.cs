using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteEffectCotroller : MonoBehaviour
{
    public GameObject[] effects;
    public NoteEffectCotroller instance;
    void Awake(){
        instance = this;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void effect(Vector3 pos,int id = -1){
        if(id < 0 || id > effects.Length){
            id = Random.Range(0,effects.Length);
        }
        GameObject t = Instantiate(effects[id],pos,Quaternion.identity);
        t.GetComponent<ParticleSystem>().Play();
        StartCoroutine(delete(t));
    }
    IEnumerator delete(GameObject x){
        yield return new WaitForSeconds(5f);
        Destroy(x);
    }
}
