using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WXDataModel;

namespace WXDataBLL.Base
{
    public class WX_MediaManager:BaseBLL<WX_Media>
    {

        public WX_Media GetByMediaId(string mediaid,string appid)
        {
            return Where(m => m.AppId.Equals(appid) && m.MediaId.Equals(mediaid)).FirstOrDefault();
        }
    }
}
