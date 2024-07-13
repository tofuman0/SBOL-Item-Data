using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.IO;
using System.Windows.Forms;

namespace SBOL_Item_Data
{
    public class ItemDataManager
    {
        #region Variables
        public DataSet dsItemData = null;
        public List<int> itemDataSizes = new List<int>() { 2, 1, 1, 1, 1, 2, 1, 1, 1, 1, 2, 2, 2 };

        public readonly Dictionary<int, string> carNames = new Dictionary<int, string> {
            { -1, "ALL" },
            { 0, "AE86L3" },
            { 1, "AE86T3" },
            { 2, "AE86L2" },
            { 3, "AE86T2" },
            { 4, "SW20GT" },
            { 5, "SW20G" },
            { 6, "A80RZ" },
            { 7, "A80SZ" },
            { 8, "A80RZM" },
            { 9, "A80SZM" },
            { 10, "XE10RS" },
            { 11, "XE10AS" },
            { 12, "100TV" },
            { 13, "100TVM" },
            { 14, "100MV" },
            { 15, "100MVM" },
            { 16, "S13K" },
            { 17, "S13Q" },
            { 18, "PS13K" },
            { 19, "PS13Q" },
            { 20, "PS13KK" },
            { 21, "S14K" },
            { 22, "S14Q" },
            { 23, "S14KM" },
            { 24, "S14QM" },
            { 25, "S15R" },
            { 26, "S15S" },
            { 27, "RPS13X" },
            { 28, "RPS133" },
            { 29, "R32RV2" },
            { 30, "R32RN" },
            { 31, "R32GTM" },
            { 32, "R33RV" },
            { 33, "R33RVM" },
            { 34, "R34RV" },
            { 35, "R34RVM" },
            { 36, "R34GT" },
            { 37, "R34GTM" },
            { 38, "Z32VS" },
            { 39, "Z32ZX" },
            { 40, "Y33CV" },
            { 41, "Y33GTU" },
            { 42, "EK9" },
            { 43, "EK9M" },
            { 44, "EKC" },
            { 45, "DC2" },
            { 46, "DC2M" },
            { 47, "DB8" },
            { 48, "DB8M" },
            { 49, "NA1" },
            { 50, "NA2" },
            { 51, "CE9A" },
            { 52, "CN9A" },
            { 53, "CP9A5" },
            { 54, "CP9A6" },
            { 55, "CP9A6M" },
            { 56, "FC3E3" },
            { 57, "FC3X" },
            { 58, "FD3RS" },
            { 59, "FD3RZ" },
            { 60, "GC8K" },
            { 61, "GC8S4" },
            { 62, "GC8C4" },
            { 63, "GC8S5" },
            { 64, "GC8C5" },
            { 65, "GC8S6" },
            { 66, "GC8C6" },
            { 67, "GF8W4" },
            { 68, "GF8W5" },
            { 69, "GF8W6" },
            { 70, "AP1" },
            { 71, "AP1HT" },
            { 72, "964T" },
            { 73, "964C2" },
            { 74, "S30Z" },
            { 75, "S30ZG" },
            { 76, "AE111RL" },
            { 77, "AE111RT" },
            { 78, "UZZ30" },
            { 79, "JZZ30" },
            { 80, "NB8RS" },
            { 81, "NB8RSM" },
            { 82, "EG6" },
            { 83, "ST205" },
            { 84, "ST202" },
            { 85, "Z15AM" },
            { 86, "Z15A" },
            { 87, "Z16A" },
            { 88, "DE3A" },
            { 89, "N15N1" },
            { 90, "BE5" },
            { 91, "BH5B" },
            { 92, "T231" },
            { 93, "W30" },
            { 94, "Y34CV" },
            { 95, "Y34GU" },
            { 96, "E36" },
            { 97, "N5S16M" },
            { 98, "N5S16" },
            { 99, "C34M" },
            { 100, "C34" },
            { 101, "S161V" },
            { 102, "S161VM" },
            { 103, "EF8" },
            { 104, "EF8G" },
            { 105, "CH9" },
            { 106, "CL1" },
            { 107, "PP1" },
            { 108, "PG6SA" },
            { 109, "EA11R" },
            { 110, "EA21R" },
            { 111, "JCESE" },
            { 112, "A31C" },
            { 113, "RF2" },
            { 114, "A187" },
            { 115, "RX3" },
            { 116, "KPGC10" },
            { 117, "R30M" },
            { 118, "R30" },
            { 119, "SAGTX" },
            { 120, "VGTS" },
            { 121, "S30ZX" },
            { 122, "CHA" },
            { 123, "CFA" },
            { 124, "D32AGS" },
            { 125, "ECGT" },
            { 126, "BB6S" },
            { 127, "NCP35" },
            { 128, "UCF30B" },
            { 129, "A70GTA" },
            { 130, "A70TTR" },
            { 131, "CT9A" },
            { 132, "TAGDA" },
            { 133, "TASTI" },
            { 134, "CBAEP" },
            { 135, "100TVK" },
            { 136, "AP1HTK" },
            { 137, "JZZ30K" },
            { 138, "Z15AMK" },
            { 139, "NA2K" },
            { 140, "A70GTK" },
            { 141, "R34RKK" },
            { 142, "Y34GUK" },
            { 143, "UZZ30K" },
            { 144, "R34RK" },
            { 145, "Z32VSK" },
            { 146, "A80RZK" },
            { 147, "FD3RK" },
            { 148, "NCP35K" },
            { 149, "S15RK" },
            { 150, "R33RK" },
            { 151, "FD3RKK" },
            { 152, "S13KKK" },
            { 153, "Z32ZXK" },
            { 154, "S14KMK" },
            { 155, "Y33CVK" },
            { 156, "S161VK" },
            { 157, "A80RK" },
            { 158, "FC3E3K" },
            { 159, "Z16AK" },
            { 160, "NA1K" },
            { 161, "PS13XK" },
            { 162, "S15RKK" },
            { 163, "JCESEK" },
            { 164, "R32RNK" },
            { 165, "INVALID" }
        };
        public readonly Dictionary<int, string> classNames = new Dictionary<int, string>
        {
            { -1, "Class ALL" },
            { 0, "Class A" },
            { 1, "Class B" },
            { 2, "Class C" },
            { 3, "INVALID" }
        };
        public readonly Dictionary<int, string> manufacturerNames = new Dictionary<int, string>
        {
            { -1, "ALL" },
            { 0, "Toyota"  },
            { 1, "Nissan" },
            { 2, "Mitsubishi" },
            { 3, "Mazda" },
            { 4, "Subaru" },
            { 5, "Sukuki" },
            { 6, "Honda" },
            { 7, "INVALID" }
        };
        public readonly Dictionary<int, string> itemTypeNames = new Dictionary<int, string>
        {
            { 0, "Car" },
            { 1, "Part" },
            { 2, "Item" },
            { 3, "INVALID" }
        };
        #endregion
        #region Load Data
        public DataSet LoadItemData(string fileName)
        {
            try
            {
                ItemData itemData = new ItemData();
                dsItemData = itemData.CreateDataStructure();

                using (FileStream stream = new FileStream(fileName, FileMode.Open, FileAccess.Read))
                {
                    BinaryReader reader = new BinaryReader(stream);
                    UInt32 tableCount = Convert.ToUInt32(stream.Length / 18);

                    if((tableCount == 0) || (stream.Length % 18) != 0)
                    {
                        stream.Close();
                        return null;
                    }

                    long size = stream.Length;
                    byte[] data = new byte[size];
                    stream.Read(data, 0, (int)size);

                    PopulateTable(data, "itemData", itemDataSizes, (int)size);

                    stream.Close();
                }

                return dsItemData;
            }
            catch(Exception ex)
            {
                MessageBox.Show("Error Loading item data: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }
        public void ClearItemData()
        {
            if (dsItemData != null)
            {
                dsItemData.Clear();
                dsItemData = null;
            }
        }
        #endregion
        #region Save Data
        public void SaveItemData(string fileName, DataSet dsSourceData)
        {
            using (FileStream stream = new FileStream(fileName, FileMode.Create, FileAccess.Write))
            {
                try
                {
                    foreach (DataRow dr in dsSourceData.Tables["ItemData"].Rows)
                    {
                        int fieldSizePosition = 0;
                        List<string> columns = new List<string> { "Item ID", "_ItemType", "_CarClass", "_Manufacturer", "unknown1", "_CarID", "Category", "Type", "Type Value", "Item Boost", "Resale Value", "Sale Value", "unknown2" };
                        byte[] byteValue = null;

                        foreach (string column in columns)
                        {
                            if (dsSourceData.Tables["ItemData"].Columns[column].DataType == typeof(System.SByte))
                            {
                                byteValue = BitConverter.GetBytes(Convert.ToSByte(dr[column]));
                                for (int i = 0; i < Convert.ToInt16(itemDataSizes[fieldSizePosition]); i++)
                                {
                                    stream.WriteByte(byteValue[i]);
                                }
                            }
                            else if (dsSourceData.Tables["ItemData"].Columns[column].DataType == typeof(System.Byte))
                            {
                                byteValue = BitConverter.GetBytes(Convert.ToByte(dr[column]));
                                for (int i = 0; i < Convert.ToInt16(itemDataSizes[fieldSizePosition]); i++)
                                {
                                    stream.WriteByte(byteValue[i]);
                                }
                            }
                            else if (dsSourceData.Tables["ItemData"].Columns[column].DataType == typeof(System.Int16))
                            {
                                byteValue = BitConverter.GetBytes(Convert.ToInt16(dr[column]));
                                for (int i = 0; i < Convert.ToInt16(itemDataSizes[fieldSizePosition]); i++)
                                {
                                    stream.WriteByte(byteValue[i]);
                                }
                            }
                            else if (dsSourceData.Tables["ItemData"].Columns[column].DataType == typeof(System.UInt16))
                            {
                                byteValue = BitConverter.GetBytes(Convert.ToUInt16(dr[column]));
                                for (int i = 0; i < Convert.ToInt16(itemDataSizes[fieldSizePosition]); i++)
                                {
                                    stream.WriteByte(byteValue[i]);
                                }
                            }
                            else if (dsSourceData.Tables["ItemData"].Columns[column].DataType == typeof(System.Int32))
                            {
                                byteValue = BitConverter.GetBytes(Convert.ToInt32(dr[column]));
                                for (int i = 0; i < Convert.ToInt16(itemDataSizes[fieldSizePosition]); i++)
                                {
                                    stream.WriteByte(byteValue[i]);
                                }
                            }
                            else if (dsSourceData.Tables["ItemData"].Columns[column].DataType == typeof(System.UInt32))
                            {
                                byteValue = BitConverter.GetBytes(Convert.ToUInt32(dr[column]));
                                for (int i = 0; i < Convert.ToInt16(itemDataSizes[fieldSizePosition]); i++)
                                {
                                    stream.WriteByte(byteValue[i]);
                                }
                            }
                            fieldSizePosition++;
                        }
                    }
                }
                finally
                {
                    stream.Close();
                }
            }
        }
        #endregion
        #region Data Processing
        private void PopulateTable(byte[] data, string TableName, List<int> fieldSizes, int size)
        {
            DataTable dtTable = dsItemData.Tables[TableName];
            int startColumn = dtTable.Columns.Count - fieldSizes.Count;
            int dataLength = 0;

            //Calculate the total number of bytes for the data to populate the DataTable with
            for (int i = 0; i < fieldSizes.Count; i++)
            {
                dataLength += Convert.ToInt32(fieldSizes[i]);
            }

            for (int j = 0; j < size; j++)
            {
                DataRow newRow = dtTable.NewRow();
                int fieldPos = 0;
                for (int k = 0; k < fieldSizes.Count; k++)
                {
                    newRow[k + startColumn] = GetValue(data, j + fieldPos, Convert.ToInt32(fieldSizes[k]), dtTable.Columns[k + startColumn].DataType);
                    fieldPos += Convert.ToInt32(fieldSizes[k]);
                }
                j += (dataLength - 1);
                dtTable.Rows.Add(newRow);
            }
        }
        private object GetValue(byte[] data, int offset, int length, System.Type dataType)
        {
            object rv = null;

            switch (length)
            {
                case 1:
                    if (dataType == typeof(System.SByte))
                    {
                        rv = (sbyte)data[offset];
                    }
                    else
                        rv = data[offset];
                    break;
                case 2:
                    if (dataType == typeof(System.String))
                    {
                        byte bit1 = data[offset];
                        byte bit2 = data[offset + 1];
                        rv = string.Format("{0:X2}", bit1) + string.Format("{0:X2}", bit2);
                    }
                    else if (dataType == typeof(System.UInt16))
                        rv = BitConverter.ToUInt16(data, offset);
                    else
                        rv = BitConverter.ToInt16(data, offset);
                    break;
                case 3:
                    if (dataType == typeof(System.String))
                    {
                        byte bit1 = data[offset];
                        byte bit2 = data[offset + 1];
                        byte bit3 = data[offset + 2];
                        rv = string.Format("{0:X2}", bit1) + string.Format("{0:X2}", bit2) + string.Format("{0:X2}", bit3);
                    }
                    else
                        rv = ConvertBytesToInt32(data, offset, 3);
                    break;
                case 4:
                    if (dataType == typeof(System.Int32))
                        rv = BitConverter.ToInt32(data, offset);
                    else if (dataType == typeof(System.UInt32))
                        rv = BitConverter.ToUInt32(data, offset);
                    else if (dataType == typeof(System.Single))
                    {
                        if (dataType == typeof(System.Single)) rv = BitConverter.ToSingle(data, offset);
                    }
                    else if (dataType == typeof(System.String))
                    {
                        byte bit1 = data[offset];
                        byte bit2 = data[offset + 1];
                        byte bit3 = data[offset + 2];
                        byte bit4 = data[offset + 3];
                        rv = string.Format("{0:X2}", bit1) + string.Format("{0:X2}", bit2) + string.Format("{0:X2}", bit3) + string.Format("{0:X2}", bit4);
                    }
                    break;
            }
            return rv;
        }
        private Int32 ConvertBytesToInt32(byte[] data, int offset, int length)
        {
            byte[] fieldValue = new byte[length];
            StringBuilder hexValue = new StringBuilder();

            for (int i = 0; i < length; i++)
            {
                fieldValue[i] = data[offset + i];
            }

            for (int j = length - 1; j >= 0; j--)
            {
                hexValue.Append(string.Format("{0:X2}", fieldValue[j]));
            }
            return Convert.ToInt32(hexValue.ToString(), 16);
        }
        #endregion
    }
}
