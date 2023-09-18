using LootLocker.Requests;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class Leaderboards : MonoBehaviour
{
    const string LEADERBOARD_ID = "17442";

    [SerializeField] private GameObject leaderboardEntryPrefab;
    [SerializeField] private RectTransform leaderboardEntryParent;
    public void SendScoreToLootLocker(int score)
    {
        LootLockerSDKManager.SubmitScore(string.Empty, score,LEADERBOARD_ID, (response) =>
        { });
    }

    public void OpenLeaderboard()
    {
        LootLockerSDKManager.GetScoreList(LEADERBOARD_ID, 10, (response) => {
            if (!response.success)
                return;

            for (int i = leaderboardEntryParent.childCount - 1; i >= 0; i--)
                Destroy(leaderboardEntryParent.GetChild(i).gameObject);

            foreach (var p in response.items.OrderBy(item => item.score))
            {
                var entry = Instantiate(leaderboardEntryPrefab, leaderboardEntryParent);
                entry.transform.SetAsLastSibling();
                entry.transform.Find("Name").GetComponent<TMPro.TextMeshProUGUI>().text = p.player.name;
                entry.transform.Find("Score").GetComponent<TMPro.TextMeshProUGUI>().text = p.score.ToString();
            }
        });
    }
}
