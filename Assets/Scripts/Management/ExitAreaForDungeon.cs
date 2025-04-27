using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitAreaForDungeon : MonoBehaviour
{
      [SerializeField] private string sceneToLoad;
    [SerializeField] private string sceneTransitionName;
    private float waitToLoadTime = 1f;
    private bool canTrigger = false;

    private void Start()
    {
        StartCoroutine(EnableTriggerAfterDelay());
    }

    private IEnumerator EnableTriggerAfterDelay()
    {
        yield return new WaitForSeconds(1f); // Prevent instant re-entry
        canTrigger = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!canTrigger) return;

        if (other.GetComponent<PlayerControler>())
        {
            string currentScene = SceneManager.GetActiveScene().name;
            SceneManagement.Instance.SetLastPosition(currentScene, other.transform.position);

            SceneManagement.Instance.SetTransitionName(sceneTransitionName);
            UIFade.Instance.FadeToBlack();
            StartCoroutine(LoadSceneRoutine());
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
