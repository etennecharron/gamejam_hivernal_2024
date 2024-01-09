using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows.Speech;


public class voicereco : MonoBehaviour
{
    public Light lightSrc;

    private string[] keywords = new string[] { "Jellybean", "Allume la lumière", "Ferme la lumière" };
    private KeywordRecognizer recognizer;
    private bool isListening;
    private Coroutine attentionSpan;
    // Start is called before the first frame update
    void Start()
    {
        recognizer = new KeywordRecognizer(keywords);
        recognizer.OnPhraseRecognized += OnPhraseRecognized;

    }

    // Update is called once per frame
    void Update()
    {
       if (Input.GetKeyDown(KeyCode.V)) {
            recognizer.Start();
        } 
       if (Input.GetKeyUp(KeyCode.V))
        {
            recognizer.Stop();
        }
    }

    private void OnPhraseRecognized(PhraseRecognizedEventArgs args)
    {
        if (args.text == "Jellybean")
        {
            Debug.Log("Jellybean");
            attentionSpan = StartCoroutine(AttentionSpan());
        }

        if (isListening)
        {
            Debug.Log("ecoute");
            switch (args.text)
            {
                case "Allume la lumière":
                    LightOn();
                    break;
                case "Ferme la lumière":
                    LightOff();
                    break;
                    default: break;
            }
        }
    }

    private IEnumerator AttentionSpan()
    {
        isListening = true;
        yield return new WaitForSeconds(5f);
        isListening = false;
    }

    private void LightOn()
    {
        StopCoroutine(attentionSpan);
        isListening = false;
        lightSrc.intensity = 1f;
    }

    private void LightOff()
    {
        StopCoroutine(attentionSpan);
        isListening = false;
        lightSrc.intensity = 0f;
    }
}
