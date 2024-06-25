using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour
{
    public Transform door1Transform; // The transform of the door
    public Vector3 door1OpenRotation; // The rotation when the door is open
    public Vector3 door1ClosedRotation; // The rotation when the door is closed

    public Transform door2Transform; // The transform of the door
    public Vector3 door2OpenRotation; // The rotation when the door is open
    public Vector3 door2ClosedRotation; // The rotation when the door is closed

    public float openSpeed = 2f; // Speed at which the door opens
    private bool isOpen = false; // Is the door currently open

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") || other.CompareTag("NPC"))
        {
            StopAllCoroutines();
            StartCoroutine(OpenDoor(door1Transform, door1OpenRotation));
            StartCoroutine(OpenDoor(door2Transform, door2OpenRotation));
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") || other.CompareTag("NPC"))
        {
            StopAllCoroutines();
            StartCoroutine(CloseDoor(door1Transform, door1ClosedRotation));
            StartCoroutine(CloseDoor(door2Transform, door2ClosedRotation));
        }
    }

    private IEnumerator OpenDoor(Transform doorTransform, Vector3 doorOpenRotation)
    {
        Quaternion targetRotation = Quaternion.Euler(doorOpenRotation);
        while (Quaternion.Angle(doorTransform.localRotation, targetRotation) > 0.01f)
        {
            doorTransform.localRotation = Quaternion.RotateTowards(doorTransform.localRotation, targetRotation, openSpeed * Time.deltaTime * 100f);
            yield return null;
        }
        doorTransform.localRotation = targetRotation;
        isOpen = true;
    }

    private IEnumerator CloseDoor(Transform doorTransform, Vector3 doorClosedRotation)
    {
        Quaternion targetRotation = Quaternion.Euler(doorClosedRotation);
        while (Quaternion.Angle(doorTransform.localRotation, targetRotation) > 0.01f)
        {
            doorTransform.localRotation = Quaternion.RotateTowards(doorTransform.localRotation, targetRotation, openSpeed * Time.deltaTime * 100f);
            yield return null;
        }
        doorTransform.localRotation = targetRotation;
        isOpen = false;
    }
}
