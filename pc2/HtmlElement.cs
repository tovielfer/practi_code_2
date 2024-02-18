//1
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace pc2;
    internal class HtmlElement
{
    public string Id { get; set; }
    public string Name { get; set; }
    public List<string> Attributes { get; set; }
    public List<string> Classes { get; set; }
    public string InnerHtml { get; set; }
    public HtmlElement Parent { get; set; }
    public List<HtmlElement> Children { get; set; }
    public HtmlElement()
    {    
        Attributes = new List<string>();
        Classes = new List<string>();
        Children=new List<HtmlElement>();
    }
    public override string ToString()
    {
        string s = "";
        if (Name != null) s += "Name: " + Name;
        if (Id != null) s += " Id: " + Id;
        if (Classes.Count > 0)
        {
            s += " Classes: ";
            foreach (var c in Classes)
                s += c + " ";
        }
        return s;
    }
    public IEnumerable<HtmlElement> Descendants()
    {
        Queue<HtmlElement> q = new Queue<HtmlElement>();
        q.Enqueue(this);
        while (q.Count > 0)
        {
            HtmlElement el = q.Dequeue();
            if (this != el)
                yield return el;
            foreach (HtmlElement el2 in el.Children) { q.Enqueue(el2); }
        }
    }
    public IEnumerable<HtmlElement> Ancestors()
    {
        HtmlElement c = this;
        while (c != null)
        {
            yield return c;
            c = c.Parent;
        }
    }
}