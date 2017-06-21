using mecode.toolkit;
using mecode.toolkit.Entites;
using System;
using System.Web.Mvc;

namespace upgrade.tests.Controllers
{
    public class UpgradeController : Controller
    {
        // GET: Upgrade
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult StartUpgrade(UpgradeInfo info = null)
        {
            var response = "站点新增成功";
            try
            {
                if (info == null)
                    throw new ArgumentNullException("参数 info is null");
                Logger.Info("升级信息: " + JsonHelper.ObjectToJSON(info));
                new AutoUpgradeManager().Run(info);
            }
            catch (Exception ex)
            {
                response = ex.ToString();
                Logger.Error(ex.Message, ex);
            }
            return Content(response);
        }
    }
}