using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Events;

namespace Innovationscenter.ChatGUI
{
    [System.Serializable]
    public class MessageEvent : UnityEvent<ChatManager.IMessage>
    {
    }

    [System.Serializable]
    public class MessageOthersButtonInteractEvent : UnityEvent<ChatManager.MessageOthersButton>
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
        [SerializeField] private GameObject prefabOthersButtonMessage;
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

        public MessageEvent messageEvent;
        public MessageOthersButtonInteractEvent messageOthersButtonInteractEvent;

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
                    NewMessage(new MessageMyText
                    {
                        Text = messageTextInput.text
                    });
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
                NewMessage(new MessageMyText
                {
                    Text = messageTextInput.text
                });
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

        public void OnButtonSend()
        {
            NewMessage(new MessageMyText
            {
                Text = messageTextInput.text
            });
        }

        public void NewMessage(IMessage message)
        {
            if(message is MessageMyText)
            {
                ShowMessageMyText((MessageMyText) message);
            } else if(message is MessageOthersText)
            {
                ShowMessageOthersText((MessageOthersText)message);
            } else if(message is MessageOthersButton)
            {
                ShowMessageOthersButton((MessageOthersButton)message);
            }
        }

        private void ShowMessageMyText(MessageMyText message)
        {
            if (string.IsNullOrEmpty(messageTextInput.text)) return;

            InitNewMessage(prefabMyMessage, message);

            messageEvent.Invoke(message);

            messageTextInput.text = "";
        }

        private void ShowMessageOthersText(MessageOthersText message)
        {
            InitNewMessage(prefabOthersMessage, message);
        }

        private void ShowMessageOthersButton(MessageOthersButton message)
        {
            InitNewMessage(prefabOthersButtonMessage, message);
        }

        private void InitNewMessage(GameObject messagePrefab, IMessage message)
        {
            //Init a new gameobject
            GameObject newMessage = Instantiate(messagePrefab, content);

            //Set the textmessage
            IChatMessage chatMessage = newMessage.GetComponent<IChatMessage>();
            chatMessage.ShowMessage(message, content);

            scrollRect.verticalNormalizedPosition = 0f;
        }

        public interface IMessage
        {

        }

        public class MessageMyText : IMessage
        {
            public string Text { get; set; }
        }

        public class MessageOthersText : IMessage
        {
            public string Text { get; set; }
            public string Sender { get; set; }
        }

        public class MessageOthersButton : IMessage
        {
            public string ButtonText { get; set; }
            public string Sender { get; set; }
        }
    }
}


