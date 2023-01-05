using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TemplatePattern.Web.UserCards
{
    public class PrimeUserCardTemplate : UserCardTemplate
    {
        protected override string SetFooter()
        {
            var sb = new StringBuilder();
            sb.Append("<a href='#' class='card-link'>Mesaj GÖnder</a>");
            sb.Append("<a href='#' class='card-link'>Profil Gör</a>");

            return sb.ToString();
        }

        protected override string SetPicture()
        {
            return $"<img src='{AppUser.PictureUrl}' class='card-img-top'>";
        }
    }
}
