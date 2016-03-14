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
