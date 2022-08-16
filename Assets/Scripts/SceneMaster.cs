using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneMaster : MonoBehaviour
{
    public int currentScene = 0;
    bool sceneSwitching = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    void Update()
    {
        /*
        if (sceneSwitching)
        {
            GameObject[] enemies = GameObject.FindGameObjectsWithTag("NPC");
            int deadEnemies = 0;

            foreach (GameObject enemy in enemies)
            {
                if (enemy.GetComponent<NPCMove>().dead)
                {
                    deadEnemies++;
                }
            }

            if (enemies.Length <= deadEnemies)
            {
                SwitchScenes();
            }
        }
        
        if (currentScene > 0)
        {
            sceneSwitching = true;
        }
        else
        {
            sceneSwitching = false;
        }
        */
    }
    
    void Awake()
    {
        DontDestroyOnLoad(this);
    }

    public void SwitchScenes()
    {
        currentScene++;
        if (currentScene > 3)
        {
            BackToMainMenu();
        }
        SceneManager.LoadScene(currentScene);
    }

    public void BackToMainMenu()
    {
        currentScene = 0;
        SceneManager.LoadScene(0);
        Destroy(this.gameObject);
    }

    public void SceneOne()
    {
        currentScene = 1;
        SceneManager.LoadScene(currentScene);
    }

    public void SceneTwo()
    {
        currentScene = 2;
        SceneManager.LoadScene(currentScene);
    }

    public void SceneThree()
    {
        currentScene = 3;
        SceneManager.LoadScene(currentScene);
    }
}
