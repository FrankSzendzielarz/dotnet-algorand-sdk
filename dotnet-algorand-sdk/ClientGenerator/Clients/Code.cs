using System;
using System.Collections.Generic;
using System.Text;

namespace AVM.ClientGenerator.Clients
{
    internal class Code
    {
        private List<string> opening = new List<string>();
        private List<string> closing = new List<string>();
        private List<Code> children=new List<Code>();
        private int indent;

        internal Code(int indent)
        {
            this.indent=indent;
        }

        internal void AddOpeningLine(string opening)
        {
            this.opening.Add(opening);
        }

        internal void AddClosingLine(string closing)
        {
            this.closing.Add(closing);
        }

        internal Code AddChild()
        {
            var child=new Code(indent+1);
            children.Add(child);
            return child;
        }

        override public string ToString()
        {
            string indentPrefix = new String('\t', indent);
            StringBuilder stringBuilder=new StringBuilder();
            
            foreach(string line in opening)
            {
                stringBuilder.Append(indentPrefix);
                stringBuilder.AppendLine(line);
            }

            foreach (var child in children)
            {
                stringBuilder.AppendLine(child.ToString());
            }
            
            foreach (string line in closing)
            {
                stringBuilder.Append(indentPrefix);
                stringBuilder.AppendLine(line);
            }

            return stringBuilder.ToString();
        }


    }
}
