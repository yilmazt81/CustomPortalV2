    using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CustomPortalV2.RestApi.Controllers
{
    public class UserRuleMenuController : ControllerBase
    {
      

        // POST: UserRuleMenuController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return Ok();
            }
        }

        // GET: UserRuleMenuController/Edit/5
        public ActionResult Edit(int id)
        {
            return Ok();
        } 
      
    }
}
