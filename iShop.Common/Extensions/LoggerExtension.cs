//using System;
//using iShop.Common.Helpers;

//namespace iShop.Common.Extensions
//{
//    public static class LoggerExtension
//    {
//        public static void LogMessage(this ILogger logger, int code, string itemName, Guid itemId)
//        {
//            switch (code)
//            {
//                case LoggingEvents.SavedFail:
//                    logger.LogError(LoggingEvents.Fail, itemName + " with id " + itemId + " failed to saved");
//                    break;
//                case LoggingEvents.Created:
//                    logger.LogInformation(LoggingEvents.Created, itemName + " with id " + itemId + " has been created");
//                    break;
//                case LoggingEvents.Deleted:
//                    logger.LogError(LoggingEvents.Deleted, itemName + " with id " + itemId + " has been deleted");
//                    break;
//                case LoggingEvents.Updated:
//                    logger.LogInformation(LoggingEvents.Updated, itemName + " with id " + itemId + " has been updated");
//                    break;
//                default: 
//                    throw new ArgumentException("code is invalid");
//            }
//        }
//    }
//}
