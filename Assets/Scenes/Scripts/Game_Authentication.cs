using LootLocker.Requests;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;

public class Game_Authentication : MonoBehaviour
{
    [SerializeField] TMP_InputField userName_input;
    [SerializeField] TMP_InputField password_input;

    private IEnumerator loginProcess;

    public void StatLoginProcess()
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
                return;
            }

            print("White Label Login successfull");
        });
    }

    public void Login()
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
                }
                else if(!response.SessionResponse.success){
                    print("Error while starting session\n" + response.SessionResponse.errorData.message);
                }
                print(response.errorData.message);
                return;
            }

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
            }
            else {
                print("Session is NOT VALID, we should show the login form");
            }
        });
    }

    //The responses from the various api calls will push this enumerator forward as the individual processes succeed
    IEnumerator LoginProcess()
    {
        Login();
        print("Waiting for login......");
        yield return null;

    }

}
public struct LootLockerPlayer
{
    public string memberID;
    public string name;
}
