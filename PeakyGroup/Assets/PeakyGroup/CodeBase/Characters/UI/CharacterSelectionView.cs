using SelectionSystem.Core.UI;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace SelectionSystem.Character
{
    public sealed class CharacterSelectionView : BaseSelectionView<CharacterData>
    {
        [Header("Canvas UI")]
        [SerializeField] private TextMeshProUGUI nameTMP;
        [SerializeField] private TextMeshProUGUI levelTMP;
        [SerializeField] private Image avatarImage;
        [SerializeField] private Button nextButton;
        [SerializeField] private Button previousButton;
        [SerializeField] private Button closeButton;
        
        [Header("3D Preview")]
        [SerializeField] private Transform model3DRoot;

        private GameObject _currentModel;
        private CharacterSelector _selector;
        
        private new void OnDestroy()
        {
            nextButton?.onClick.RemoveAllListeners();
            previousButton?.onClick.RemoveAllListeners();
            closeButton?.onClick.RemoveAllListeners();
        }
        
        public void SetSelector(CharacterSelector selector)
        {
            _selector = selector;

            nextButton?.onClick.RemoveAllListeners();
            nextButton?.onClick.AddListener(_selector.SelectNext);
            previousButton?.onClick.RemoveAllListeners();
            previousButton?.onClick.AddListener(_selector.SelectPrevious);
            closeButton?.onClick.RemoveAllListeners();
            closeButton?.onClick.AddListener(DestroyView);
        }

        protected override void OnSelectionChanged(CharacterData data)
        {
            if (data == null)
                return;

            nameTMP?.SetText(data.DisplayName);
            levelTMP?.SetText($"Level {data.Level}");
            if (avatarImage != null)
            {
                avatarImage.sprite = data.Avatar;
                avatarImage.enabled = data.Avatar != null;
            }
            
            Update3DModel(data);
        }

        private void Update3DModel(CharacterData data)
        {
            if (model3DRoot == null)
                return;
            
            if (_currentModel != null)
                Destroy(_currentModel);

            if (data.Model3DPrefab == null) 
                return;
            
            _currentModel = Instantiate(data.Model3DPrefab, model3DRoot);
            _currentModel.transform.localPosition = Vector3.zero;
            _currentModel.transform.localRotation = Quaternion.identity;
        }
    }
}