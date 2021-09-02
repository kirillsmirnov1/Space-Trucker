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
            // TODO 
        }

    }
}