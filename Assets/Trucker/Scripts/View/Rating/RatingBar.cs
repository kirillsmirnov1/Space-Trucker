using TMPro;
using Trucker.Model.Rating;
using UnityEngine;

namespace Trucker.View.Rating
{
    public class RatingBar : MonoBehaviour
    {
        [Header("Components")]
        [SerializeField] private TextMeshProUGUI progressPercents;
        [SerializeField] private TextMeshProUGUI progressRelation;
        
        [Header("Data")]
        [SerializeField] private EmployeePerformanceRating rating;
        
        private void OnEnable()
        {
            SetValues();
        }

        private void SetValues()
        {
            var currentRating = rating.Rating;
            var requiredRating = rating.RequiredRating;
            var percent = (int)((float) currentRating / requiredRating * 100);

            progressPercents.text = $"[{percent}%]";
            progressRelation.text = $"{currentRating} / {requiredRating}";
        }
    }
}