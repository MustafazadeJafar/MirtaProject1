namespace Server01.Models
{
    public static class ExtensionsEO
    {
        public static List<(string html, string directory)> GetAllTemplates()
        {
            List<(string, string)> htmls = new List<(string, string)>();
            string[] folderNames = Directory.GetDirectories(Path.Combine(Directory.GetCurrentDirectory(), "_templates"));
            foreach (string folderName in folderNames)
            {
                string[] fileNames = Directory.GetFiles(folderName);
                foreach (string fileName in fileNames)
                {
                    if (fileName.EndsWith(".html"))
                    {
                        using (StreamReader sr = new StreamReader(fileName))
                        {
                            htmls.Add((sr.ReadToEnd(), Path.Combine(folderName, fileName)));
                            sr.Close();
                        }
                    }
                }
            }
            return htmls;
        }

        public static string HtmlToCshtml(this string html)
        {
            string cshtml = "";

            for (int i = 0; i < html.Length; i++)
            {
                //if (html[i] == '\n') cshtml += "\r";
                cshtml += html[i];

                // http://.css
                // http://.html

                // g.css -> ~/g.css
                // g.html -> g

                if (cshtml.EndsWith("href=") || cshtml.EndsWith("src="))
                {
                    cshtml += html[++i];
                    string temp = "" + html[++i];

                    while (temp[^1] != cshtml[^1])
                    {
                        temp += html[++i];
                    }

                    if (!temp.Contains("://"))
                    {
                        if (temp.EndsWith(".html")) temp = temp.Substring(0, temp.Length - 5);
                        else temp = "~/" + temp;
                    }

                    cshtml += temp;
                }
            }

            return cshtml;
        }

        public static void SaveTemplate(this (string html, string directory) template)
        {

        }

    }
}
