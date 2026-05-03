using SelectionSystem.Core.UI;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace SelectionSystem.Location.UI
{
    public sealed class LocationSelectionView : BaseSelectionView<LocationData>
    {
        [Header("UI Elements")]
        [SerializeField] private TextMeshProUGUI titleTMP;
        [SerializeField] private TextMeshProUGUI descriptionTMP;
        [SerializeField] private Image previewImage;
        [SerializeField] private Button loadButton;
        [SerializeField] private Button nextButton;
        [SerializeField] private Button previousButton;
        [SerializeField] private Button closeButton;

        private LocationSelector _selector;
        
        private new void OnDestroy()
        {
            loadButton?.onClick.RemoveAllListeners();
            nextButton?.onClick.RemoveAllListeners();
            previousButton?.onClick.RemoveAllListeners();
            closeButton?.onClick.RemoveAllListeners();
        }
        
        public void SetSelector(LocationSelector selector)
        {
            _selector = selector;

            loadButton?.onClick.RemoveAllListeners();
            loadButton?.onClick.AddListener(_selector.LoadCurrentLocation);
            nextButton?.onClick.RemoveAllListeners();
            nextButton?.onClick.AddListener(_selector.SelectNext);
            previousButton?.onClick.RemoveAllListeners();
            previousButton?.onClick.AddListener(_selector.SelectPrevious);
            closeButton?.onClick.RemoveAllListeners();
            closeButton?.onClick.AddListener(DestroyView);
        }

        protected override void OnSelectionChanged(LocationData data)
        {
            if (data == null)
                return;
            
            titleTMP?.SetText(data.DisplayName);
            descriptionTMP?.SetText(data.Description);
            if(data.PreviewImage != null)
            {
                previewImage.sprite = data.PreviewImage;
                previewImage.enabled = data.PreviewImage != null;
            }
        }
    }
}