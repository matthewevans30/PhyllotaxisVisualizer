using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Phyllotaxis : MonoBehaviour
{
    public GameObject Sphere;
    public float Degree, Scale;
    public int N;
    public float sphereSize;

    private Vector2 _phyllotaxisPosition;

    //phyllotaxis formula: 
    //angle = n * degree;
    //radius = scale * sqrt(n) 
    //then convert from polar to cartesian coordinates
    private Vector2 CalculatePhyllotaxis(float degree, float scale, int count)
    {
        double angle = count * (degree * Mathf.Deg2Rad);

        float radius = scale * Mathf.Sqrt(count);

        float x = radius * (float)System.Math.Cos(angle);
        float y = radius * (float)System.Math.Sin(angle);

        Vector2 cartesianPoint = new Vector2(x, y);

        return cartesianPoint;  
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            _phyllotaxisPosition = CalculatePhyllotaxis(Degree, Scale, N);
            GameObject sphereInstance = Instantiate(Sphere);
            sphereInstance.transform.position = new Vector3(_phyllotaxisPosition.x, _phyllotaxisPosition.y, 0);
            sphereInstance.transform.localScale = new Vector3(sphereSize, sphereSize, sphereSize);
            N++;
        }
    }
}
