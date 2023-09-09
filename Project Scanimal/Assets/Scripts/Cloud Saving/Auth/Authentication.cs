using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Services.Core;
using Unity.Services.Authentication;
using System.Threading.Tasks;
using TMPro;
using System;

public class Authentication : MonoBehaviour
{
    public TextMeshProUGUI outputText;

    async void Start()
    { 
        await UnityServices.InitializeAsync();
    }

    public async void SignIn()
    {
        await SignInAnonymuos();
    }

    async Task SignInAnonymuos()
    {
        if (!AuthenticationService.Instance.IsAuthorized)
        {
            try
            {
                await AuthenticationService.Instance.SignInAnonymouslyAsync();

                outputText.text = "ID: " + AuthenticationService.Instance.PlayerId;

            }
            catch (AuthenticationException ex)
            {
                Debug.LogException(ex);
            }
        }
        else
        {
            outputText.text = "ID: " + AuthenticationService.Instance.PlayerId;
        }
    }
}
