using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ShopOnline.App_Data;
using ShopOnline.Constants;

namespace ShopOnline.Service
{
    public class LocationService : BaseService
    {
        public List<Location> GetCities()
        {
            var locations = Context.Locations.Where(x => x.LocationLevel == 1).OrderByDescending(x=>x.LocationName);
            return locations.ToList();
        }
        public List<Location> GetDistricts(int locationId)
        {
            var locations =
                Context.Locations.Where(x => x.LocationLevel == 2 && x.ParentId == locationId).OrderByDescending(
                    x => x.LocationName);
            return locations.ToList();
        }
    }
}