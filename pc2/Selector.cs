//5
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pc2
{
    internal class Selector
    {
        public string TagName { get; set; }
        public string Id { get; set; }
        public List<string> Classes { get; set; }
        public Selector Child { get; set; }
        public Selector Parent { get; set; }
        public Selector()
        {
            Classes= new List<string>();
        }
        public static Selector ParseToSelector(string text)
        {
            Selector root = new Selector();
            Selector current = root;
            string[] bySpace = text.Split(' ');
            foreach (string s in bySpace)
            {
                current.Child = new Selector();
                current.Child.Parent = current;
                current = current.Child;
                string tagName = s.Substring(0, Math.Min(s.IndexOf('.') > 0 ? s.IndexOf('.') : s.Length, s.IndexOf('#') > 0 ? s.IndexOf('#') : s.Length));
                if (HtmlHelper.Instance.HtmlTags.Contains(tagName))
                    current.TagName = tagName;
                string str;
                for (int i = 0; i < s.Length; i++)
                {
                    str = "";
                    if (s[i] == '.')
                    {
                        i++;
                        while (i < s.Length && s[i] != '.' && s[i] != '#')
                            str += s[i++];
                        current.Classes.Add(str);
                        str = "";
                        i--;
                    }
                    if (s[i] == '#')
                    {
                        i++;
                        while (i < s.Length && s[i] != '.')
                            str += s[i++];
                        current.Id = str;
                        i--;
                        str = "";
                    }
                }
            }
            root = root.Child;
            root.Parent = null;
            return root; 
        }
    }
}
