using Core.DomainService;
using Services.DataServices.SuperAdmin;
using Services.Domain.Admin;
using Services.Domain.SuperAdmin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.DomainServices.SuperAdmin
{
    public class SalesDomainService : DomainService<PRODUCT_ORDERS, long>
    {
        private readonly SalesDataService _salesDataService;
        public SalesDomainService(SalesDataService salesDataService) : base(salesDataService)
            {
            _salesDataService = salesDataService;
        }

        public IList<Admin_Get_OrderList> GetAllOrderListForAdmin()
        {
            return _salesDataService.GetAllOrderListForAdmin();
        }

        public OrderDetailsData GetOrderDetailsByOrderId(int id)
        {
            return _salesDataService.GetOrderDetailsByOrderId(id);
        }

        public int UpdateOrderStatusCanceled(string orderId, int OrderStatusTypeId)
        {
            return _salesDataService.UpdateOrderStatusCanceled(orderId, OrderStatusTypeId);
        }
        public int DeleteOrderByOrderId(string orderId)
        {
            return _salesDataService.DeleteOrderByOrderId(orderId);
        }
        public IList<OrderStatus> GetProductOrderStatusType()
        {
            return _salesDataService.GetProductOrderStatusType();
        }

    }
}
