// using System;
// using Animation;
// using UnityEngine;
// using UnityEngine.UI;
//
// namespace Store
// {
//     public class RestorePurchaseItemPresenter : MonoBehaviour
//     {
//         [SerializeField] private Button restoreButton;
//         [SerializeField] private DrawerAnimator drawerAnimator;
//         [SerializeField] private float drawAnimnDuration;
//
//         private void Awake()
//         {
//             restoreButton.onClick.AddListener(() => { OnRestore?.Invoke(); });
//         }
//
//         public event Action OnRestore;
//
//         public void Show(bool show)
//         {
//             if (show)
//                 drawerAnimator.OpenAnimation(drawAnimnDuration);
//             else
//                 drawerAnimator.CloseAnimation(drawAnimnDuration);
//         }
//     }
// }