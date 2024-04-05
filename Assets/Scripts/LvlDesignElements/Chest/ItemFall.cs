using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemFall : MonoBehaviour
{

    public Vector2 startPoint; // Pozycja pocz¹tkowa
    public Vector2 endPoint; // Pozycja koñcowa
    public float maxHeight = 1f; // Maksymalna wysokoœæ lotu
    public float throwDuration = 1f; // Czas trwania lotu

    private float throwTimer = 0f;

    void Update()
    {
        if (throwTimer < throwDuration)
        {
            throwTimer += Time.deltaTime;

            // Obliczanie pozycji na podstawie równania rzutu ukoœnego
            float t = throwTimer / throwDuration;
            Vector2 newPos = CalculateParabolicPosition(startPoint, endPoint, t);

            transform.position = newPos;
        }
    }

    Vector2 CalculateParabolicPosition(Vector2 start, Vector2 end, float t)
    {
        float parabolicHeight = Mathf.Sin(t * Mathf.PI) * maxHeight;
        Vector2 result = Vector2.Lerp(start, end, t);
        result.y += parabolicHeight;
        return result;
    }
}
