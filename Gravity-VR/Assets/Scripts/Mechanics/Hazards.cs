using UnityEngine;
using System.Collections;
using VolumetricLines;

public class Hazards : MonoBehaviour {
    public GameObject shotPrefab;
    public float shotSpeed = 6f;
    public float shotFreqeuncy = 1f;
    public float shotLife = 2f;

    public int movementDimension = 0;
    public float maxPolar;
    public float minPolar;
    public float maxElevation;
    public float minElevation;
    public float angularSpeed;
    public float radius;

    private float copy;
    private SphericalCoordinates curPos;
    private int sign;

    // Use this for initialization
    void Start () {
        copy = shotFreqeuncy;
        minPolar = minPolar *Mathf.Deg2Rad;
        maxPolar = maxPolar *Mathf.Deg2Rad;
        minElevation = minElevation * Mathf.Deg2Rad;
        maxElevation *= Mathf.Deg2Rad;

        curPos = new SphericalCoordinates(radius, 0, 0, 1, radius, minPolar, maxPolar, minElevation, maxElevation);
        sign = 1;
    }

    // Update is called once per frame
    void Update () {
        shotFreqeuncy -= Time.deltaTime;
        if (shotFreqeuncy <= 0)
        {
            GameObject shot = Instantiate(shotPrefab, gameObject.transform.position, Quaternion.identity) as GameObject;
            LaserBehaivor b = shot.GetComponent<LaserBehaivor>();
            b.direction = movementDimension;
            b.sign = sign;
            b.radius = radius - .5f;
            b.angularSpeed = shotSpeed;
            b.lifetime = shotLife;

            VolumetricLineBehavior shotLine = shot.GetComponent<VolumetricLineBehavior>();
            if (movementDimension == 0)
                shotLine.EndPos = Vector3.right;
            else
                shotLine.EndPos = Vector3.up;

            shotFreqeuncy = copy;
        }

        if (movementDimension != 2)
            ProcessMovement();
    }

    private void ProcessMovement()
    {
        curPos.FromCartesian(gameObject.transform.position);

        if (movementDimension == 0)
        {
            if (curPos.polar >= maxPolar)
            {
                sign = -1;
            }
            else if (curPos.polar <= minPolar)
            {
                sign = 1;
            }

            float radians = angularSpeed * Time.deltaTime * sign;
            curPos.RotatePolarAngle(radians);
        }
        else if (movementDimension == 1)
        {
            if (curPos.elevation >= maxElevation)
            {
                sign = -1;
            }
            else if (curPos.elevation <= minElevation)
            {
                sign = 1;
            }

            float radians = angularSpeed * Time.deltaTime * sign;
            curPos.RotateElevationAngle(radians);
        }

        transform.position = curPos.toCartesian;
    }


}
