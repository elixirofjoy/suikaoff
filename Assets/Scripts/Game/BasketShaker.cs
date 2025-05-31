using UnityEngine;
using System.Collections;
public class BasketShaker : MonoBehaviour
{
    public float shakeDuration = 0.5f;      // Длительность тряски
    public float shakeAngle = 10f;          // Максимальный угол отклонения
    public float shakeSpeed = 20f;          // Скорость колебаний
    public float restoreSpeed = 5f;         // Скорость возвращения в исходное положение

    private Rigidbody2D rb;
    private float originalZRotation;
    private bool isShaking = false;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.interpolation = RigidbodyInterpolation2D.Interpolate;
        rb.bodyType = RigidbodyType2D.Kinematic;
    }

    public void ShakeBasket()
    {
        if (!isShaking && GameManager.instance.CurrentScore > 199)
        {
            StartCoroutine(ShakeRoutine());
            GameManager.instance.DecreaseScore(200);
        }
    }

    private IEnumerator ShakeRoutine()
    {
        isShaking = true;
        originalZRotation = rb.rotation;
        float timer = 0f;

        float rampDuration = 0.2f;

        while (timer < shakeDuration)
        {
            float ramp = Mathf.Clamp01(timer / rampDuration); // мягкий старт
            float zAngle = Mathf.Sin(Time.time * shakeSpeed) * shakeAngle * ramp;
            rb.MoveRotation(zAngle);
            timer += Time.unscaledDeltaTime;
            yield return null;
        }

        // Плавный возврат
        float restoreTimer = 0f;
        float currentZ = rb.rotation;

        while (Mathf.Abs(currentZ - originalZRotation) > 0.1f)
        {
            currentZ = Mathf.Lerp(currentZ, originalZRotation, restoreTimer * restoreSpeed * Time.unscaledDeltaTime);
            rb.MoveRotation(currentZ);
            restoreTimer += Time.unscaledDeltaTime;
            yield return null;
        }

        rb.MoveRotation(originalZRotation);
        isShaking = false;
    }

}
