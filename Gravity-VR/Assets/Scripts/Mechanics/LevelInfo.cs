using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LevelInfo : MonoBehaviour {

    public int level;
    public int nextLevel;
    public float restartTimer = 3f;
    private float wait = 0;

    void Update()
    {
        if (Input.GetButton("Options"))
        {
            wait += Time.deltaTime;
            if (wait >= restartTimer)
                Restart();
        } else
        {
            wait = 0f;
        }
    }
	public void LoadNext()
    {
        Debug.Log("Loading next");
        SceneManager.LoadScene(nextLevel);
    }
	
    private void Restart()
    {
        SceneManager.LoadScene(level);
    }
}
