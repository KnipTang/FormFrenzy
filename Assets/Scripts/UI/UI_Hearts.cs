using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Hearts : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private GameObject _heartsPrefab;
    [SerializeField] private Transform _heartsPosition;
    private List<GameObject> _hearts = new List<GameObject>(); // List to store references to heart GameObjects

    // Start is called before the first frame update
    void Start()
    {
        // Instantiate or activate the hearts
        UpdateHearts();
    }

    // Method to update the hearts based on CurrentLives value
    public void UpdateHearts()
    {
        Debug.Log("Hearts");
        // Clear the existing hearts
        ClearHearts();

        Vector3 heartPosition = _heartsPosition.position;

        // Instantiate or activate the hearts based on CurrentLives
        for (int i = 0; i < GameStats.Instance.CurrentLives; i++)
        {
            GameObject heart = Instantiate(_heartsPrefab, _heartsPosition);
            // Position heart
            heart.transform.position = heartPosition;

            // Add heart to list
            _hearts.Add(heart);

            // Update position for next heart
            heartPosition.x += _heartsPrefab.GetComponent<RectTransform>().rect.width;
        }
    }

    // Method to clear the existing hearts
    private void ClearHearts()
    {
        foreach (GameObject heart in _hearts)
        {
            Destroy(heart);
        }
        _hearts.Clear();
    }
}
