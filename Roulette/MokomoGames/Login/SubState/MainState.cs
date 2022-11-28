#if UNITY_EDITOR
using UnityEditor;
#endif
using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using MokomoGames.Debugger;
using MokomoGames.GameConfig;
using MokomoGames.Localization;
using MokomoGames.Network;
using MokomoGames.StateMachine;
using MokomoGames.User;
using UnityEngine;
using Object = UnityEngine.Object;

namespace MokomoGames.Login.SubState
{
    internal class MainState : State
    {
        public event Action OnCompletedBegin;

        public override async UniTask Begin(StateChangeRequest inputData, CancellationToken ct)
        {
            await base.Begin(inputData, ct);

#if DEV
            await new LoginService().CustomLoginAsync("LoginUser");
#else
            await new LoginService().LoginAsync();
#endif
            
#if UNITY_EDITOR
            var gameDebugSaveData = AssetDatabase.LoadAssetAtPath<GameDebugSaveData>(GameDebugSaveData.SaveDataFileName);
            await Object
                .FindObjectOfType<LocalizeManager>()
                .LoadAsync(gameDebugSaveData.GameLanguage);
#else
            await Object
                .FindObjectOfType<LocalizeManager>()
                .LoadAsync(Application.systemLanguage);
#endif


            var inventoryManager = Object.FindObjectOfType<InventoryManager>();
            await inventoryManager.LoadInventoryItems();
            
            // var storeManager = Object.FindObjectOfType<StoreManager>();
            // storeManager.IapService.OnPurchased += async () =>
            // {
            //     await inventoryManager.LoadInventoryItems();
            // };
            // storeManager.IapService.OnRestored += async () =>
            // {
            //     await inventoryManager.LoadInventoryItems();
            // };
            // await storeManager.InitializeAsync();

            Object.FindObjectOfType<GameConfigManager>().Load();

            OnCompletedBegin?.Invoke();
        }

        public override StateHistoryItem CreateHistory()
        {
            return null;
        }

        public class Request : StateChangeRequest
        {
        }
    }
}