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
    [SerializeField] private CanvasGroup leaderboardCanvasGroup;

    [SerializeField] Game_Authentication Authentication;

    private int localMemeberScore = 0;

    private void OnEnable()
    {
        Authentication.OnSessionStart += CacheCurrentMemberScore;
    }

    private void OnDisable()
    {
        Authentication.OnSessionStart -= CacheCurrentMemberScore;
    }

    public void CacheCurrentMemberScore()
    {
        LootLockerSDKManager.GetMemberRank(LEADERBOARD_ID, string.Empty, (response) =>
        {
            if (!response.success)
                return;

            localMemeberScore = response.score;
        });
    }

    public void SendScoreToLootLocker()
    {
        localMemeberScore += 1;
        LootLockerSDKManager.SubmitScore(string.Empty, localMemeberScore, LEADERBOARD_ID, (response) =>
        {
            print("Score submitted: " + response.success);
        });
    }

    public void OpenLeaderboard()
    {
        leaderboardCanvasGroup.alpha = 1;
        leaderboardCanvasGroup.interactable = true;
        leaderboardCanvasGroup.blocksRaycasts = true;

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

    public void CloseLeaderboard()
    {
        for (int i = leaderboardEntryParent.childCount - 1; i >= 0; i--)
            Destroy(leaderboardEntryParent.GetChild(i).gameObject);

        leaderboardCanvasGroup.alpha = 0;
        leaderboardCanvasGroup.interactable = false;
        leaderboardCanvasGroup.blocksRaycasts = false;
    }
}
