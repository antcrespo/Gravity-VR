using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LevelInfo : MonoBehaviour {

    public int level;
    public int nextLevel;
	// Use this for initialization
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
