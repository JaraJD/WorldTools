using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorldTools.Domain.Entities;

namespace WorldTools.Domain.Ports
{
    public interface IPublishEventRepository
    {
        void PublishRegisterBranchEvent(StoredEvent eventToPublished);
        void PublishAddProduct(StoredEvent eventToPublished);
        void PublishRegisterProductSaleCustomer(StoredEvent eventToPublished);
        void PublishRegisterProductSaleReseller(StoredEvent eventToPublished);
        void PublishRegisterProductStock(StoredEvent eventToPublished);
        void PublishRegisterUser(StoredEvent eventToPublished);

    }
}
