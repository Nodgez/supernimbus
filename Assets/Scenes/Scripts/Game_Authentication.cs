using LootLocker.Requests;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;
using UnityEngine.UI;

public class Game_Authentication : MonoBehaviour
{
    [SerializeField] private TMP_InputField userName_input;
    [SerializeField] private TMP_InputField password_input;
    [SerializeField] private Button leaderboardButton;
    [SerializeField] private CanvasGroup canvasGroup;

    public Action OnSessionStart;

    private IEnumerator loginProcess;

    private void Start()
    {
        CheckForSession();
    }

    public void StartLoginProcess()
    {
        loginProcess = LoginProcess();
        loginProcess.MoveNext();
    }

    public void SignUp()
    {
        string email = userName_input.text;
        string pwd = password_input.text;

        LootLockerSDKManager.WhiteLabelSignUp(email, pwd, (response) =>
        {
            if(!response.success)
            {
                print(response.errorData.message);
                Alert.Instance.ShowMessage(response.errorData.message);
                return;
            }

            print("White Label Login successfull");
            OnSessionStart?.Invoke();
            SetJoinSessionView();
        });
    }

    private void Login()
    {
        string email = userName_input.text;
        string pwd = password_input.text;
        bool rememberMe = false;

        LootLockerSDKManager.WhiteLabelLoginAndStartSession(email, pwd, rememberMe, (response) =>
        {
            if(!response.success)
            {
                if(!response.LoginResponse.success)
                {
                    print("Error while logging in\n" + response.LoginResponse.errorData.message);
                    Alert.Instance.ShowMessage("Login failed");
                }
                else if(!response.SessionResponse.success){
                    print("Error while starting session\n" + response.SessionResponse.errorData.message);
                    Alert.Instance.ShowMessage("Login Failed");
                }
                print(response.errorData.message);
                return;
            }
            OnSessionStart?.Invoke();
            loginProcess.MoveNext();
        });
    }


    private void CheckForSession()
    {
        LootLockerSDKManager.CheckWhiteLabelSession(response =>
        {
            if (response)
            {
                print("session is valid, you can start a game session");
                Alert.Instance.ShowMessage("Entering Lootlocker Session");

                LootLockerSDKManager.StartWhiteLabelSession((response) => {
                    if (!response.success)
                    {
                        SetLoginView();
                        return;
                    }
                        
                    SetJoinSessionView();

                });

            }
            else {
                print("Session is NOT VALID, we should show the login form");
                SetLoginView();
            }
        });
    }

    //The responses from the various api calls will push this enumerator forward as the individual processes succeed
    IEnumerator LoginProcess()
    {
        Login();
        print("Waiting for login......");
        yield return null;
        print("enabling Leaderboards and game UI");
        SetJoinSessionView();
    }

    private void SetLoginView()
    {
        canvasGroup.interactable = true;
        leaderboardButton.interactable = false;
        LobbyUI.Instance.TurnOff();
    }

    private void SetJoinSessionView() {
        canvasGroup.interactable = false;
        leaderboardButton.interactable = true;
        LobbyUI.Instance.TurnOn();
    }

}
public struct LootLockerPlayer
{
    public string memberID;
    public string name;
}
