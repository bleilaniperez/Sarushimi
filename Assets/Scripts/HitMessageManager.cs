using UnityEngine;

public class HitMessageManager : MonoBehaviour
{
    public GameObject missPrefab;
    public GameObject okPrefab;
    public GameObject goodPrefab;
    public GameObject perfectPrefab;

    public void ShowHitFeedback(string hitRating, Vector3 position)
    {
        GameObject feedbackObject = null;

        switch (hitRating)
        {
            case "MISS":
                feedbackObject = Instantiate(missPrefab, position + new Vector3(0f, 2f, 0f), Quaternion.identity);
                break;
            case "OK":
                feedbackObject = Instantiate(okPrefab, position, Quaternion.identity);
                break;
            case "GOOD":
                feedbackObject = Instantiate(goodPrefab, position, Quaternion.identity);
                break;
            case "PERFECT":
                feedbackObject = Instantiate(perfectPrefab, position, Quaternion.identity);
                break;
        }
    }
}
