//2
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pc2
{
    internal static class HtmlElementExtension
    {
        public static IEnumerable<HtmlElement> FindElementsBySelector(this HtmlElement root, Selector selector)
        {
            HashSet<HtmlElement> result = new HashSet<HtmlElement>();
            foreach (var child in  root.Descendants())
                FindElementsRecursively(child,selector, result);
            return result;
        }

        private static void FindElementsRecursively(HtmlElement currentElement, Selector currentSelector, HashSet<HtmlElement> result)
        {
            if (!IsElementMatch(currentElement,currentSelector))
                return;

            if (currentSelector.Child == null)
                result.Add(currentElement);
            else
                foreach (var child in currentElement.Descendants())
                    FindElementsRecursively(child, currentSelector.Child, result);
        }

        private static bool IsElementMatch(HtmlElement element, Selector selector)
        {
            return (selector.TagName == null || element.Name==null||selector.TagName == element.Name) &&
                   (selector.Id == null ||element.Id==null|| selector.Id == element.Id) &&
                   (selector.Classes.Count == 0 ||element.Classes== null || selector.Classes.Any(element.Classes.Contains));
        }
    }
}
