//using System.Security.Claims;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;

//namespace iShop.Common.Extensions
//{
//    public static class ApiControllerExtensions
//    {
//        // pretend a User
//        // a helper method for testing purposes
//        public static void MockUser(this Microsoft.AspNetCore.Mvc.Controller controller, string userId, string userName)
//        {
//            var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
//            {
//                new Claim(ClaimTypes.NameIdentifier, userId),
//                new Claim(ClaimTypes.Name, userName)
//            }));
//            controller.ControllerContext = new ControllerContext()
//            {
//                HttpContext = new DefaultHttpContext() { User = user }
//            };
//        }
//    }
//}
