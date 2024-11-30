//10.������� ������, ���������� ��� ������ ��������. 
using lab6._2;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace lab6._2
{
    public partial class Form1 : Form
    {
        //private MatrMake matrMake;
        //private int[,] originalMatrix; //���������� ��� �������� �������� �������
        int size;
        int n_str_res;
        public Form1()
        {
            InitializeComponent();
        }

        private void textBoxSize_Enter(object sender, EventArgs e)
        {
            if (textBoxSize.Text == "������ �������")
            {
                textBoxSize.Text = "";
                textBoxSize.ForeColor = Color.Black; // ���������� ���� ������ �� ������
            }
        }

        private void textBoxSize_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxSize.Text))
            {
                textBoxSize.Text = "������ �������";
                textBoxSize.ForeColor = Color.Gray; // ���������� ���� ������ �� �����
            }
        }
        //�������� ������� � ����������
        private void btnCreate_Click(object sender, EventArgs e)
        {
            //��������� ������� ������� � DataGridView 
            int i;
            size = Int32.Parse(textBoxSize.Text);
            if (size > 0)
            {
                DataTable matr = new DataTable("matr");
                DataColumn[] cols = new DataColumn[size];
                for (i = 0; i < size; i++)
                {
                    cols[i] = new DataColumn(i.ToString());
                    matr.Columns.Add(cols[i]);
                }
                for (i = 0; i < size; i++)
                {
                    DataRow newRow;
                    newRow = matr.NewRow();
                    matr.Rows.Add(newRow);
                }
                dataGridViewInput.DataSource = matr;
                for (i = 0; i < size; i++)
                    dataGridViewInput.Columns[i].Width = 50;
            }
            else
            {
                MessageBox.Show("����������, ������� ���������� ������ �������.");
            }
        }

        private void btnSaveOriginal_Click(object sender, EventArgs e)
        {
            if (dataGridViewInput.RowCount > 0)
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "Text Files (*.txt)|*.txt|C# Files (*.cs)|*.cs|All Files (*.*)|*.*";
                saveFileDialog.Title = "��������� �������� �������";

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    using (StreamWriter writer = new StreamWriter(saveFileDialog.FileName))
                    {
                        for (int i = 0; i < size; i++)
                        {
                            for (int j = 0; j < size; j++)
                            {
                                writer.Write(dataGridViewInput.Rows[i].Cells[j].Value.ToString());
                                if (j < size -1)
                                    writer.Write("\t"); // ��������� ��������� ����� ����������
                            }
                            writer.WriteLine(); // ������� �� ����� ������ ����� ������ ������ �������
                        }
                    }
                    MessageBox.Show("�������� ������� ���������.");
                }
            }
            else
            {
                MessageBox.Show("������� �������� �������.");
            }
        }
        private void btnSaveResult_Click(object sender, EventArgs e)
        {
            if (dataGridViewResult.RowCount > 0)
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "Text Files (*.txt)|*.txt|C# Files (*.cs)|*.cs|All Files (*.*)|*.*";
                saveFileDialog.Title = "��������� ������������ �������";

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    using (StreamWriter writer = new StreamWriter(saveFileDialog.FileName))
                    {
                        for (int i = 0; i < n_str_res; i++)
                        {
                            for (int j = 0; j < size; j++)
                            {
                                writer.Write(dataGridViewResult.Rows[i].Cells[j].Value.ToString());
                                if (j < size - 1)
                                    writer.Write("\t"); // ��������� ��������� ����� ����������
                            }
                            writer.WriteLine(); // ������� �� ����� ������ ����� ������ ������ �������
                        }
                    }
                    MessageBox.Show("������������ ������� ���������.");
                }
            }
            else
            {
                MessageBox.Show("������� ��������� ������.");
            }
        }
        //�������� ������� � ����������
        private void btnLoadMatrix_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Text Files (*.txt)|*.txt|C# Files (*.cs)|*.cs|All Files (*.*)|*.*";
            openFileDialog.Title = "��������� �������";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string[] lines = File.ReadAllLines(openFileDialog.FileName);

                size = Convert.ToInt32(lines[0]);
                
                DataTable matr = new DataTable("matr");
                DataColumn[] cols = new DataColumn[size];
                for (int i = 0; i < size; i++)
                {
                    cols[i] = new DataColumn(i.ToString());
                    matr.Columns.Add(cols[i]);
                }
                for (int i = 1; i <= size; i++)
                {
                    string[] values = lines[i].Split(new char[] { '\t' }, StringSplitOptions.RemoveEmptyEntries);

                    DataRow newRow;
                    newRow = matr.NewRow();
                    for (int j = 0; j < size; j++)
                    {
                        newRow[j] = int.Parse(values[j]);
                    }
                        matr.Rows.Add(newRow);
                }
                dataGridViewInput.DataSource = matr;
                for (int i = 0; i < size; i++)
                    dataGridViewInput.Columns[i].Width = 50;
                ;
                //originalMatrix = new int[size, size];
                //dataGridViewInput.ColumnCount = size;
                //dataGridViewInput.RowCount = size;
                //for (int i = 0; i < size; i++)
                //{
                //    string[] values = lines[i + 1].Split(new char[] { '\t' }, StringSplitOptions.RemoveEmptyEntries);
                //    for (int j = 0; j < size; j++)
                //    {
                //            originalMatrix[i, j] = int.Parse(values[j]);
                //            dataGridViewInput.Rows[i].Cells[j].Value = originalMatrix[i, j];
                //    }
                //}
            }
        }

        private void btnTask_Click(object sender, EventArgs e)
        {
            dataGridViewResult.Visible = false;
            MatrMake mt = new MatrMake(size);
            mt.GridToMatrix(dataGridViewInput);
            if (mt.DelStr())
                MessageBox.Show("��� ������ �������� ����");
            else
            {
                dataGridViewResult.Visible = true;
                //dataGridViewResult.ColumnCount = size;
                n_str_res = mt.GetStr();
                //dataGridViewResult.RowCount = n_str_res;
                mt.MatrixToGrid(dataGridViewResult);
            }
        }
    }
}
