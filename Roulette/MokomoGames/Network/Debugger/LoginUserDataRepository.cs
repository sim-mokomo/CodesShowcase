using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using PlayFab.ClientModels;
using UnityEngine;

namespace MokomoGames.Network.Debugger
{
    public class LoginUserDataRepository
    {
        private const string UserIdListKey = "user_id_list";
        
        public async UniTask<LoginUserIdList> LoadUserIdList()
        {
            try
            {
                var request = new GetUserDataRequest
                {
                    Keys = new List<string> { UserIdListKey }
                };
                var userData = await PlayFabExtensions.GetUserDataAsync(request);
                var json = userData.Data[UserIdListKey].Value;
                Debug.Log($"Load UserId List {json}");
                return JsonUtility.FromJson<LoginUserIdList>(json);
            }
            catch (KeyNotFoundException keyNotFoundException)
            {
                return new LoginUserIdList();
            }
        }

        public async UniTask SaveUserIdList(LoginUserIdList loginUserIdList)
        {
            var json = JsonUtility.ToJson(loginUserIdList);
            var request = new UpdateUserDataRequest
            {
                Data = new Dictionary<string, string>
                {
                    { UserIdListKey, json }
                }
            };
            Debug.Log($"Save UserId List {json}");
            await PlayFabExtensions.UpdateUserDataAsync(request); 
        }
    }
}