using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using Button = UnityEngine.UI.Button;

namespace MokomoGames.Network.Debugger
{
    public class LoginDebugUserPresenter : MonoBehaviour
    {
        [SerializeField] private ScrollRect scrollRect;
        [SerializeField] private Button closeButton;
        [SerializeField] private TMP_InputField addUserIdInputField;
        [SerializeField] private Button addUserButton;
        private List<LoginDebugUserView> _loginDebugUserViews;
        [SerializeField] private LoginDebugUserView loginDebugUserViewPrefab;

        public event Action onClickedClose;
        public event Action<string> onClickedAddUserButton;
        public event Action<string> onClickedLoginUserButton;

        public void Initialize(LoginUserIdList loginUserIdList)
        {
            onClickedClose = null;
            onClickedAddUserButton = null;
            onClickedLoginUserButton = null;

            addUserIdInputField.text = string.Empty;
            addUserButton.onClick.RemoveAllListeners();
            addUserButton.onClick.AddListener(() =>
            {
                onClickedAddUserButton?.Invoke(addUserIdInputField.text);
            });
            
            closeButton.onClick.RemoveAllListeners();
            closeButton.onClick.AddListener(() =>
            {
                onClickedClose?.Invoke();
            });
            
            RefreshDebugUserList(loginUserIdList);
        }

        public void RefreshDebugUserList(LoginUserIdList loginUserIdList)
        {
            foreach (Transform child in scrollRect.content.transform)
            {
                Destroy(child.gameObject);
            }
            
            _loginDebugUserViews = loginUserIdList.idList
                .Select(userId =>
                {
                    var view = Instantiate(loginDebugUserViewPrefab, scrollRect.content.transform);
                    view.transform.localScale = Vector3.one;
                    view.Initialize(userId);
                    return view;
                })
                .ToList();
            _loginDebugUserViews.ForEach(x =>
            {
                x.OnClickedCell += id =>
                {
                    onClickedLoginUserButton?.Invoke(id);
                };
            });
        }
    }
}