using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PUPtimer : MonoBehaviour
{
    public float doublePTimer = 0f;
    public float doubleSTimer = 0f;
    public float lockedWallTimer = 0f;
    public float reversedControllsTimer = 0f;
    public PUPcontroller pup;

    private void Start()
    {
        pup = FindObjectOfType<PUPcontroller>();
    }

    private void Update() {
        DoublePCountdown();
        DoubleSCountdown();
        lockedWallCountdown();
        reversedControllsCountdown();
    }

    public void DoublePCountdown() {
        if (doublePTimer > 0) {
            doublePTimer -= Time.deltaTime;
        }
        if (doublePTimer < 0) {
            pup.resetDoublePoints();
            doublePTimer = 0;
        }
    }

    public void DoubleSCountdown() {
        if (doubleSTimer > 0) {
            doubleSTimer -= Time.deltaTime;
        }
        if (doubleSTimer < 0) {
            pup.resetDoubleSpeed();
            doubleSTimer = 0;
        }
    }

    public void lockedWallCountdown() {
        if (lockedWallTimer > 0) {
            lockedWallTimer -= Time.deltaTime;
        }
        if (lockedWallTimer < 0) {
            pup.resetLockedWalls();
            lockedWallTimer = 0;
        }
    }

    public void reversedControllsCountdown() {
        if (reversedControllsTimer > 0) {
            reversedControllsTimer -= Time.deltaTime;
        }
        if (reversedControllsTimer < 0) {
            pup.ResetReversedControls();
            reversedControllsTimer = 0;
        }
    }

    public void resetTimers() {
        doublePTimer = 0;
        doubleSTimer = 0;
        lockedWallTimer = 0;
        reversedControllsTimer = 0;
    }
}
