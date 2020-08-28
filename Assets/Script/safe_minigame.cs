using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;


public class safe_minigame : MonoBehaviour
{
    
    [Header("Win Position")]
    [Range(0,4)]
    public List<int> winPos = new List<int>();

    [Header("Deafult Position")]
    [Range(0,4)]
    public List<int> DeafultPos = new List<int>();
    public List<shiftPin> pins = new List<shiftPin>();
    public int stepCount = 5;
    public GameObject safe;
    public GameObject wallColor;
    public float speedFade = 1.5f;
    public AnimationCurve curve;
    public AudioClip Close,Open;
    private List<int> codInGame = new List<int>();
    private bool complite = false;
    private AudioSource audioS;

    public void PressPin(int pinNum){
        //Calculate new position for pin
        codInGame[pinNum]++;
        codInGame[pinNum] %= stepCount;
        pins[pinNum].MovePin(codInGame[pinNum]);
        if(CheckCompl()){
            audioS.clip = Open;
            audioS.Play();
            StartCoroutine(FadeIn(false));
            complite = true;
        }
        //Debug.Log(codInGame[pinNum]);  

    }

    public void OpenGame(){
        //The function opens the game panel on the screen
        // and sets the default values if game not complite
        if(!complite){
            codInGame.Clear();
            gameObject.SetActive(true);

            audioS = gameObject.GetComponent<AudioSource>();
            audioS.clip = Close;
            audioS.Play();

            for(int i=0;i< pins.Count;i++){
                pins[i].SetMyDeafultPosition(DeafultPos[i]);
            }
            for(int i=0;i<DeafultPos.Count;i++){
                codInGame.Add(DeafultPos[i]);
            }
            StartCoroutine(FadeIn(true));
        }
    }

    private bool CheckCompl(){
        //checking the position of pins in victorious states
        bool win = true;
        for(int i=0;i<winPos.Count;i++){
            if(winPos[i] != codInGame[i])
                win = false;
        }
        return win;
    }

    IEnumerator FadeIn (bool hide)
        {//smooth fade
            float t = 1f;
            while (t > 0f)
            {
                t -= Time.deltaTime* speedFade;
                float a = curve.Evaluate(t);
                if(!hide)
                    a = curve.Evaluate(1-t);
                wallColor.GetComponent<Image>().color = new Color(244,255,143,a);
                yield return 0;
            }
            if(!hide){
                gameObject.SetActive(false);
                safe.SetActive(true);
            }
        }
}
