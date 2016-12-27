using System.Web.Mvc;

namespace EnterpriseRequests.Web.Extensions
{
    public static class HtmlHelperExtensions
    {
        public static MvcHtmlString SubmitLinkAndSpinner(this HtmlHelper htmlHelper, string text, string htmlId)
        {
            const string spinnerDiv = "<div id='spinner-div' class='spinner-div'></div>";
            string script = "<script>" +
                                  "$(\"#" + htmlId + "\").click(function(){" +
                                  "$('body').append(\"" + spinnerDiv + "\");" +
                                  "$('#spinner-div').spin();" +
                                  "$('form').submit();" +
                                  "});" +
                                  "</script>";

            string link = string.Format("<a href=\"#\" id=\"" + htmlId + "\"><i class='fa fa-floppy-o fa-fw'></i> {0}</a>", text);

            return new MvcHtmlString(string.Format("{0}{1}", link, script));
        }

        public static MvcHtmlString DeleteOrDeactivateSpinner(this HtmlHelper htmlHelper, string text, bool deleting, string htmlId)
        {
            const string spinnerDiv = "<div id='spinner-div' class='spinner-div'></div>";
            string hiddenInput = "<input id='IsDeleting' name='IsDeleting' hidden='hidden' value=" + deleting + " />";

            string script = "<script>" +
                            "$(\"#" + htmlId + "\").click(function(){" +
                            "$('form').append(\"" + hiddenInput + "\");" +
                            "$('body').append(\"" + spinnerDiv + "\");" +
                            "$('#spinner-div').spin();" +
                            "$('form').submit();" +
                            "});" +
                            "</script>";

            string link = string.Format("<a href=\"#\" id=\"" + htmlId + "\"><i class='fa {0} fa-fw'></i> {1}</a>",
                deleting ? "fa-trash-o" : "fa-power-off", text);

            return new MvcHtmlString(string.Format("{0}{1}", link, script));
        }
    }
}