using System;
using System.Collections.Generic;
using System.Reflection;
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

        public void CheckDataType(Type type, PropertyInfo prop, object instance, string data)
        {
            if (typeof(int?).IsAssignableFrom(prop.PropertyType))
            {
                prop.SetValue(instance, data.ToInt32(), null);
            }
            else if (typeof(decimal?).IsAssignableFrom(prop.PropertyType))
            {
                prop.SetValue(instance, data.ToDecimal(), null);
            }
            else
            {
                prop.SetValue(instance, data, null);
            }
        }

        public void Dispose()
        {
        }
    }
}
