using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shiftPin : MonoBehaviour
{
    // Start is called before the first frame update
    [Range(0,100)]
    public int zeroPos = 92;
    public int step = 44;
    public int lift = 10;
    public float speedAnim;
    private Vector3 wVc;
    private bool move,lift_back;
    private AudioSource audioS;
    void Start(){  

        audioS = gameObject.GetComponent<AudioSource>();
    
    }
    
    private void FixedUpdate() {

        //look for need animate
        if(move)
            Animate();

    }
    public void SetMyDeafultPosition(int pos){
       
       int newPos = zeroPos;
       for(int i = 0;i<pos;i++){
            newPos += step;
        }
        transform.localPosition = new Vector2(transform.localPosition.x,newPos);
    
    }

    public void MovePin(int pos){
        
        // This function is called in the button and calculate new position
        // this is the trigger to push
        audioS.Play();
        wVc = transform.localPosition;
        wVc.y = zeroPos;
        for(int i = 0;i<pos;i++){
            wVc.y += step;
        }
        move = true;

    }

    //Animate pin if player press
    private void Animate(){

        Vector3 wVc_lift = new Vector3(wVc.x,wVc.y+lift,wVc.z);
        if(wVc_lift!=transform.localPosition && !lift_back)

            //Pin deflection by a specified distance
            transform.localPosition = Vector2.MoveTowards(transform.localPosition,wVc_lift,Time.deltaTime*speedAnim);
        
        else{
            
            lift_back = true;
            transform.localPosition = Vector2.MoveTowards(transform.localPosition,wVc,Time.deltaTime*speedAnim);
            if(wVc == transform.localPosition){
                move = false;
                lift_back = false;
            }
        
        }

    }
}
