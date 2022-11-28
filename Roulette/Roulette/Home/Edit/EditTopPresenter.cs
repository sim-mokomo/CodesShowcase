using System;
using System.Linq;
using MokomoGames.UI;
using MokomoGames.Utilities;
using Protobuf;
using UnityEngine;

namespace Roulette.Home.Edit
{
    public class EditTopPresenter : MonoBehaviour, IReleaseable
    {
        [SerializeField] private IconButtonView addItemButtonView;
        [SerializeField] private IconButtonView addTemplateButtonView;
        [SerializeField] private TextButtonView submitButtonView;
        [SerializeField] private BackButtonView backButtonView;
        [SerializeField] private EditDataSetView editDataSetView;

        private void Awake()
        {
            addTemplateButtonView.OnClickedButton += () => OnClickedAddTemplateButton?.Invoke();
            addItemButtonView.OnClickedButton += () => OnClickedAddButton?.Invoke();
            backButtonView.OnClickedButton += () => OnClickedBackButton?.Invoke();
            submitButtonView.OnClickedButton += () => OnClickedSubmitButton?.Invoke();
            editDataSetView.OnClickedTrashButton += dataSetItem => OnClickedTrashButton?.Invoke(dataSetItem);
            editDataSetView.OnChangedItemContent += dataSetItem => OnChangedItemContent?.Invoke(dataSetItem);
        }

        public event Action OnClickedSubmitButton;
        public event Action OnClickedBackButton;
        public event Action OnClickedAddButton;
        public event Action OnClickedAddTemplateButton;
        public event Action<DataSetItem> OnClickedTrashButton;
        public event Action<DataSetItem> OnChangedItemContent;

        public void Init(Protobuf.DataSet dataSetEntity)
        {
            editDataSetView.Init(dataSetEntity);
            UpdateButtonsEnable(dataSetEntity);
        }

        public void Tick()
        {
            editDataSetView.Tick();
        }

        public void Dispose()
        {
            editDataSetView.Clear();
        }

        public void UpdateList(Protobuf.DataSet dataSet)
        {
            editDataSetView.UpdateList(dataSet.ItemList);
            UpdateButtonsEnable(dataSet);   
        }

        public void UpdateButtonsEnable(Protobuf.DataSet dataSet)
        {
            var isValid = dataSet.ItemList.Count > 0 &&
                          dataSet.ItemList.All(x => !string.IsNullOrEmpty(x.Name) && x.Num > 0);
            addTemplateButtonView.SetEnable(isValid);
            submitButtonView.SetEnable(isValid);
        }

        public void Release()
        {
            OnClickedBackButton = null;
            OnClickedSubmitButton = null;
            OnClickedAddButton = null;
            OnClickedAddTemplateButton = null;
            OnClickedTrashButton = null;
            OnChangedItemContent = null;
            
            editDataSetView.Clear();
        }
    }
}