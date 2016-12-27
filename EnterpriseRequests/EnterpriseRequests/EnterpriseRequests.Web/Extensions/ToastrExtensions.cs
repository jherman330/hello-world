using System.Web.Mvc;

namespace EnterpriseRequests.Web.Extensions
{
    public static class ToastrExtensions
    {
        public static void AddSuccessMessage(this Controller controller, string message)
        {
            controller.TempData.Add(ToastrKeys.ToastrSuccess.ToString(), message);
        }

        public static void AddInfoMessage(this Controller controller, string message)
        {
            controller.TempData.Add(ToastrKeys.ToastrInfo.ToString(), message);
        }

        public static void AddErrorMessage(this Controller controller, string message)
        {
            controller.TempData.Add(ToastrKeys.ToastrError.ToString(), message);
        }

        public static string GetSuccessMessage(this TempDataDictionary tempData)
        {
            return (string)tempData[ToastrKeys.ToastrSuccess.ToString()];
        }

        public static string GetInfoMessage(this TempDataDictionary tempData)
        {
            return (string)tempData[ToastrKeys.ToastrInfo.ToString()];
        }

        public static string GetErrorMessage(this TempDataDictionary tempData)
        {
            return (string)tempData[ToastrKeys.ToastrError.ToString()];
        }

        private enum ToastrKeys
        {
            ToastrSuccess,
            ToastrInfo,
            ToastrError
        }
    }
}