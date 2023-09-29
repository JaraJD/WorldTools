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
            HandTools = 1,
            PowerTools = 2,
            Locksmithing = 3,
            ConstructionHardware = 4,
            PaintAndAccessories = 5,
            GardeningAndOutdoors= 6,
            SafetyAndProtectiveEquipment = 7,
            PlumbingSupplies = 8,
            Electrical = 9,
            HomeFixtures = 10,
            Others = 11
        }
    }
}
