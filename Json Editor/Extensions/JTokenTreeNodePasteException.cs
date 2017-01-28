using System;

namespace ZTn.Json.Editor.Extensions
{
    public class JTokenTreeNodePasteException : AggregateException
    {
        public JTokenTreeNodePasteException(Exception sourceException)
            : base(sourceException)
        {
        }
    }
}
