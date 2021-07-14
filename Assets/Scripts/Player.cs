using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Player : MonoBehaviour {

    [Tooltip("In ms^-1")] [SerializeField] float Speed = 10f;
    [SerializeField] float positionPithFactor = -5f;
    [SerializeField] float controlPithFactor = -20f;
    [SerializeField] float positionYawFactor = 5f;
    [SerializeField] float controlRollFactor = -20f;
    [SerializeField] GameObject[] guns;

    float xThrow, yThrow;
    bool isControlEnabled = true;

    // Update is called once per frame
    void Update ()
    {
        if (isControlEnabled)
        {
            Translation();
            Rotation();
            Firing();
        }
    }

    private void PlayerOnDeath()
    {
        isControlEnabled = false;
    }

    private void Translation()
    {
        xThrow = CrossPlatformInputManager.GetAxis("Horizontal");
        float xoffset = xThrow * Speed * Time.deltaTime;
        float rawNewX = transform.localPosition.x + xoffset;
        float clampedX = Mathf.Clamp(rawNewX, -5.4f, 5.4f);

        yThrow = CrossPlatformInputManager.GetAxis("Vertical");
        float yoffset = yThrow * Speed * Time.deltaTime;
        float rawNewY = transform.localPosition.y + yoffset;
        float clampedY = Mathf.Clamp(rawNewY, -3f, 2.7f);

        transform.localPosition = new Vector3(clampedX, clampedY, transform.localPosition.z);
    }

    private void Rotation()
    {
        float pitchDueRotation = transform.localPosition.y * positionPithFactor;
        float pitchDueControlThrow = yThrow * controlPithFactor;
        float pitch = pitchDueRotation + pitchDueControlThrow;

        float yaw = transform.localPosition.x * positionYawFactor;

        float roll = xThrow * controlRollFactor;

        transform.localRotation = Quaternion.Euler(pitch, yaw, roll);
    }

    void Firing()
    {
        if (CrossPlatformInputManager.GetButton("Fire"))
            SetGunsActive(true);
        else
            SetGunsActive(false);
    }

    private void SetGunsActive(bool isActive)
    {
        foreach (GameObject gun in guns)
        {
            var emmisionModule = gun.GetComponent<ParticleSystem>().emission;
            emmisionModule.enabled = isActive;
        }
    }

}
