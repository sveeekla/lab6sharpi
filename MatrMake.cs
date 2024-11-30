using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab6._2
{
    class MatrMake
    {
        int n_str,     //количество строк 
            n_col;     //количество столбцов 
        int[,] matrix; // обрабатываемая матрица
        public MatrMake(int n)
        {
            n_str = n;
            n_col = n; // на входе матрица квадратная 
            matrix = new int[n, n];
        }
        //матрица из DataGridView 
        public void GridToMatrix(DataGridView dgv)
        {
            DataGridViewCell txtCell;
            for (int i = 0; i < n_str; i++)
            {
                for (int j = 0; j < n_col; j++)
                {
                    txtCell = dgv.Rows[i].Cells[j];
                    string s = txtCell.Value.ToString();
                    if (s == "")
                        matrix[i, j] = 0;
                    else
                        matrix[i, j] = Int32.Parse(s);
                }
            }
        }
        //вывод матрицы в DataGridView 
        public void MatrixToGrid(DataGridView dgv)
        {
            //установка размеров 
            int i;
            DataTable matr = new DataTable("matr");
            DataColumn[] cols = new DataColumn[n_col];
            for (i = 0; i < n_col; i++)
            {
                cols[i] = new DataColumn(i.ToString());
                matr.Columns.Add(cols[i]);
            }
            for (i = 0; i < n_str; i++)
            {
                DataRow newRow;
                newRow = matr.NewRow();
                matr.Rows.Add(newRow);
            }
            dgv.DataSource = matr;
            for (i = 0; i < n_col; i++)
                dgv.Columns[i].Width = 50;
            //занесение значений
            DataGridViewCell txtCell;
            for (i = 0; i < n_str; i++)
            {
                for (int j = 0; j < n_col; j++)
                {
                    txtCell = dgv.Rows[i].Cells[j];
                    txtCell.Value = matrix[i, j].ToString();
                }
            }
        }
        public bool DelStr()// Метод для удаления строк true если пустая
        {
            bool ok;
            int i, j;
            i = 0;
            while (i < n_str)
            {
                ok = true;
                for (j = 0; j < n_col - 1 && ok; j++)
                    if (matrix[i, j] != matrix[i, j + 1])
                        ok = false;
                if (ok)
                {
                    for (int k = i; k < n_str - 1; k++)
                        for (j = 0; j < n_col; j++)
                            matrix[k, j] = matrix[k + 1, j];
                    n_str--;
                }
                else
                    i++;
            }
            if (n_str == 0)
                return true;
            else
                return false;
        }
        public int GetCol()
        {
            return n_col;
        }
        public int GetStr()
        {
            return n_str;
        }
    }
}
