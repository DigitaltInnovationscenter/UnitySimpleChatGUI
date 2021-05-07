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

        [SerializeField] private TMP_InputField messageTextInput;

        public TextMessageEvent textMessageEvent;

        private bool isEditing = false;

        // Start is called before the first frame update
        void Start()
        {
            messageTextInput.Select();
            messageTextInput.ActivateInputField();
        }

        // Update is called once per frame
        void Update()
        {
            if (isEditing)
            {
                if (Input.GetKeyDown(KeyCode.Return))
                {
                    NewMyMessage();
                }
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
            messageTextInput.Select();
            messageTextInput.ActivateInputField();
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


