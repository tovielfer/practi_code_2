//4
using pc2;
using System.Text.RegularExpressions;

async Task<string> Load(string url)
{
    HttpClient client = new HttpClient();
    var response = await client.GetAsync(url);
    var html = await response.Content.ReadAsStringAsync();
    return html;
}


var html =
    await Load
    //("<html><div id=\"lk\" class=\"cd vb\"><a id=\"gh\"/></div></html>");
 ("https://mail.google.com/mail/u/0/#inbox");

html = new Regex("[\\r\\n\\t]").Replace(new Regex("\\s{2,}").Replace(html, ""), "");
List<string> htmlLines = new Regex("<(.*?)>").Split(html).Where(s => s.Length > 0).ToList();

HtmlElement root = new HtmlElement();
HtmlElement currentHtmlElement = root;
foreach (string line in htmlLines)
{
    string[] splits = line.Split(' ');
    if (splits[0] == "/html")
    {
        root = root.Children[0];
        root.Parent = null;
        break;
    }
    if (splits[0].StartsWith('/'))
        currentHtmlElement = currentHtmlElement.Parent;
    else
    {
        if (HtmlHelper.Instance.HtmlTags.Contains(splits[0]))
        {
            string attrib = "";
            if (line.IndexOf(' ') > 0)
                attrib = line.Substring(line.IndexOf(' ') , line.Length - line.IndexOf(' '));
            HtmlElement child = new HtmlElement();
            currentHtmlElement.Children.Add(child);
            child.Parent = currentHtmlElement;
            MatchCollection matches = Regex.Matches(attrib, "(.*?)=\"(.*?)\"");
            foreach (Match match in matches)
            {
                string attributeName = match.Groups[1].Value;
                string attributeValue = match.Groups[2].Value;

                if (attributeName == "id"||attributeName==" id")
                    child.Id = attributeValue;
                else if (attributeName == "class"||attributeName==" class")
                    child.Classes = attributeValue.Split(' ').ToList();
                else
                    child.Attributes.Add($"{attributeName}=\"{attributeValue}\"");
            }
            child.Name = splits[0];
            if (!(HtmlHelper.Instance.HtmlVoidTags.Contains(splits[0]) || line.EndsWith('/')))
                currentHtmlElement = child;
        }
        else
            currentHtmlElement.InnerHtml = line;
    }
}
Console.WriteLine("HTML Tree:");
PrintHtmlTree(root, "");
Selector selector = Selector.ParseToSelector("form div div.YhhY8");
IEnumerable<HtmlElement> matchingElements = root.FindElementsBySelector(selector);
foreach (var item in matchingElements.ToList())
{
    Console.WriteLine(item.Name+"lllllllll");
}
static void PrintHtmlTree(HtmlElement element, string indentation)
{
    Console.WriteLine($"{indentation}{element}");
    foreach (var child in element.Children)
        PrintHtmlTree(child, indentation + "  ");
}
