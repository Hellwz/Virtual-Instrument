using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    public int midiNumber;

    void OnMouseDown()
    {
        Debug.Log("Clicked " + midiNumber); 
        GameObject.Find("SoundObject").GetComponent<Oscillator>().OnNote(midiNumber);
    }

    void OnMouseUp()
    {
        Debug.Log("Released " + midiNumber);
        GameObject.Find("SoundObject").GetComponent<Oscillator>().OnNoteOff(midiNumber);
    }
}
