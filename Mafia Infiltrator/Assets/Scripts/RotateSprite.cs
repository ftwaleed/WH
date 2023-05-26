using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateSprite : MonoBehaviour
{
    [SerializeField] private float startAngle = -5.4f;
    [SerializeField] private float endAngle = 170f;
    [SerializeField] private float rotationTime = 2f;
    [SerializeField] private float waitTime = 5f;

    private bool isRotating = false;

    private IEnumerator RotateCoroutine(float startAngle, float endAngle, float rotationTime)
    {
        isRotating = true;

        Quaternion startRotation = transform.rotation;
        Quaternion endRotation = Quaternion.Euler(startRotation.eulerAngles.x, endAngle, startRotation.eulerAngles.z);

        float elapsedTime = 0f;

        while (elapsedTime < rotationTime)
        {
            transform.rotation = Quaternion.Slerp(startRotation, endRotation, (elapsedTime / rotationTime));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.rotation = endRotation;

        yield return new WaitForSeconds(waitTime);

        Quaternion returnRotation = Quaternion.Euler(startRotation.eulerAngles.x, startAngle, startRotation.eulerAngles.z);

        elapsedTime = 0f;

        while (elapsedTime < rotationTime)
        {
            transform.rotation = Quaternion.Slerp(endRotation, returnRotation, (elapsedTime / rotationTime));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.rotation = returnRotation;

        yield return new WaitForSeconds(waitTime);

        isRotating = false;
    }

    private void Update()
    {
        if (!isRotating)
        {
            StartCoroutine(RotateCoroutine(startAngle, endAngle, rotationTime));
        }
    }
}
