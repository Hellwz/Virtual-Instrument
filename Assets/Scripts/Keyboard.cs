using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Keyboard : MonoBehaviour
{
    public int base_midi_number = 60;

    void Awake()
    {
        for (int i=0; i<12; i++)
        {
            this.transform.GetChild(i).GetComponent<Key>().midiNumber = base_midi_number + i;
        } 
    }

}
