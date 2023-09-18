using LootLocker.Requests;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class Leaderboard_UI : MonoBehaviour
{
    [SerializeField] private GameObject leaderboardPrefab;
    [SerializeField] private RectTransform leaderboardContainer;

    public void ShowScores(LootLockerLeaderboardMember[] items)
    { 
        foreach(var item in items.OrderBy(x => x.rank)) {
            var newEntry = Instantiate(leaderboardPrefab, leaderboardContainer);
            newEntry.transform.Find("Name").GetComponent<TextMeshProUGUI>().text = item.player.name;
            newEntry.transform.Find("Score").GetComponent<TextMeshProUGUI>().text = item.score.ToString();
        }
    }
}
