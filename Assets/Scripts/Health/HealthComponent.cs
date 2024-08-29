using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HealthComponent : MonoBehaviour
{
    // Start is called before the first frame update
    public void Damage(float damage = 1)
    {
        GameStats.Instance.CurrentLives -= damage;

        if (GameStats.Instance.CurrentLives <= 0)
            StartCoroutine(GameOver());
    }

    public void Heal(float amount = 1)
    {
        GameStats.Instance.CurrentLives += amount;
    }

    private IEnumerator GameOver()
    {
        Debug.Log("GAME OVER!");

        yield return new WaitForSeconds(3);

        SceneManager.LoadScene("EndScene", LoadSceneMode.Additive);
    }
}
