using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ShopOnline.App_Data;

namespace ShopOnline.Service
{
    public class ColorService : BaseService
    {
        public List<Color>  GetColors()
        {
            return Context.Colors.OrderByDescending(x=>x.UpdatedDate).ThenByDescending(x=>x.CreatedDate).ToList();
        }
        public bool CreateColor(string name,string value)
        {
            var color = new Color();
            color.ColorName = name;
            color.ColorValue = value;
            color.CreatedDate = DateTime.Now;
            color.UpdatedDate = DateTime.Now;
            Context.Colors.Add(color);
            Context.SaveChanges();
            return color.ColorId > 0;
        }
        public bool UpdateColor(int colorId,string name,string value)
        {
            var color = Context.Colors.FirstOrDefault(x => x.ColorId == colorId);
            var result = 0;
            if (color != null)
            {
                color.ColorName = name;
                color.ColorValue = value;
                color.UpdatedDate = DateTime.Now;
                result = Context.SaveChanges();
            }
            return color != null && result > 0;
        }
        public bool DeleteColor(int id)
        {
            var result = 0;
            var color = Context.Colors.FirstOrDefault(x => x.ColorId == id);
            if (color != null)
            {
                Context.Colors.Remove(color);
                result = Context.SaveChanges();
            }
            return result > 0;
        }
    }
}