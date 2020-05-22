using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class line : MonoBehaviour
{
    public static line _instance;
    public float notespeed = 70f;
    public GameObject player;
    public bool onplayermove = false;
    #region HOLDNOTE
    private float[] holdtime = new float[10];
    private bool[] onhold = new bool[10];
    private GameObject[] holdnotes = new GameObject[10];
    #endregion

    #region FLICKNOTE
    //临时方案，移动设备上需要更改
    private float fingerActionSensitivity;
    private float fingerBeginX = 0,fingerBeginY = 0;
    private float fingerCurrentX = 0,fingerCurrentY = 0;
    private float fingerSegmentX = 0,fingerSegmentY = 0;
    private int fingerTouchState = 0;
    private int FINGER_STATE_NULL = 0,FINGER_STATE_TOUCH = 1,FINGER_STATE_ADD = 2;
    private NoteResultType flickres;
    #endregion 
    
    private Ray ray;
    private RaycastHit hit;
    private GameObject obj;
    private Vector3 hitpoint;    

    private GameObject curnote;
    private KeyCode curkey;

    public GameObject noteeffectcontroller;


    void Awake(){
        _instance = this;
    }
    void Start()
    {
        Input.multiTouchEnabled = true;
        Input.simulateMouseWithTouches = true;
        fingerActionSensitivity = Screen.width * 0.05f;
    }
    void Update()
    {   
        for(int i = 0;i < 1;i++){
            if(Input.GetMouseButtonDown(0)){
                ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if(Physics.Raycast(ray, out hit,9999f,(1 << LayerMask.NameToLayer("Line")))){
                    obj = hit.collider.gameObject;
                    hitpoint = hit.point;
                    if(obj.name == "Line"){
                        if(Mathf.Abs(player.transform.position.x - hitpoint.x) < 3){
                            onplayermove = true;
                            player.transform.position = new Vector3(hitpoint.x,player.transform.position.y,player.transform.position.z);
                        }
                    }
                }
                else{
                    onplayermove = false;
                    ray = new Ray(player.transform.position,Vector3.forward);
                    if(Physics.Raycast(ray,out hit)){
                        hitpoint = hit.point;
                        obj = hit.collider.gameObject;
                        if(hit.transform.tag == "ClickNote"){
                            NoteResultType res  = CheckClickResult(hitpoint.z);
                            Debug.Log(res);
                            if(res != NoteResultType.Unknown){
                                Destroy(hit.collider.gameObject);    
                                noteeffectcontroller.GetComponent<NoteEffectCotroller>().instance.effect(obj.transform.position);
                            }
                        }
                        if(hit.transform.tag == "FlickNote"){
                            flickres = CheckClickResult(hitpoint.z);
                            if(flickres != NoteResultType.Unknown)
                                curnote = hit.transform.gameObject;
                            Debug.Log(flickres);
                            if(fingerTouchState == FINGER_STATE_NULL){
                                fingerTouchState = FINGER_STATE_TOUCH;
                                fingerBeginX = Input.mousePosition.x;
                                fingerBeginY = Input.mousePosition.y;
                            }
                        }
                    }
                }
            }        
            if(Input.GetMouseButton(0)){
                if(onplayermove){
                    ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                    if(Physics.Raycast(ray, out hit,9999f,(1 << LayerMask.NameToLayer("Line")))){
                        //Debug.Log(hit.collider.gameObject.name);
                        obj = hit.collider.gameObject;
                        hitpoint = hit.point;
                        if(obj.name == "Line"){
                            if(Mathf.Abs(player.transform.position.x - hitpoint.x) < 10){
                                player.transform.position = new Vector3(hitpoint.x,player.transform.position.y,player.transform.position.z);
                            }
                            else{
                                onplayermove = false;
                            }
                        }
                        else{
                            onplayermove = false;
                        }
                    }
                    else{
                        onplayermove = false;
                    }
                }
                if(fingerTouchState == FINGER_STATE_TOUCH){
                    fingerCurrentX = Input.mousePosition.x;
                    fingerCurrentY = Input.mousePosition.y;
                    fingerSegmentX = fingerCurrentX - fingerBeginX;
                    fingerSegmentY = fingerCurrentY - fingerBeginY;
                    float fingerDistance = fingerSegmentX*fingerSegmentX + fingerSegmentY*fingerSegmentY; 
                    if(fingerDistance > (fingerActionSensitivity*fingerActionSensitivity)){
                        Debug.Log(flickres);
                        fingerTouchState = FINGER_STATE_ADD;
                        if (Mathf.Abs (fingerSegmentX) > Mathf.Abs (fingerSegmentY)) {
                            fingerSegmentY = 0;
                        } 
                        else {
                            fingerSegmentX = 0;
                        }
                        if(fingerSegmentX == 0) {
                            if (fingerSegmentY > 0) {
                                Debug.Log ("up");
                                if(curnote && curnote.GetComponent<FlickNoteAct>().flickdir == 2){
                                    Destroy(curnote);
                                    noteeffectcontroller.GetComponent<NoteEffectCotroller>().instance.effect(obj.transform.position);
                                }
                            }
                            else {
                                Debug.Log ("down");
                                if(curnote && curnote.GetComponent<FlickNoteAct>().flickdir == 4){
                                    Destroy(curnote);
                                    noteeffectcontroller.GetComponent<NoteEffectCotroller>().instance.effect(obj.transform.position);
                                }
                            }
                        } 
                        else if(fingerSegmentY == 0) {
                            if(fingerSegmentX > 0){
                                Debug.Log ("right");
                                if(curnote && curnote.GetComponent<FlickNoteAct>().flickdir == 1){
                                    Destroy(curnote);
                                    noteeffectcontroller.GetComponent<NoteEffectCotroller>().instance.effect(obj.transform.position);
                                }
                            }
                            else{
                                Debug.Log("left");
                                if(curnote && curnote.GetComponent<FlickNoteAct>().flickdir == 3){
                                    Destroy(curnote);
                                    noteeffectcontroller.GetComponent<NoteEffectCotroller>().instance.effect(obj.transform.position);
                                }
                            }
                        }
                    }
                }
            }
            if(Input.GetMouseButtonUp(0)){
                fingerTouchState = FINGER_STATE_NULL;
                onplayermove = false;            
            }
            if(Input.GetKeyDown(KeyCode.Space)){
                ray = new Ray(player.transform.position,Vector3.forward);
                if(Physics.Raycast(ray,out hit)){
                    hitpoint = hit.point;
                    obj = hit.collider.gameObject;
                    if(hit.transform.tag == "ClickNote"){
                        NoteResultType res  = CheckClickResult(hitpoint.z);
                        Debug.Log(res);
                        if(res != NoteResultType.Unknown){
                            Destroy(hit.collider.gameObject);    
                            noteeffectcontroller.GetComponent<NoteEffectCotroller>().instance.effect(obj.transform.position);
                        }
                    }
                }
            }
            if(Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.D)){
                if(Input.GetKeyDown(KeyCode.W)) curkey = KeyCode.W;
                if(Input.GetKeyDown(KeyCode.A)) curkey = KeyCode.A;
                if(Input.GetKeyDown(KeyCode.S)) curkey = KeyCode.S;
                if(Input.GetKeyDown(KeyCode.D)) curkey = KeyCode.D;
                ray = new Ray(player.transform.position,Vector3.forward);
                if(Physics.Raycast(ray,out hit)){
                    hitpoint = hit.point;
                    obj = hit.collider.gameObject;
                    if(hit.transform.tag == "FlickNote"){
                        flickres = CheckClickResult(hitpoint.z);
                        if(flickres != NoteResultType.Unknown){
                            curnote = obj;
                        }
                        Debug.Log(flickres);
                        switch(curkey){
                            case KeyCode.D:
                                if(curnote && curnote.GetComponent<FlickNoteAct>().flickdir == 1){
                                    Destroy(curnote);
                                    noteeffectcontroller.GetComponent<NoteEffectCotroller>().instance.effect(obj.transform.position);
                                }
                                break;
                            case KeyCode.W:
                                if(curnote && curnote.GetComponent<FlickNoteAct>().flickdir == 2){
                                    Destroy(curnote);
                                    noteeffectcontroller.GetComponent<NoteEffectCotroller>().instance.effect(obj.transform.position);
                                }
                                break;
                            
                            case KeyCode.A:
                                if(curnote && curnote.GetComponent<FlickNoteAct>().flickdir == 3){
                                    Destroy(curnote);
                                    noteeffectcontroller.GetComponent<NoteEffectCotroller>().instance.effect(obj.transform.position);
                                }
                                break;
                            case KeyCode.S:
                                if(curnote && curnote.GetComponent<FlickNoteAct>().flickdir == 4){
                                    Destroy(curnote);
                                    noteeffectcontroller.GetComponent<NoteEffectCotroller>().instance.effect(obj.transform.position);
                                }
                                break;
                        }
                    }
                }
            }
        }
    }
    private NoteResultType CheckClickResult(float d){
        d /= notespeed;
        if(d < 0.1f)
            return NoteResultType.Perfect;
        if(d < 0.2f)
            return NoteResultType.Good;
        if(d < 0.25f)
            return NoteResultType.Bad;
        return NoteResultType.Unknown;
    }
    public static void CheckEnergy(float d){
        float dis = Mathf.Abs(d - _instance.player.transform.position.x);
        Debug.Log(dis);
        if(dis < 1f){
            Energy.addenergy(-10f);
        }
        if(dis < 1f && dis < 10f){
            Energy.addenergy(0.7f * (10-dis));
        }
    }
}
