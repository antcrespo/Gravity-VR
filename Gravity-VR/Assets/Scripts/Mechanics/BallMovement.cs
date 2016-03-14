using UnityEngine;
using System.Collections;

public class BallMovement : MonoBehaviour {
    public float radius = 28.5f;
    private float speed = 0;
    private float elevationAngle;
    private float polarAngle;
    private bool activeAdjustment;
    private int direction;
    private int sign;
    private GameObject ballCam;
    private GameObject mainCam;

	// Use this for initialization
	void Start () {
        activeAdjustment = false;
        ballCam = GameObject.FindGameObjectWithTag("BallCam");
        mainCam = GameObject.FindGameObjectWithTag("Head");
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Slider"))
        {
            Slider s = collision.gameObject.GetComponent<Slider>();
            if (s != null)
            {
                int tsign = s.getSign();
                if (tsign == 0)
                {
                    sign *= -1;
                } else
                {
                    sign = tsign;
                    direction = s.movementDimension;
                    speed = s.angularSpeed;
                }
            }

            playSound();
        } else if (collision.gameObject.CompareTag("Wedge"))
        {
            //Debug.Log("Hit Wedge");
            WedgeBehavior wb = collision.gameObject.GetComponentInParent<WedgeBehavior>();
            if (collision.collider.Equals(wb.bottom))
            {
                sign *= wb.sign;

                direction = direction == 1 ? 0 : 1;
                //Debug.Log("Bottom");
            } else
            {
                sign = -sign;
                //Debug.Log("side");
            }

            playSound();
        }
        else if (collision.gameObject.CompareTag("Barrier"))
        {
            sign = 0;

            playSound();
        }

        Vector3 startPos = gameObject.transform.position;
        SphericalCoordinates sc = new SphericalCoordinates(radius, 0, 0, 1, radius + .3f, -Mathf.PI, Mathf.PI, -Mathf.PI / 2, Mathf.PI / 2);
        sc.FromCartesian(startPos);
        elevationAngle = sc.elevation;
        polarAngle = sc.polar;
        activeAdjustment = true;

    }


    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("LaserShot"))
        {
            collision.gameObject.GetComponent<AudioSource>().Play();
            //Debug.Log("Hit Shot");
            GameObject levelInfo = GameObject.FindGameObjectsWithTag("Info")[0];
            levelInfo.GetComponent<LevelInfo>().Restart();
        }
    }


    void Update()
    {
        if (activeAdjustment)
        {
            Vector3 currentPos = gameObject.transform.position;

            SphericalCoordinates sc = new SphericalCoordinates(radius, 0, 0, 1, radius + .3f, -Mathf.PI, Mathf.PI, -Mathf.PI / 2, Mathf.PI / 2);
            sc.loopPolar = true;
            sc.FromCartesian(currentPos);
            //sc.SetRadius(radius);
            float change = sign * speed * Time.deltaTime;
            if (direction == 0)
            {
                sc.RotatePolarAngle(change);
                gameObject.transform.position = sc.toCartesian;
            } else
            {
                sc.RotateElevationAngle(change);
                gameObject.transform.position = sc.toCartesian;
            }
        }
        if (Input.GetButton("R1"))
        {
            mainCam.SetActive(false);
            ballCam.SetActive(true);
        }
        else
        {
            mainCam.SetActive(true);
            ballCam.SetActive(false);
        }
    }

    void playSound()
    {
        AudioSource source = GetComponent<AudioSource>();
        source.Play();
    }
}
