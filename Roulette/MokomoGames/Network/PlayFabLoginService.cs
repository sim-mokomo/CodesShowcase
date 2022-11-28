using System;
using Cysharp.Threading.Tasks;
using MokomoGames.User;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine;
#if UNITY_ANDROID
using UnityEngine;
#endif

namespace MokomoGames.Network
{
    public class LoginService
    {
        public LoginService()
        {
#if DEV
            PlayFabSettings.staticSettings.TitleId = "";
#else
            PlayFabSettings.staticSettings.TitleId = "";
#endif
        }
#if DEV
        private void CustomLogin(string id, Action<LoginResult> onLoggedIn, Action<PlayFabError> onError)
        {
            PlayFabClientAPI.ForgetAllCredentials();
            var loginRequest = new LoginWithCustomIDRequest
            {
                CustomId = id,
                CreateAccount = true
            };
            PlayFabClientAPI.LoginWithCustomID(loginRequest, onLoggedIn, onError);
        }
        
        public UniTask<LoginResponse> CustomLoginAsync(string id)
        {
            var source = new UniTaskCompletionSource<LoginResponse>();
            CustomLogin(
                id,
                result => { source.TrySetResult(new LoginResponse(result, null)); },
                error => { source.TrySetResult(new LoginResponse(null, error)); }
            );
            return source.Task;
        }
#endif

        private void Login(
            Action<LoginResult> loggedInCallback, 
            Action<PlayFabError> errorCallback = null)
        {
            var userDataRepository = new UserSaveDataRepository();
            UserData userData;
            if (userDataRepository.SaveExists())
            {
                userData = userDataRepository.Load();
            }
            else
            {
                userData = new UserData
                {
                    UserId = Guid.NewGuid().ToString("N")
                };
                userDataRepository.Save(userData);
            }
            
            var deviceModel = SystemInfo.deviceModel;
#if UNITY_IOS || UNITY_EDITOR_OSX
            var loginWithIOSDeviceIDRequest = new LoginWithIOSDeviceIDRequest
            {
                DeviceModel = deviceModel,
                DeviceId = userData.UserId,
                CreateAccount = true,
            };
            PlayFabClientAPI.LoginWithIOSDeviceID(loginWithIOSDeviceIDRequest, loggedInCallback, errorCallback);
#elif UNITY_ANDROID
            var loginWithAndroidDeviceIDRequest = new LoginWithAndroidDeviceIDRequest
            {
                AndroidDevice = deviceModel,
                AndroidDeviceId = userData.UserId,
                CreateAccount = true,
            };
            PlayFabClientAPI.LoginWithAndroidDeviceID(loginWithAndroidDeviceIDRequest, loggedInCallback, errorCallback);
#endif
        }

        public class LoginResponse
        {
            public LoginResult LoginResult { get; }
            public PlayFabError LoginError { get; }

            public LoginResponse(LoginResult loginResult, PlayFabError loginError)
            {
                LoginResult = loginResult;
                LoginError = loginError;
            }
        }

        public UniTask<LoginResponse> LoginAsync()
        {
            var source = new UniTaskCompletionSource<LoginResponse>();
            Login(
                result => { source.TrySetResult(new LoginResponse(result, null)); },
                error => { source.TrySetResult(new LoginResponse(null, error)); });

            return source.Task;
        }
    }
}