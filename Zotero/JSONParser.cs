using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Text.RegularExpressions;

namespace Zotero
{
    public class JSONNode
    {
        public const string SPLIT_INNER_OBJECTS_REGEX_QUERY = @"^\[(?:[\s\n]*\{([\s\S]*?\}*)\}\,*[\s\n]*)*\]$";
        public static Regex SPLIT_INNER_ONJECTS = new Regex(SPLIT_INNER_OBJECTS_REGEX_QUERY);

        public JSONNode(string innerJSON)
        {
            this.innerJSON = innerJSON;
        }

        public List<JSONNode> InnerNodes
        {
            get
            {
                List<JSONNode> innerNodes = new List<JSONNode>();

                int balancingBracketsStatus = -1;
                int substringStart = 0;

                for (int i = 0; i < this.innerJSON.Length; i++)
                {
                    switch (this.innerJSON[i])
                    {
                        case '{':
                            balancingBracketsStatus++;
                            break;
                        case '}':
                            balancingBracketsStatus--;
                            break;
                    }

                    if (balancingBracketsStatus == 0)
                    {
                        substringStart = i + 1;
                        innerNodes.Add(new JSONNode(this.innerJSON.Substring(substringStart, i)));
                    }
                }

                CaptureCollection innerJSONNodesCaptures = SPLIT_INNER_ONJECTS.Match(this.innerJSON).Captures;
                foreach (Match innerJSONNodeCapture in innerJSONNodesCaptures)
                    innerNodes.Add(new JSONNode(innerJSONNodeCapture.Value));

                return innerNodes;
            }
        }

        private string innerJSON;
        public string InnerJSON { get { return this.innerJSON; } }
    }

    public class JSONAttribute : JSONNode
    {
        public JSONAttribute(string innerJSON) : base(innerJSON) { }
    }
}