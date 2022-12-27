using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestI18nDemo.Pages
{
    public partial class Index
    {
        private void SetEnlish()
        {
            I18n.SetCulture(new CultureInfo("en-US"));
        }
        private void SetChinese()
        {
            I18n.SetCulture(new CultureInfo("zh-CN"));
        }
    }
}
