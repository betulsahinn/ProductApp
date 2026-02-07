using ProductApp.Models;
using ProductApp.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProductApp
{
    public partial class Form1 : Form
    {
        private readonly ProductService _service = new();
        private int _selectedId = 0;

        public Form1()
        {
            InitializeComponent();
            LoadProducts();

            dataGridView1.ReadOnly = true;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            UpdateButtonStates();
        }

        private void txtName_TextChanged(object sender, EventArgs e)
        {
            UpdateButtonStates();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (_selectedId > 0)
            {
                MessageBox.Show(
                    "Mevcut bir kaydı düzenliyorsunuz.\n" +
                    "Yeni kayıt eklemek için formu temizleyin.",
                    "Uyarı");
                return;
            }

            try
            {
                var product = new Product
                {
                    Name = txtName.Text,
                    Price = Convert.ToDecimal(txtPrice.Text)
                };

                bool added = _service.Add(product);

                if (!added)
                {
                    MessageBox.Show("Bu ürün zaten mevcut.");
                    return;
                }

                MessageBox.Show("Kayıt eklendi");
                LoadProducts();
                ClearForm();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Hata");
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            txtName.Focus();
        }


        private void txtPrice_TextChanged(object sender, EventArgs e)
        {
            UpdateButtonStates();
        }
        private void LoadProducts()
        {
            dataGridView1.DataSource = _service.GetAll();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (_selectedId == 0)
            {
                MessageBox.Show("Lütfen güncellenecek bir kayıt seçin.");
                return;
            }

            var product = new Product
            {
                Id = _selectedId,
                Name = txtName.Text,
                Price = Convert.ToDecimal(txtPrice.Text)
            };

            _service.Update(product);
            LoadProducts();

            MessageBox.Show("Güncellendi");
            ClearForm();

        }


        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (_selectedId == 0)
            {
                MessageBox.Show("Lütfen silinecek bir kayıt seçin.");
                return;
            }

            var result = MessageBox.Show(
                "Bu ürünü silmek istediğinize emin misiniz?",
                "Onay",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (result == DialogResult.Yes)
            {
                _service.Delete(_selectedId);
                LoadProducts();
                ClearForm();

                MessageBox.Show("Silindi");
            }
        }

        private void ClearForm()
        {
            txtName.Clear();
            txtPrice.Clear();
            _selectedId = 0;

            dataGridView1.ClearSelection();

            btnAdd.Enabled = true;
            UpdateButtonStates();
        }


        private void UpdateButtonStates()
        {
            btnAdd.Enabled = !string.IsNullOrWhiteSpace(txtName.Text)
                             && decimal.TryParse(txtPrice.Text, out _);

            btnUpdate.Enabled = _selectedId > 0;
            btnDelete.Enabled = _selectedId > 0;
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
                return;

            var row = dataGridView1.SelectedRows[0];

            _selectedId = (int)row.Cells["Id"].Value;
            txtName.Text = row.Cells["Name"].Value.ToString();
            txtPrice.Text = row.Cells["Price"].Value.ToString();

            btnAdd.Enabled = false;
            UpdateButtonStates();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            dataGridView1.ClearSelection();

            _selectedId = 0;

            txtName.Clear();
            txtPrice.Clear();

            btnAdd.Enabled = true;
            btnUpdate.Enabled = false;
            btnDelete.Enabled = false;

            txtName.Focus();
        }
    }
}
