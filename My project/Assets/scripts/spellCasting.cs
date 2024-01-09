using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.Windows.Speech;
using System.Linq;
using UnityEngine.UIElements;
using System;

public class spellCasting : MonoBehaviour
{

    public GameObject spell;
    public Transform spawn;
    public float fireSpeed = 20;

    private KeywordRecognizer keywordRecognizer;
    private Dictionary<string, Action> actions = new Dictionary<string, Action>();


    // Start is called before the first frame update
    void Start()
    {
        XRGrabInteractable grabbable = GetComponent<XRGrabInteractable>();
        grabbable.activated.AddListener(spellActivation);

        actions.Add("bibabobabibabo", FireBall);


        keywordRecognizer = new KeywordRecognizer(actions.Keys.ToArray());
        keywordRecognizer.OnPhraseRecognized += RecognizedSpeech;
        keywordRecognizer.Start();
    }

    private void RecognizedSpeech(PhraseRecognizedEventArgs speech)
    {
        Debug.Log(speech.text);
        actions[speech.text].Invoke();
    }

    public void spellActivation(ActivateEventArgs arg)
    {
        GameObject spellSpawned = Instantiate(spell);
        spellSpawned.transform.position = spawn.position;
        spellSpawned.GetComponent<Rigidbody>().velocity = spawn.forward * fireSpeed;
        Destroy(spellSpawned, 5);    
    }

    public void FireBall()
    {
        GameObject spellSpawned = Instantiate(spell);
        spellSpawned.transform.position = spawn.position;
        spellSpawned.GetComponent<Rigidbody>().velocity = spawn.forward * fireSpeed;
        Destroy(spellSpawned, 5);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
