/*
# Copyright (c) 2022 Zhaowen Wang. All rights reserved.
# Licensed under the GPLv3 License.

# Author: Zhaowen Wang
# Date: 2022-08
# Lang: C#
# Description: sound generator for unity
*/

// ref: https://www.youtube.com/watch?v=GqHFGMy_51c
// https://www.youtube.com/watch?v=K4dKsIJnh1c

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oscillator : MonoBehaviour
{
    // public double frequency = 440.0;
    public double gain; // Amplitude?
    
    // private double phase, increment;
    private double sr = 48000.0; // sample rate

    class SinSignal
    {
        public float frequency;
        public float vibrato;
        public float phase, increment;
    };

    Dictionary<int, SinSignal> sin_signals;

    void Awake()
    {
        sin_signals = new Dictionary<int, SinSignal>();
    }

    public void OnNote(int midiNumber)
    {
        SinSignal sin_signal = new SinSignal();
        sin_signal.frequency = 440f * Mathf.Pow(2, ((float)midiNumber-69f)/12f); // caculate the frequency by 12 equal temperament
        sin_signal.vibrato = 0f;
        sin_signals[midiNumber] = sin_signal;
        // Debug.Log(midiNumber + "Signal generated" ); 
    }

    public void OnNoteOff(int midiNumber)
    {
        sin_signals.Remove(midiNumber);
    }

    void OnAudioFilterRead(float[] data, int channels)
    {
        foreach (var key in sin_signals.Keys)
        {
            sin_signals[key].increment = sin_signals[key].frequency * (float)(2.0 * Mathf.PI / sr);
            for (int i = 0; i < data.Length; i += channels)
            {
                sin_signals[key].phase += sin_signals[key].increment;
                data[i] = (float)(gain * Mathf.Sin((float)sin_signals[key].phase));

                if (channels == 2) data[i+1] = data[i];
                if (sin_signals[key].phase > 2 * Mathf.PI) sin_signals[key].phase -= (float)(2.0 * Mathf.PI); // "phase = 0.0" in original version
            }
        }
    }
}
