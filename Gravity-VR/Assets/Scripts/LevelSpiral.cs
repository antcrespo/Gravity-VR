using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;


public class LevelSpiral : MonoBehaviour {
    public int levelInt;

    private Spiral spiralComp;
        
	// Use this for initialization
	void Start () {
        spiralComp = gameObject.GetComponent<Spiral>();

    }

    // Update is called once per frame
    void Update () {
        if (spiralComp.isSelected && Input.GetButtonDown("X"))
        {
            Debug.Log("X selected");
           SceneManager.LoadScene(levelInt);
        }
	}

    public void SelectLevel()
    {
        SceneManager.LoadScene(levelInt);
    }
}
