using System;

namespace ZTn.Json.Editor.Extensions
{
    public class JTokenTreeNodeDeleteException : AggregateException
    {
        public JTokenTreeNodeDeleteException(Exception sourceException)
            : base(sourceException)
        {
        }
    }
}
