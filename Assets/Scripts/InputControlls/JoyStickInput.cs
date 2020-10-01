using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace GameInput
{
    public class JoyStickInput : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler
    {
        [SerializeField]
        float _offset;
        private Image _bgImage;
        private Image _joystickImage;
        public Vector2 _inputDir { set; get;}
        public static JoyStickInput _instance;
        private void Start()
        {
            _instance = this;
            _bgImage = GetComponent<Image>();
            _joystickImage = transform.GetChild(0).GetComponent<Image>();
            _inputDir = Vector2.zero;
        }
        public void OnDrag(PointerEventData eventData)
        {
            Vector2 pos = Vector2.zero;
            float _bgImageSizeX = _bgImage.rectTransform.sizeDelta.x;
            float _bgImageSizeY = _bgImage.rectTransform.sizeDelta.y;
            if(RectTransformUtility.ScreenPointToLocalPointInRectangle(_bgImage.rectTransform,eventData.position, eventData.pressEventCamera,out pos))
            {
                pos.x /= _bgImageSizeX;
                pos.y /= _bgImageSizeY;
                _inputDir = new Vector2(pos.x, pos.y);
                _inputDir = _inputDir.magnitude > 1 ? _inputDir.normalized : _inputDir;
                _joystickImage.rectTransform.anchoredPosition = new Vector2(_inputDir.x * _bgImageSizeX/_offset, _inputDir.y * _bgImageSizeY/_offset);
              
            }

        }

        public void OnPointerDown(PointerEventData eventData)
        {
            OnDrag(eventData);
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            _joystickImage.rectTransform.anchoredPosition = Vector2.zero;
        }
    }
}