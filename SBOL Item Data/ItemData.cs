using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace SBOL_Item_Data
{
    public class ItemData
    {
        public DataSet CreateDataStructure()
        {
            DataSet dsItemDataStructure = new DataSet("ItemDataStructure");
            DataTable dtItemData = new DataTable("ItemData");

            dtItemData.Columns.Add("Item ID", System.Type.GetType("System.UInt16"));
            dtItemData.Columns.Add("_ItemType", System.Type.GetType("System.SByte"));
            dtItemData.Columns.Add("_CarClass", System.Type.GetType("System.SByte"));
            dtItemData.Columns.Add("_Manufacturer", System.Type.GetType("System.SByte"));
            dtItemData.Columns.Add("unknown1", System.Type.GetType("System.SByte"));
            dtItemData.Columns.Add("_CarID", System.Type.GetType("System.Int16"));
            dtItemData.Columns.Add("Category", System.Type.GetType("System.SByte"));
            dtItemData.Columns.Add("Type", System.Type.GetType("System.SByte"));
            dtItemData.Columns.Add("Type Value", System.Type.GetType("System.SByte"));
            dtItemData.Columns.Add("Item Boost", System.Type.GetType("System.SByte"));
            dtItemData.Columns.Add("Resale Value", System.Type.GetType("System.Int16"));
            dtItemData.Columns.Add("Sale Value", System.Type.GetType("System.Int16"));
            dtItemData.Columns.Add("unknown2", System.Type.GetType("System.Int16"));

            dtItemData.Columns["Item ID"].DefaultValue = 0;
            dtItemData.Columns["_ItemType"].DefaultValue = 0;
            dtItemData.Columns["_CarClass"].DefaultValue = -1;
            dtItemData.Columns["_Manufacturer"].DefaultValue = -1;
            dtItemData.Columns["unknown1"].DefaultValue = 0;
            dtItemData.Columns["_CarID"].DefaultValue = -1;
            dtItemData.Columns["Category"].DefaultValue = -1;
            dtItemData.Columns["Type"].DefaultValue = -1;
            dtItemData.Columns["Type Value"].DefaultValue = -1;
            dtItemData.Columns["Item Boost"].DefaultValue = -1;
            dtItemData.Columns["Resale Value"].DefaultValue = -1;
            dtItemData.Columns["Sale Value"].DefaultValue = -1;
            dtItemData.Columns["unknown2"].DefaultValue = 0;
            dsItemDataStructure.Tables.Add(dtItemData);

            return dsItemDataStructure;
        }
    }
}
