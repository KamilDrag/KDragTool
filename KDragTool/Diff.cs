using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace KDragTool
{
    static class Diff
    {
        internal static string MakeUnorderedLinesDiff(string firstPath, string secondPath)
        {
            var firstFile = new HashSet<string>(File.ReadAllLines(firstPath));
            var secondFile = new HashSet<string>(File.ReadAllLines(secondPath));

            firstFile.ExceptWith(secondFile);
            return string.Join(Environment.NewLine, firstFile);
        }
    }
}
