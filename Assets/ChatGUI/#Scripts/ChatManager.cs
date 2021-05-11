using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Events;

namespace Innovationscenter.ChatGUI
{
    [System.Serializable]
    public class TextMessageEvent : UnityEvent<ChatManager.TextMessage>
    {
    }

public class ChatManager : MonoBehaviour
    {
        private static ChatManager _instance;
        public static ChatManager Instance { get { return _instance; } }

        private void Awake()
        {
            if (_instance != null && _instance != this)
            {
                Destroy(this.gameObject);
            }
            else
            {
                _instance = this;
            }
        }

        [SerializeField] private GameObject prefabMyMessage;
        [SerializeField] private GameObject prefabOthersMessage;
        [SerializeField] private RectTransform content;
        [SerializeField] private ScrollRect scrollRect;
        [SerializeField] private RectTransform panelBack;
        [SerializeField] private float keyboardHeightAndroid = 500f;

        [SerializeField] private TMP_InputField messageTextInput;

        private bool KeyboardVisible
        {
            get
            {
                return keyboardVisible;
            }
            set
            {
                if(keyboardVisible != value)
                {
                    keyboardVisible = value;
                    KeyboardVisibleChanged();
                }
            }
        }
        private bool keyboardVisible = false;

        public TextMessageEvent textMessageEvent;

        private bool isEditing = false;

        // Start is called before the first frame update
        void Start()
        {
            TouchScreenKeyboard.hideInput = true;

            //messageTextInput.Select();
            //messageTextInput.ActivateInputField();
        }

        // Update is called once per frame
        void Update()
        {
            if (TouchScreenKeyboard.visible)
            {
                KeyboardVisible = true;
            } else
            {
                KeyboardVisible = false;
            }
            if (isEditing)
            {
                if (Input.GetKeyDown(KeyCode.Return))
                {
                    NewMyMessage();
                }
            }
        }

        private void KeyboardVisibleChanged()
        {
            if (TouchScreenKeyboard.visible)
            {
                float height = GetKeyboardHeight();
                panelBack.offsetMin = new Vector2(panelBack.offsetMin.x, height);
                scrollRect.verticalNormalizedPosition = 0f;
            }
            else
            {
                panelBack.offsetMin = new Vector2(panelBack.offsetMin.x, 0f);
                scrollRect.verticalNormalizedPosition = 0f;
                NewMyMessage();
            }
        }

        private float GetKeyboardHeight()
        {
            if(Application.platform == RuntimePlatform.IPhonePlayer)
            {
                return TouchScreenKeyboard.area.height;

            } else if(Application.platform == RuntimePlatform.Android)
            {
                //Solution for getting the keyboard height needed
                return keyboardHeightAndroid;
            } else
            {
                return TouchScreenKeyboard.area.height;
            }
        }

        public void OnInputStartEdit()
        {
            isEditing = true;
        }

        public void OnInputExit()
        {
            isEditing = false;
        }

        public void NewMyMessage()
        {
            if (string.IsNullOrEmpty(messageTextInput.text)) return;

            TextMessage textMessage = new TextMessage
            {
                MessageType = TextMessage.MessageTypes.MyMessage,
                Text = messageTextInput.text
            };

            InitNewMessage(prefabMyMessage, textMessage);

            textMessageEvent.Invoke(textMessage);

            messageTextInput.text = "";
            //messageTextInput.Select();
            //messageTextInput.ActivateInputField();
        }

        public void NewOthersMessage(TextMessage textMessage)
        {
            InitNewMessage(prefabOthersMessage, textMessage);
        }

        private void InitNewMessage(GameObject messagePrefab, TextMessage textMessage)
        {
            //Init a new gameobject
            GameObject newMessage = Instantiate(messagePrefab, content);

            //Set the textmessage
            ChatMessage chatMessage = newMessage.GetComponent<ChatMessage>();
            chatMessage.ShowMessage(textMessage, content);

            scrollRect.verticalNormalizedPosition = 0f;
        }

        public class TextMessage
        {
            public enum MessageTypes { MyMessage, OthersMessage }

            public string Text { get; set; }
            public string Sender { get; set; }
            public MessageTypes MessageType { get; set; }
        }
    }
}


