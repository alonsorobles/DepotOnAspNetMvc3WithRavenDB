using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Depot.Web.Helpers
{
    public static class FlashHtmlHelper
    {
        private static readonly Dictionary<FlashMessageType, string> DefaultFlashMessageTypeTagClass = new Dictionary
            <FlashMessageType, string>
                                                                                                         {
                                                                                                             {
                                                                                                                 FlashMessageType
                                                                                                                 .Notice
                                                                                                                 ,
                                                                                                                 "notice"
                                                                                                                 },
                                                                                                             {
                                                                                                                 FlashMessageType
                                                                                                                 .Error,
                                                                                                                 "error"
                                                                                                                 }
                                                                                                         };

        public static MvcHtmlString Flash(this HtmlHelper instance,
                                          FlashMessageType messageType,
                                          string tagName = @"div",
                                          string tagClass = null)
        {
            var messageData = new FlashMessageData(instance.ViewContext.TempData);
            var messages = messageData.PopMessages(messageType);
            if (string.IsNullOrEmpty(tagClass))
                tagClass = DefaultFlashMessageTypeTagClass[messageType];
            var html = string.Empty;
            foreach (var message in messages)
            {
                var tagBuilder = new TagBuilder(tagName);
                tagBuilder.Attributes.Add("id", "flash");
                tagBuilder.Attributes.Add("class", tagClass);
                tagBuilder.SetInnerText(message);
                html += tagBuilder.ToString();
            }
            return new MvcHtmlString(html);
        }
    }

    public class FlashMessageData
    {
        private static readonly string FlashDataKey = typeof (FlashMessageData).FullName;

        private readonly TempDataDictionary _tempDataDictionary;

        public FlashMessageData(TempDataDictionary tempDataDictionary)
        {
            _tempDataDictionary = tempDataDictionary;
        }

        public void Add(FlashMessageType messageType, string message)
        {
            if (string.IsNullOrWhiteSpace(message))
                return;
            IDictionary<FlashMessageType, IList<string>> messageDictionary;
            
            if (_tempDataDictionary.ContainsKey(FlashDataKey))
            {
                messageDictionary = (IDictionary<FlashMessageType, IList<string>>) _tempDataDictionary[FlashDataKey];
            }
            else
            {
                messageDictionary = new Dictionary<FlashMessageType, IList<string>>();
                _tempDataDictionary.Add(FlashDataKey, messageDictionary);
            }
            
            if (messageDictionary.ContainsKey(messageType))
            {
                messageDictionary[messageType].Add(message);
            }
            else
            {
                messageDictionary.Add(messageType, new List<string>{message});
            }

            _tempDataDictionary.Keep(FlashDataKey);
        }

        public IList<string> PopMessages(FlashMessageType messageType)
        {
            if (!_tempDataDictionary.ContainsKey(FlashDataKey))
                return new List<string>();
            var messageDictionary = (IDictionary<FlashMessageType, IList<string>>)_tempDataDictionary[FlashDataKey];
            if (messageDictionary.ContainsKey(messageType))
            {
                var messages = messageDictionary[messageType];
                messageDictionary.Remove(messageType);
                return messages;
            }
            return new List<string>();
        }
    }

    public enum FlashMessageType
    {
        Notice,
        Error
    }
}