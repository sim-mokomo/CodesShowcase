// using System;
// using Animation;
// using DG.Tweening;
// using Sirenix.OdinInspector;
// using UniRx;
// using UnityEngine;
// using UnityEngine.UI;
//
// namespace Store
// {
//     public class StoreItemButtonView : SerializedMonoBehaviour
//     {
//         [SerializeField] private Button purchaseButton;
//         [SerializeField] private ScaleAnimator scaleAnimator;
//         
//         public IObservable<Unit> OnClickPurchasedButton => purchaseButton.onClick.AsObservable();
//
//         public void Show(bool show,float duration)
//         {
//             scaleAnimator.PlayShowAnimation(show, duration);
//         }
//     }
// }