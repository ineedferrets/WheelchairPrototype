﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenArduino : MonoBehaviour {

    public WheelMovement wheelController;

    public ArduinoConnector arduino;

    public float maxVelocity = 20f;

	// Use this for initialization
	void Start () {
        arduino = this.GetComponent<ArduinoConnector>();
        arduino.Open();
        StartCoroutine(
            arduino.AsynchronousReadFromArduino(
                (string s) => SendVelocityReading(s),
                () => Debug.LogError("Error!"),
                1000f
            )
        );
    }

    private void OnApplicationQuit() {
        arduino.Close();
    }

    private void SendVelocityReading(string s) {
        int velocity = 0;
        velocity = -1 * int.Parse(s);
        wheelController.wheelInput = Mathf.Clamp( velocity / maxVelocity, -1f, 1f);
    }
}
