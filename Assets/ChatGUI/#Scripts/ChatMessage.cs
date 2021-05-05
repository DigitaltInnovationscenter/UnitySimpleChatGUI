using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace Innovationscenter.ChatGUI
{
    public class ChatMessage : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI textMessageText;
        [SerializeField] private TextMeshProUGUI textMessageSender;
        [SerializeField] private float paddingHieght = 10f;
        [SerializeField] private float messageSpaceMy = 30f;
        [SerializeField] private float messageSpaceOthers = 60f;

        public ChatManager.TextMessage TextMessage
        {
            get
            {
                return textMessage;
            }
            private set
            {
                textMessage = value;
            }
        }
        private ChatManager.TextMessage textMessage;

        private void Start()
        {

        }

        public void ShowMessage(ChatManager.TextMessage textMessage, RectTransform contentView)
        {
            RectTransform rectTransform = GetComponent<RectTransform>();

            TextMessage = textMessage;

            float messageHeight = 0f;
            float messageSpace = messageSpaceMy;

            if (textMessage.MessageType == ChatManager.TextMessage.MessageTypes.OthersMessage)
            {
                textMessageSender.text = TextMessage.Sender;
                textMessageSender.ForceMeshUpdate();
                messageSpace = messageSpaceOthers;
            }

            textMessageText.text = TextMessage.Text;
            textMessageText.ForceMeshUpdate();

            messageHeight += textMessageText.textBounds.size.y;
            messageHeight += paddingHieght;

            Vector2 size = rectTransform.sizeDelta;
            size.y = messageHeight;
            rectTransform.sizeDelta = size;

            Vector2 contentSize = contentView.sizeDelta;
            contentSize.y += messageHeight + messageSpace;
            contentView.sizeDelta = contentSize;

            Vector2 messagePos = new Vector2();
            messagePos.y = -contentView.sizeDelta.y + (messageHeight / 2f);

            if (textMessage.MessageType == ChatManager.TextMessage.MessageTypes.MyMessage)
            {
                messagePos.x = -(rectTransform.sizeDelta.x / 2f);
            }
            else
            {
                messagePos.x = rectTransform.sizeDelta.x / 2f;
            }

            rectTransform.anchoredPosition = messagePos;
        }

    }
}

