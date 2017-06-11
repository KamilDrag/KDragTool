using KDragTool.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace KDragTool
{
    static class Diff
    {
        internal static IEnumerable<TextLine> MakeUnorderedLinesDiff(string firstPath, string secondPath)
        {
            var firstFile = File.ReadAllLines(firstPath).ToList();
            var firstSet = new HashSet<string>(firstFile);
            var secondSet = new HashSet<string>(File.ReadAllLines(secondPath));

            firstSet.ExceptWith(secondSet);

            return firstSet.Select(line => new TextLine()
            {
                LineNumber = firstFile.IndexOf(line).ToString(),
                LineContent = line
            });
        }
    }
}
