using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using ShopOnline.App_Data;
using ShopOnline.Constants;

namespace ShopOnline.Service
{
    public class ConfigService : BaseService
    {
        
        public bool ChangeLogo(HttpPostedFileBase file)
        {
            var result = true;

            var config = Context.Configs.FirstOrDefault(x => x.ConfigCode == Common.Logo);
            if (config != null)
            {
                //remove old file
                var oldPath = HttpContext.Current.Server.MapPath(config.ConfigValue);
                if (File.Exists(oldPath))
                {
                    File.Delete(oldPath);
                }
                var fName = file.FileName;
                var fNameIndex = fName.LastIndexOf('.');
                fName = fName.Insert(fNameIndex, "_" + DateTime.Now.Ticks.ToString());
                var folderPath = HttpContext.Current.Server.MapPath("~/images/logo");
                string filePath = Path.Combine(folderPath, fName);
                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }
                System.IO.File.WriteAllBytes(filePath, this.ReadData(file.InputStream));

                config.ConfigValue = "~/Images/Logo/" + fName;
                result = Context.SaveChanges() > 0;
            }
            return result;

        }
        public Config GetConfig(string code)
        {
            var config = new Config();
            var configDb=Context.Configs.FirstOrDefault(x => x.ConfigCode == code);
            if(configDb!=null)
            {
                config = configDb;
            }
            return config;
        }
        public bool SaveConfig(string code,string value)
        {
            var result = 0;
            var config = Context.Configs.FirstOrDefault(x => x.ConfigCode == code);
            if (config != null)
            {
                config.ConfigValue = value;
                result = Context.SaveChanges();
            }
            return config != null && result>0;
        }
    }
}