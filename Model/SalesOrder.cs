using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;

namespace XuatThuy.Model
{
    public class SalesOrder : ViewModelBase
    {
        long _ID;
        long? _ORDER_ID;
        double? _ORDER_QUANTITY;
        int? _INVENTORY_ITEM_ID;
        int? _TRANSPORT_METHOD_ID;
        int? _CUSTOMER_ID;
        int? _AREA_ID;
        string _MacSP;
        string _UOM_CODE;
        string _DELIVERY_CODE;
        string _DRIVER_NAME;
        string _VEHICLE_CODE;
        string _STATUS;

        public long ID { get => _ID; set => _ID = value; }
        public long? ORDER_ID { get => _ORDER_ID; set => _ORDER_ID = value; }

        public double? ORDER_QUANTITY
        {
            get { return _ORDER_QUANTITY; }
            set
            {
                if (_ORDER_QUANTITY != value)
                {
                    _ORDER_QUANTITY = value;
                    RaisePropertyChanged("ORDER_QUANTITY");
                }
            }
        }

        public string UOM_CODE
        {
            get { return _UOM_CODE; }
            set
            {
                if (_UOM_CODE != value)
                {
                    _UOM_CODE = value;
                    RaisePropertyChanged("UOM_CODE");
                }
            }
        }

        public string MacSP
        {
            get { return _MacSP; }
            set
            {
                if (_MacSP != value)
                {
                    _MacSP = value;
                    RaisePropertyChanged("MacSP");
                }
            }
        }

        public int? CUSTOMER_ID
        {
            get { return _CUSTOMER_ID; }
            set
            {
                if (_CUSTOMER_ID != value)
                {
                    _CUSTOMER_ID = value;
                    RaisePropertyChanged("CUSTOMER_ID");
                }
            }
        }

        public int? AREA_ID
        {
            get { return _AREA_ID; }
            set
            {
                if (_AREA_ID != value)
                {
                    _AREA_ID = value;
                    RaisePropertyChanged("AREA_ID");
                }
            }
        }

        public int? INVENTORY_ITEM_ID
        {
            get { return _INVENTORY_ITEM_ID; }
            set
            {
                if (_INVENTORY_ITEM_ID != value)
                {
                    _INVENTORY_ITEM_ID = value;
                    RaisePropertyChanged("INVENTORY_ITEM_ID");
                }
            }
        }

        public int? TRANSPORT_METHOD_ID
        {
            get { return _TRANSPORT_METHOD_ID; }
            set
            {

                    _TRANSPORT_METHOD_ID = value;
                    RaisePropertyChanged("TRANSPORT_METHOD_ID");

            }
        }

        public string DELIVERY_CODE
        {
            get { return _DELIVERY_CODE; }
            set
            {
                if (_DELIVERY_CODE != value)
                {
                    _DELIVERY_CODE = value;
                    RaisePropertyChanged("DELIVERY_CODE");
                }
            }
        }

        public string VEHICLE_CODE
        {
            get { return _VEHICLE_CODE; }
            set
            {
                if (_VEHICLE_CODE != value)
                {
                    _VEHICLE_CODE = value;
                    RaisePropertyChanged("VEHICLE_CODE");
                }
            }
        }

        public string DRIVER_NAME
        {
            get { return _DRIVER_NAME; }
            set
            {
                if (_DRIVER_NAME != value)
                {
                    _DRIVER_NAME = value;
                    RaisePropertyChanged("DRIVER_NAME");
                }
            }
        }

        public string STATUS
        {
            get { return _STATUS; }
            set
            {
                if (_STATUS != value)
                {
                    _STATUS = value;
                    RaisePropertyChanged("STATUS");
                }
            }
        }

        public static SalesOrder GetEmptySalesOrder()
        {
            return new SalesOrder(0, 0, 0, string.Empty, string.Empty, 0, 0, 0, 0, string.Empty, string.Empty, string.Empty, string.Empty);
        }

        public static SalesOrder GetSalesOrderByID(Guid? SessionID, long? OrderID)
        {
            XuatHangDataContext db = new XuatHangDataContext();
            SalesOrder result = null;

            var so = db.p_SALES_ORDERS_LayTheoID(SessionID, OrderID).FirstOrDefault();

            if (so != null)
            {
                result = new SalesOrder(so.ID, so.ORDER_ID, so.ORDER_QUANTITY, so.UOM_CODE, so.MacSP, so.CUSTOMER_ID, so.AREA_ID,
                                        so.INVENTORY_ITEM_ID, so.TRANSPORT_METHOD_ID, so.DELIVERY_CODE, so.VEHICLE_CODE, so.DRIVER_NAME, so.STATUS);
            }

            return result;
        }

        public bool GetSalesOrderByCode(Guid? SessionID, string DELIVERY_CODE)
        {
            XuatHangDataContext db = new XuatHangDataContext();

            var salesOrder = db.p_SALES_ORDERS_Lay(SessionID, DELIVERY_CODE).FirstOrDefault();

            if (salesOrder != null)
            {
                ID = salesOrder.ID;
                ORDER_ID = salesOrder.ORDER_ID;
                ORDER_QUANTITY = salesOrder.ORDER_QUANTITY;
                INVENTORY_ITEM_ID = salesOrder.INVENTORY_ITEM_ID;
                TRANSPORT_METHOD_ID = salesOrder.TRANSPORT_METHOD_ID;
                CUSTOMER_ID = salesOrder.CUSTOMER_ID;
                AREA_ID = salesOrder.AREA_ID;
                MacSP = salesOrder.MacSP;
                UOM_CODE = salesOrder.UOM_CODE;
                DELIVERY_CODE = salesOrder.DELIVERY_CODE;
                DRIVER_NAME = salesOrder.DRIVER_NAME;
                VEHICLE_CODE = salesOrder.VEHICLE_CODE;

                return true;
            }

            return false;
        }

        public SalesOrder(long iD, long? oRDER_ID, double? oRDER_QUANTITY, string uOM_CODE, string macSP, int? cUSTOMER_ID, int? aREA_ID, int? iNVENTORY_ITEM_ID, int? tRANSPORT_METHOD_ID, string dELIVERY_CODE, string vEHICLE_CODE, string dRIVER_NAME, string sTATUS)
        {
            ID = iD;
            ORDER_ID = oRDER_ID;
            ORDER_QUANTITY = oRDER_QUANTITY;
            UOM_CODE = uOM_CODE;
            MacSP = macSP;
            CUSTOMER_ID = cUSTOMER_ID;
            AREA_ID = aREA_ID;
            INVENTORY_ITEM_ID = iNVENTORY_ITEM_ID;
            TRANSPORT_METHOD_ID = tRANSPORT_METHOD_ID;
            DELIVERY_CODE = dELIVERY_CODE;
            VEHICLE_CODE = vEHICLE_CODE;
            DRIVER_NAME = dRIVER_NAME;
            DRIVER_NAME = dRIVER_NAME;
            STATUS = sTATUS;
        }

        public void Init()
        {
            ID = 0;
            ORDER_ID = 0;
            ORDER_QUANTITY = 0;
            INVENTORY_ITEM_ID = 0;
            TRANSPORT_METHOD_ID = 0;
            CUSTOMER_ID = 0;
            AREA_ID = 0;
            MacSP = null;
            UOM_CODE = null;
            DELIVERY_CODE = null;
            DRIVER_NAME = null;
            VEHICLE_CODE = null;
        }
    }
}
