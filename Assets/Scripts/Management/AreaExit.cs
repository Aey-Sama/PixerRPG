using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AreaExit : MonoBehaviour
{
    [SerializeField] private string sceneToLoad;
    [SerializeField] private string sceneTransitionName;
    public bool canEnterDungeon = false;

    public GameObject DungeonMsg; 
    public Transform Dungeonlocation; 
    
    public Vector3 offset = new Vector3(0, 2f, 0);

    private float waitToLoadTime = 1f;
    private bool canTrigger = false;

    

    private void Start()
    {
        StartCoroutine(EnableTriggerAfterDelay());
        if (DungeonMsg != null)
            DungeonMsg.SetActive(false); // Hide the message at start
    }

    private IEnumerator EnableTriggerAfterDelay()
    {
        yield return new WaitForSeconds(1f); // Prevent instant re-entry
        canTrigger = true;
    }

    private void Update()
    {
        // Always follow the door position
        if (DungeonMsg != null && Dungeonlocation != null && DungeonMsg.activeSelf)
        {
            Vector3 screenPosition = Camera.main.WorldToScreenPoint(Dungeonlocation.position + offset);
            DungeonMsg.transform.position = screenPosition;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!canTrigger) return;

        if (!canEnterDungeon)
        {
            if (DungeonMsg != null)
            {
                DungeonMsg.SetActive(true);
                StartCoroutine(HideDungeonMsg());
            }
            return; // 🚫 Player can't enter
        }

        if (other.GetComponent<PlayerControler>())
        {
            string currentScene = SceneManager.GetActiveScene().name;
            SceneManagement.Instance.SetLastPosition(currentScene, other.transform.position);

            SceneManagement.Instance.SetTransitionName(sceneTransitionName);
            UIFade.Instance.FadeToBlack();
            StartCoroutine(LoadSceneRoutine());
        }
    }

    private IEnumerator HideDungeonMsg()
    {
        yield return new WaitForSeconds(2f);
        if (DungeonMsg != null)
        {
            DungeonMsg.SetActive(false);
        }
    }

    private IEnumerator LoadSceneRoutine()
    {
        while (waitToLoadTime >= 0) 
        {
            waitToLoadTime -= Time.deltaTime;
            yield return null;
        }

        SceneManager.LoadScene(sceneToLoad);
        DontDestroyOnLoad(PlayerControler.Instance.gameObject);
    }
}
