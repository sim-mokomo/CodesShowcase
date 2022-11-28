using System;
using System.Collections.Generic;
using System.Linq;
using MokomoGames.Audio;
using MokomoGames.Audio.MasterTable;
using MokomoGames.Extensions;
using MokomoGames.GameConfig;
using MokomoGames.UI;
using MokomoGames.UI.Dialog;
using MokomoGames.Utilities;
using Roulette.Graph;
using UnityEngine;
using UnityEngine.UI;

namespace Roulette.Home.Top
{
    public class TopStatePresenter : MonoBehaviour, IReleaseable
    {
        [SerializeField] private Button startButton;
        [SerializeField] private Button configButton;
        [SerializeField] private Button moveToSettingStateButton;
        [SerializeField] private Button moveToTemplateStateButton;
        [SerializeField] private RouletteView rouletteView;
        private Protobuf.DataSet _dataSetEntity;

        private void Awake()
        {
            var audioService = new AudioService();
            var audioManager = FindObjectOfType<AudioManager>();
            var uiManager = FindObjectOfType<UIManager>();
            var gameConfigManager = FindObjectOfType<GameConfigManager>();
            startButton.onClick.AddListener(() =>
            {
                var canRotateRoulette = _dataSetEntity.ItemList.Any() && !rouletteView.IsRotating;
                if (!canRotateRoulette) return;

                audioService.PlayOneShot(audioManager, SoundName.DrumRoll01, gameConfigManager);
                rouletteView.StartRoulette();
            });

            configButton.onClick.AddListener(() =>
            {
                if (rouletteView.IsRotating)
                {
                    return;
                }
                OnClickedConfigButton?.Invoke();
            });
            moveToSettingStateButton.onClick.AddListener(() =>
            {
                if (rouletteView.IsRotating)
                {
                    return;
                }
                OnClickedEditButton?.Invoke();
            });
            moveToTemplateStateButton.onClick.AddListener(() =>
            {
                if (rouletteView.IsRotating)
                {
                    return;
                }
                OnClickedTemplateButton?.Invoke();
            });

            rouletteView.OnEnded += itemName =>
            {
                var item = _dataSetEntity.ItemList.FirstOrDefault(x => x.Name == itemName);
                if (item == null) return;
                var confirmDialog = uiManager.Create<ConfirmDialog>(UIManager.CanvasOrder.Front);
                confirmDialog.OnClosed += () =>
                {
                    uiManager.Destroy(confirmDialog);
                };
                confirmDialog.Open(new ConfirmDialog.ConfirmDialogParam
                {
                    dialogType = ConfirmDialog.DialogType.Confirm,
                    headerKey = "Result",
                    content = item.Name,
                });
                audioService.Stop(audioManager, SoundName.DrumRoll01);
                audioService.PlayOneShot(audioManager, SoundName.DrumRollFinish01, gameConfigManager);
            };
        }

        public event Action OnClickedConfigButton;
        public event Action OnClickedEditButton;
        public event Action OnClickedTemplateButton;

        public void Init(Protobuf.DataSet dataSetEntity)
        {
            _dataSetEntity = dataSetEntity;
            rouletteView.SetupGraph(CreateCircleGraphDataList(_dataSetEntity));
        }

        private static List<CircleGraphData> CreateCircleGraphDataList(Protobuf.DataSet dataSetEntity)
        {
            var total = (float)dataSetEntity.ItemList.Sum(x => x.Num);
            var graphViewDataList = dataSetEntity
                .ItemList
                .Select(x =>
                    new CircleGraphData(
                        x.Num / total,
                        x.Name,
                        ColorExtensions.ConvertProtoBufColorToUnityColor(x.Color))
                )
                .ToList();
            return graphViewDataList;
        }

        public void Release()
        {
            OnClickedConfigButton = null;
            OnClickedEditButton = null;
            OnClickedTemplateButton = null;
        }
    }
}