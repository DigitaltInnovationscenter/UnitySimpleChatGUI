using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Innovationscenter.ChatGUI
{
    public class ChatMessageMyText : ChatMessage
    {
        [SerializeField] private TextMeshProUGUI textMessageText;
        [SerializeField] private float messageSpace = 30f;

        public override void ShowMessage(ChatManager.IMessage newMessage, RectTransform contentView)
        {
            RectTransform rectTransform = GetComponent<RectTransform>();

            ChatManager.MessageMyText messageMyText = (ChatManager.MessageMyText)newMessage;
            Message = messageMyText;

            float messageHeight = 0f;

            textMessageText.text = messageMyText.Text;
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
            messagePos.x = -(rectTransform.sizeDelta.x / 2f);

            rectTransform.anchoredPosition = messagePos;
        }
    }

}

