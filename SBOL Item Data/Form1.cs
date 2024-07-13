using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SBOL_Item_Data
{
    public partial class Form1 : Form
    {
        #region Variables
        private bool ready = false;
        private string fileName;
        private bool errorsDetected = false;
        public DataSet dsItemData = null;
        public ItemDataManager itemDataManager = new ItemDataManager();
        #endregion
        public Form1()
        {
            InitializeComponent();
            SetDoubleBuffered(dgTableView);
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.InitialDirectory = ".";
                openFileDialog.Filter = "SBOL Item Data|*.dat";
                openFileDialog.FilterIndex = 1;
                openFileDialog.RestoreDirectory = true;

                if(openFileDialog.ShowDialog(this) == DialogResult.OK)
                {
                    ready = false;
                    tsStatus.Text = "Ready";
                    errorsDetected = false;
                    dgTableView.DataSource = null;
                    itemDataManager.ClearItemData();
                    if (dgTableView.Rows.Count > 0) dgTableView.Rows.Clear();
                    if (dgTableView.Columns.Count > 0) dgTableView.Columns.Clear();
                    dgTableView.Refresh();
                    fileName = openFileDialog.FileName;
                    if ((dsItemData = itemDataManager.LoadItemData(fileName)) != null)
                    {
                        tsStatus.Text = fileName;
                        LoadDataTable(dsItemData.Tables["itemData"], null);
                        FormatTable();
                    }
                    else
                    {
                        MessageBox.Show("Item file format is invalid or not supported.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch(Exception ex)
            {
                tsStatus.Text = "Ready";
                MessageBox.Show("An error occured whilst trying to open the selected file: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.InitialDirectory = ".";
                saveFileDialog.Filter = "SBOL Item Data|*.dat";
                saveFileDialog.FilterIndex = 1;
                saveFileDialog.RestoreDirectory = true;
                if (saveFileDialog.ShowDialog(this) == DialogResult.OK)
                {
                    fileName = saveFileDialog.FileName;
                    itemDataManager.SaveItemData(fileName, dsItemData);
                    tsStatus.Text = fileName + (errorsDetected ? " (Errors Detected)" : "");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occured whilst trying to save file: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public static void SetDoubleBuffered(System.Windows.Forms.Control control)
        {
            // set instance non-public property with name "DoubleBuffered" to true
            typeof(System.Windows.Forms.Control).InvokeMember("DoubleBuffered",
                System.Reflection.BindingFlags.SetProperty | System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic,
                null, control, new object[] { true });
        }

        private void LoadDataTable(DataTable dtData, string Filter)
        {
            DataGridViewComboBoxColumn itemType = new DataGridViewComboBoxColumn();
            itemType.Name = "Item Type";
            itemType.DataPropertyName = "Item Type";
            itemType.FlatStyle = FlatStyle.Flat;
            itemType.ValueMember = "Value";
            itemType.DisplayMember = "Value";
            itemType.DisplayStyle = DataGridViewComboBoxDisplayStyle.Nothing;
            itemType.DataSource = new BindingSource(itemDataManager.itemTypeNames, null);
            dgTableView.Columns.Add(itemType);

            DataGridViewComboBoxColumn carClass = new DataGridViewComboBoxColumn();
            carClass.Name = "Car Class";
            carClass.DataPropertyName = "Car Class";
            carClass.FlatStyle = FlatStyle.Flat;
            carClass.ValueMember = "Value";
            carClass.DisplayMember = "Value";
            carClass.DisplayStyle = DataGridViewComboBoxDisplayStyle.Nothing;
            carClass.DataSource = new BindingSource(itemDataManager.classNames, null);
            dgTableView.Columns.Add(carClass);

            DataGridViewComboBoxColumn manufacturer = new DataGridViewComboBoxColumn();
            manufacturer.Name = "Manufacturer";
            manufacturer.DataPropertyName = "Manufacturer";
            manufacturer.FlatStyle = FlatStyle.Flat;
            manufacturer.ValueMember = "Value";
            manufacturer.DisplayMember = "Value";
            manufacturer.DisplayStyle = DataGridViewComboBoxDisplayStyle.Nothing;
            manufacturer.DataSource = new BindingSource(itemDataManager.manufacturerNames, null);
            dgTableView.Columns.Add(manufacturer);

            DataGridViewComboBoxColumn carID = new DataGridViewComboBoxColumn();
            carID.Name = "Car";
            carID.DataPropertyName = "Car";
            carID.FlatStyle = FlatStyle.Flat;
            carID.ValueMember = "Value";
            carID.DisplayMember = "Value";
            carID.DisplayStyle = DataGridViewComboBoxDisplayStyle.Nothing;
            carID.DataSource = new BindingSource(itemDataManager.carNames, null);
            dgTableView.Columns.Add(carID);

            SetDataSource(dtData.TableName, dtData, Filter);

            dgTableView.Columns["Item ID"].DisplayIndex = 0;
            dgTableView.Columns["Item Type"].DisplayIndex = 1;
            dgTableView.Columns["Car Class"].DisplayIndex = 2;
            dgTableView.Columns["Manufacturer"].DisplayIndex = 3;
            dgTableView.Columns["Car"].DisplayIndex = 4;
            dgTableView.Columns["Category"].DisplayIndex = 5;
            dgTableView.Columns["Type"].DisplayIndex = 6;
            dgTableView.Columns["Type Value"].DisplayIndex = 7;
            dgTableView.Columns["Item Boost"].DisplayIndex = 8;
            dgTableView.Columns["Resale Value"].DisplayIndex = 9;
            dgTableView.Columns["Sale Value"].DisplayIndex = 10;
            
            dgTableView.Columns["_ItemType"].Visible = false;
            dgTableView.Columns["_CarClass"].Visible = false;
            dgTableView.Columns["_CarID"].Visible = false;
            dgTableView.Columns["_Manufacturer"].Visible = false;
            dgTableView.Columns["unknown1"].Visible = false;
            dgTableView.Columns["unknown2"].Visible = false;
        }

        private void SetDataSource(string tableName, DataTable dtData, string Filter)
        {
            dgTableView.DataSource = dtData;
            if (Filter != null)
            {
                (dgTableView.DataSource as DataTable).DefaultView.RowFilter = string.Format(Filter);
            }
        }

        private void FormatTable()
        {
            if (ready == true) return;
            tsStatus.Text = "Loading file please wait...";
            foreach (DataGridViewRow row in dgTableView.Rows)
            {
                try
                {
                    row.Cells["Item Type"].Value = itemDataManager.itemTypeNames[Convert.ToInt32(row.Cells["_ItemType"].Value)];
                    if(row.Cells["Item Type"].Value.Equals("Item")) row.Cells["Car"] = new DataGridViewTextBoxCell();
                }
                catch
                {
                    row.Cells["Item Type"].Value = "INVALID";
                    row.Cells["_ItemType"].Value = 0;
                    tsStatus.Text = "Loading file please wait... Errors in file detected.";
                    errorsDetected = true;
                    this.Refresh();
                }
                try
                {
                    row.Cells["Car Class"].Value = itemDataManager.classNames[Convert.ToInt32(row.Cells["_CarClass"].Value)];
                }
                catch
                {
                    row.Cells["Car Class"].Value = "INVALID";
                    row.Cells["_CarClass"].Value = -1;
                    tsStatus.Text = "Loading file please wait... Errors in file detected.";
                    errorsDetected = true;
                    this.Refresh();
                }
                try
                {
                    row.Cells["Manufacturer"].Value = itemDataManager.manufacturerNames[Convert.ToInt32(row.Cells["_Manufacturer"].Value)];
                }
                catch
                {
                    row.Cells["Manufacturer"].Value = "INVALID";
                    row.Cells["_Manufacturer"].Value = -1;
                    tsStatus.Text = "Loading file please wait... Errors in file detected.";
                    errorsDetected = true;
                    this.Refresh();
                }
                try
                {
                    if (row.Cells["Car"].GetType().ToString().Equals("System.Windows.Forms.DataGridViewTextBoxCell"))
                    {
                        row.Cells["Car"].Value = row.Cells["_CarID"].Value;
                    }
                    else
                    {
                        row.Cells["Car"].Value = itemDataManager.carNames[Convert.ToInt32(row.Cells["_CarID"].Value)];
                    }
                }
                catch
                {
                    row.Cells["Car"].Value = "INVALID";
                    row.Cells["_CarID"].Value = -1;
                    tsStatus.Text = "Loading file please wait... Errors in file detected.";
                    errorsDetected = true;
                    this.Refresh();
                }
            }
            tsStatus.Text = fileName + (errorsDetected ? " (Errors Detected)" : "");
            ready = true;
        }

        private void dgTableView_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {

        }

        private void dgTableView_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (ready == true)
                {
                    string headerText = dgTableView.Columns[e.ColumnIndex].HeaderText;
                    if (dgTableView.Rows[e.RowIndex].Cells[headerText].Value.ToString().Equals("INVALID"))
                    {
                        return;
                    }
                    if (headerText.Equals("Item Type"))
                    {
                        int offset = 0;
                        if (dgTableView.Rows[e.RowIndex].Cells[headerText].Value.ToString().Equals("Item") == false)
                        {
                            DataGridViewComboBoxCell carID = new DataGridViewComboBoxCell();
                            carID.FlatStyle = FlatStyle.Flat;
                            carID.ValueMember = "Value";
                            carID.DisplayMember = "Value";
                            carID.DisplayStyle = DataGridViewComboBoxDisplayStyle.Nothing;
                            carID.DataSource = new BindingSource(itemDataManager.carNames, null);
                            dgTableView.Rows[e.RowIndex].Cells["Car"] = carID;
                            offset = itemDataManager.carNames.Keys.ToList().IndexOf(Convert.ToInt32(dgTableView.Rows[e.RowIndex].Cells["_CarID"].Value));
                            if(offset < 0)
                            {
                                dgTableView.Rows[e.RowIndex].Cells["Car"].Value = "INVALID";
                                return;
                            }
                            else
                            {
                                dgTableView.Rows[e.RowIndex].Cells["Car"].Value = itemDataManager.carNames[Convert.ToInt32(dgTableView.Rows[e.RowIndex].Cells["_CarID"].Value)];
                            }
                        }
                        else
                        {
                            dgTableView.Rows[e.RowIndex].Cells["Car"] = new DataGridViewTextBoxCell();
                            dgTableView.Rows[e.RowIndex].Cells["Car"].Value = dgTableView.Rows[e.RowIndex].Cells["_CarID"].Value;
                        }
                        offset = itemDataManager.itemTypeNames.Values.ToList().IndexOf(dgTableView.Rows[e.RowIndex].Cells[headerText].Value.ToString());
                        if (offset < 0)
                        {
                            dgTableView.Rows[e.RowIndex].Cells["_ItemType"].Value = 0;
                        }
                        else
                        {
                            dgTableView.Rows[e.RowIndex].Cells["_ItemType"].Value = offset;
                        }
                    }
                    else if (headerText.Equals("Car Class"))
                    {
                        int offset = itemDataManager.classNames.Values.ToList().IndexOf(dgTableView.Rows[e.RowIndex].Cells[headerText].Value.ToString());
                        if (offset < 0)
                        {
                            dgTableView.Rows[e.RowIndex].Cells["_CarClass"].Value = -1;
                        }
                        else
                        {
                            dgTableView.Rows[e.RowIndex].Cells["_CarClass"].Value = offset - 1;
                        }
                    }
                    else if (headerText.Equals("Manufacturer"))
                    {
                        int offset = itemDataManager.manufacturerNames.Values.ToList().IndexOf(dgTableView.Rows[e.RowIndex].Cells[headerText].Value.ToString());
                        if (offset < 0)
                        {
                            dgTableView.Rows[e.RowIndex].Cells["_Manufacturer"].Value = -1;
                        }
                        else
                        {
                            dgTableView.Rows[e.RowIndex].Cells["_Manufacturer"].Value = offset - 1;
                        }
                    }
                    else if (headerText.Equals("Car"))
                    {
                        int offset = itemDataManager.carNames.Values.ToList().IndexOf(dgTableView.Rows[e.RowIndex].Cells[headerText].Value.ToString());
                        if (offset < 0)
                        {
                            dgTableView.Rows[e.RowIndex].Cells["_CarID"].Value = -1;
                        }
                        else
                        {
                            dgTableView.Rows[e.RowIndex].Cells["_CarID"].Value = offset - 1;
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgTableView_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if(e.Button == MouseButtons.Right && dgTableView.SelectedCells.Count > 1)
            {
                contextMenuStrip1.Show(MousePosition);
            }
        }

        private void copyFromFirstSelected_Click(object sender, EventArgs e)
        {
            var toCopy = dgTableView.SelectedCells[dgTableView.SelectedCells.Count - 1].Value;
            foreach(DataGridViewCell cell in dgTableView.SelectedCells)
            {
                cell.Value = toCopy;
            }
        }
    }
}
