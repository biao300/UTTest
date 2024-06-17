using Newtonsoft.Json;

namespace UTCSharpTest.Helper
{
    public static class Utility
    {
        public static void WriteToFile(string fileName, string line)
        {
            FileInfo fi = new FileInfo(fileName);

            try
            {
                if (fi.Exists)
                {
                    using (StreamWriter sw = fi.AppendText())
                    {
                        sw.WriteLine(line);
                        sw.Close();
                    }
                }
                else
                {
                    using (StreamWriter sw = fi.CreateText())
                    {
                        sw.WriteLine(line);
                        sw.Close();
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        public static List<string> ProcessLine(string text, char separator)
        {
            bool isquotedString = false;
            int startIndex = 0;
            int endIndex = 0;
            var cols = new List<string>();
            foreach (char c in text)
            {
                //in a correctly formatted csv file,
                //when a separator occurs inside a quoted string 
                //the number of '"' is always an odd number
                if (c == '"')
                {
                    //toggle isquotedString
                    isquotedString = !isquotedString;
                }
                //ignore separator if embedded within a quoted string
                if (c == separator && !isquotedString)
                {
                    //this will add all cols except the last
                    cols.Add(trimQuotes(text.Substring(startIndex, endIndex - startIndex)));
                    startIndex = endIndex + 1;
                }

                endIndex++;
            }

            //reached the last column so trim and add it
            cols.Add(trimQuotes(text.Substring(startIndex, endIndex - startIndex)));
            return cols;
        }

        public static List<List<string>> ReadText(string text)
        {
            var rows = new List<List<string>>();

            string[] lines = text.Split("\r\n");

            int line_index = 0;

            foreach (string line in lines)
            {
                var cols = ProcessLine(line, ',');
                rows.Add(cols);

                line_index++;
            }

            return rows;
        }

        private static string trimQuotes(string text)
        {
            if (string.IsNullOrEmpty(text))
            {
                return text;
            }
            return text[0] == '"' && text.Length > 2
                       //trim enclosing '"' and replace any embedded '""' with '"' 
                       ? text.Substring(1, text.Length - 2).Replace("\"\"", "\"")
                       //replace any embedded '""' with '"' 
                       : text.Replace("\"\"", "\"");
        }
    }
}
