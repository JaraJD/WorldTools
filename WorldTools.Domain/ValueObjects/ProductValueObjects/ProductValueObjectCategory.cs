using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorldTools.Domain.ValueObjects.ProductValueObjects
{
    public class ProductValueObjectCategory
    {
        public enum Category
        {
            HandTools,
            PowerTools,
            Locksmithing,
            ConstructionHardware,
            PaintAndAccessories,
            GardeningAndOutdoors,
            SafetyAndProtectiveEquipment,
            PlumbingSupplies,
            Electrical,
            HomeFixtures,
            Others
        }
    }
}
