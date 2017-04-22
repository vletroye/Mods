//===================================================================
//Copyright (C) 2010 Scott Wisniewski
//
//Permission is hereby granted, free of charge, to any person obtaining a copy of
//this software and associated documentation files (the "Software"), to deal in
//the Software without restriction, including without limitation the rights to
//use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies
//of the Software, and to permit persons to whom the Software is furnished to do
//so, subject to the following conditions:

//The above copyright notice and this permission notice shall be included in all
//copies or substantial portions of the Software.

//THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
//IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
//FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
//AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
//LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
//OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
//SOFTWARE.
//===================================================================

using System;

namespace WindowsFormsApplication1
{
    internal static class Util
    {
        public static void Assume
        (
            bool condition,
            string message
        )
        {
            if (!condition)
            {
                throw new InternalErrorException(message);
            }
        }


        public static T AssumeNotNull<T>
        (
            this T item
        ) where T : class
        {
            Assume(item != null, "Unexpected null value!");
            return item;
        }

        public static void Assume
        (
            bool condition
        )
        {
            Assume(condition, "The condition should not be false!");
        }

        public static IntPtr AssumeNonZero
        (
            this IntPtr item
        )
        {
            Assume(item != IntPtr.Zero);
            return item;
        }

        public static bool PathContains(this string parent, string child)
        {
            parent.AssumeNotNull();
            child.AssumeNotNull();

            if (!parent.EndsWith("\\"))
            {
                parent = parent + "\\";
            }

            return
                child.StartsWith(parent, StringComparison.OrdinalIgnoreCase) ||
                parent.TrimEnd('\\').Equals(child, StringComparison.OrdinalIgnoreCase);
        }
    }
}