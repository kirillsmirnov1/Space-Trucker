using System.Collections;
using TMPro;
using UnityEngine;

namespace Trucker.View.Notifications
{
    public class NotificationView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI text;

        private const float ShowDuration = 5f; // IMPR extract settings 
        private const float FadeDuration = 1f;
        private const int FadeSteps = 20;

        public void Display(string str, bool strikethrough)
        {
            StopAllCoroutines();
            SetText(str, strikethrough);
            SetObject();
            StartCoroutine(DisplayCoroutine());
        }

        private void SetText(string str, bool strikethrough)
        {
            text.text = str;
            text.fontStyle = strikethrough ? FontStyles.Bold | FontStyles.Strikethrough : FontStyles.Bold;
            text.color = Color.white;
        }

        private void SetObject()
        {
            transform.SetAsLastSibling();
            gameObject.SetActive(true);
        }

        private IEnumerator DisplayCoroutine()
        {
            yield return new WaitForSeconds(ShowDuration);

            for (float i = FadeSteps; i >= 0; i--)
            {
                yield return new WaitForSeconds(FadeDuration / FadeSteps);
                var alpha = i / FadeSteps;
                text.color = new Color(1, 1, 1, alpha);
            }
            
            gameObject.SetActive(false);
        }
    }
}