using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Windows.Speech;
public class VoiceMovement : MonoBehaviour
{
    private KeywordRecognizer keywordRecognizer;
    private Dictionary<string, Action> actions = new Dictionary<string, Action>();

    private void Start()
    {
        actions.Add("avant", Forward);
        actions.Add("haut", Up);
        actions.Add("bas", Down);
        actions.Add("arriere", Back);
        actions.Add("rotation", Rotate);

        keywordRecognizer = new KeywordRecognizer(actions.Keys.ToArray());
        keywordRecognizer.OnPhraseRecognized += RecognizedSpeech;
        keywordRecognizer.Start();
    }

    private void RecognizedSpeech(PhraseRecognizedEventArgs speech)
    {
        Debug.Log(speech.text);
        actions[speech.text].Invoke();
    }

    private void Forward()
    {
        transform.Translate(1,0,0);
    }

private void Back() {
        transform.Translate(-1,0,0);
    }
    private void Up() { 
        transform.Translate(0,1,0);
        
    }

    private void Down() {
    transform.Translate(0,-1,0);
    }

    private void Rotate()
    {
        transform.Rotate(40,30,0);
    }


}
