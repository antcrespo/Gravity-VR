using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LevelInfo : MonoBehaviour {

    public int level;
    public int nextLevel;
    public float restartTimer = 3f;
    private float wait = 0;
    private int toLoad;

    private bool loading = false;
    void Update()
    {
        
        if (Input.GetButton("Options"))
        {
            wait += Time.deltaTime;
            if (wait >= restartTimer)
                SceneManager.LoadSceneAsync(level);
        } 
        else if (loading)
        {
            wait += Time.deltaTime;
            if (wait >= .5)
            {
                SceneManager.LoadSceneAsync(toLoad);
            }
        }
        else
        {
            wait = 0f;
        }
        /*AudioSource source = GetComponent<AudioSource>();
        if (Input.GetButtonDown("Triangle")) //is L1
        {

        }
        else if (Input.GetButtonDown("Share")) //DNE
        {
            Debug.Log("Share");
        }
        else if (Input.GetButtonDown("Circle")) // is triangle
        {
            Debug.Log("Circle");
        }
        else if (Input.GetButtonDown("R1")) // is R2
        {
            Debug.Log("R1");
        }
        else if (Input.GetButtonDown("L1")) // is L2
        {
            Debug.Log("L1"); 
        }
        else if (Input.GetButtonDown("L2")) //is share
        {
            Debug.Log("L2");
        }
        else if (Input.GetButtonDown("R2")) //is options
        {
            Debug.Log("R2");
        }
        else if (Input.GetButtonDown("PS")) //itself
        {
            Debug.Log("PS");
        }
        else if (Input.GetButtonDown("PadPress")) //is circle
        {
            Debug.Log("PadPress");
            source.Play();
        }
        else if (Input.GetButtonDown("L3"))
        {
            Debug.Log("L3");
        }
        else if (Input.GetButtonDown("R3"))
        {
            Debug.Log("R3");
        }*/
    }
	public void LoadNext()
    {
        //Debug.Log("Loading next");
        //SceneManager.LoadSceneAsync(nextLevel);
        toLoad = nextLevel;
        loading = true;
    }
	
    public void Restart()
    {
        //SceneManager.LoadSceneAsync(level);
        toLoad = level;
        loading = true;
    }
}
