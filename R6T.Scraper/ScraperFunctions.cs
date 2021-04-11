using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;

namespace R6T.Scraper
{
    public class ScraperFunctions : IDisposable
    {
        public async Task<bool> MonkeyPatchInterval(IWebDriver browser)
        {
            try
            {
                IJavaScriptExecutor executor = (IJavaScriptExecutor)browser;

                string monkeyPatchScript =
                    "window.profileApp.initRefresh = function () {var self = this; console.log('Monkey Patched'); clearInterval(self.refreshIntervalHandle);}";
                string executeScript = "window.profileApp.initRefresh();";

                executor.ExecuteScript(monkeyPatchScript);

                await Task.Run(() =>
                {
                    try
                    {
                        return executor.ExecuteAsyncScript(executeScript);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                        return false;
                    }
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return false;
            }
            return true;
        }

        public void Dispose()
        {
        }
    }
}
