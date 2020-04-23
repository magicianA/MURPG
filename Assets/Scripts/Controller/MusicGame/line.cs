using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class line : MonoBehaviour
{
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


    public GameObject noteeffectcontroller;


    void Start()
    {
        Input.multiTouchEnabled = true;
        Input.simulateMouseWithTouches = true;
        fingerActionSensitivity = Screen.width * 0.05f;
    }
    void Update()
    {   
        //Touch[] touches = Input.touches;
        for(int i = 0;i < 1;i++){
            if(Input.GetMouseButtonDown(0)){
            //if(touches[i].phase == TouchPhase.Began){
                ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if(Physics.Raycast(ray, out hit)){
                    //Debug.Log(hit.collider.gameObject.name);
                    obj = hit.collider.gameObject;
                    hitpoint = hit.point;
                    if(obj.name == "Line"){
                        onplayermove = true;
                        player.transform.position = new Vector3(hitpoint.x,player.transform.position.y,player.transform.position.z);
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
                        if(hit.transform.tag == "HoldNote"){
                            onhold[0] = true;
                            holdtime[0] = obj.GetComponent<HoldNoteAct>().endtime;
                            holdnotes[0] = obj;
                            NoteResultType res  = CheckClickResult(hitpoint.z);
                        }
                        if(hit.transform.tag == "FlickNote"){
                            flickres = CheckClickResult(hitpoint.z);
                            //Debug.Log(flickres);
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
                    if(Physics.Raycast(ray, out hit)){
                        //Debug.Log(hit.collider.gameObject.name);
                        obj = hit.collider.gameObject;
                        hitpoint = hit.point;
                        if(obj.name == "Line"){
                            onplayermove = true;
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
                if(onhold[0] && holdtime[0]>= 0){
                    holdtime[0] -= Time.deltaTime;
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
                            }
                            else {
                                Debug.Log ("down");
                            }
                        } 
                        else if(fingerSegmentY == 0) {
                            if(fingerSegmentX > 0){
                                Debug.Log ("right");
                            }
                            else{
                                Debug.Log("left");
                            }
                        }
                    }
                }
            }
            if(Input.GetMouseButtonUp(0)){
                if(onhold[0]){
                    if(holdtime[0] <= 0){
                        if(obj != null){
                            Debug.Log(NoteResultType.Perfect);
                        }
                    }
                    else{
                        Debug.Log(NoteResultType.Miss);
                    }
                    onhold[0] = false;
                }
                fingerTouchState = FINGER_STATE_NULL;
            }
        }
    }
    NoteResultType CheckClickResult(float d){
        d /= notespeed;
        if(d < 0.1f)
            return NoteResultType.Perfect;
        if(d < 0.2f)
            return NoteResultType.Good;
        if(d < 0.4f)
            return NoteResultType.Bad;
        return NoteResultType.Unknown;
    }
}
