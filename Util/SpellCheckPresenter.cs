using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static AzureSpellCheckDemo.Util.BingSpellCheck;

namespace AzureSpellCheckDemo.Util
{
    public static class SpellCheckPresenter
    {

        public static string? BuildHtml(string? submittedText, SpellCheckResponse? spellCheckResponse)
        {
            if (spellCheckResponse == null || string.IsNullOrWhiteSpace(submittedText))
            {
                return null;
            }

            var html = new StringBuilder();           
            html.AppendLine(submittedText);
            int offSetErrorMarkers = 0;
            
            if (spellCheckResponse?.FlaggedTokens?.Any(f => f?.Type?.ToLower() == "unknowntoken") == true)
            {
                foreach (var spellCheck in spellCheckResponse.FlaggedTokens.Where(f => f.Type.ToLower() == "unknowntoken"))
                {
                    string errorMarker = "<span class='error'>";
                    string errorMarkerEnd = "</span>";
                    html.Insert(spellCheck.Offset + offSetErrorMarkers, errorMarker);
                    offSetErrorMarkers += errorMarker.Length;
                    html.Insert(spellCheck.Offset + spellCheck.Token.Length + offSetErrorMarkers, errorMarkerEnd);
                    offSetErrorMarkers += errorMarkerEnd.Length;

                }


            }


            return html.ToString();




        }


    }
}
