using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace listaBarras
{
    public partial class Form1 : DevExpress.XtraEditors.XtraForm
    {
        public Form1()
        {
            InitializeComponent();
            dataGridView1.ColumnCount = 4;
            dataGridView1.Columns[0].Name = "Referencia";
            dataGridView1.Columns[1].Name = "Descrição";
            dataGridView1.Columns[2].Name = "Quantidade";
            dataGridView1.Columns[3].Name = "Lote";

            dataGridView1.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView1.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView1.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView1.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void Inserir_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox1.Text) &&
                string.IsNullOrEmpty(textBox2.Text) &&
                string.IsNullOrEmpty(textBox3.Text) &&
                string.IsNullOrEmpty(textBox4.Text))
            {
                string input = textBox5.Text;
                Preencher(input);
            }
            else if (string.IsNullOrEmpty(textBox5.Text))
            {
                addLinha(textBox1.Text, textBox2.Text, textBox3.Text, textBox4.Text);
            }
        }

        private void addLinha(string referencia, string desc, string qtd, string lote)
        {
            // Verifica se a referência já existe na tabela
            bool found = false;
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.Cells[0].Value != null && row.Cells[0].Value.ToString() == referencia)
                {
                    found = true;
                    // Verifica se o lote é o mesmo
                    if (row.Cells[3].Value != null && row.Cells[3].Value.ToString() == lote)
                    {
                        // Atualiza a quantidade na coluna correspondente
                        int quantidade = int.Parse(row.Cells[2].Value.ToString()) + int.Parse(qtd);
                        row.Cells[2].Value = quantidade.ToString();
                    }
                    else
                    {
                        // Adiciona uma nova linha com o lote e quantidade
                        String[] newRow = { referencia, desc, qtd, lote };
                        dataGridView1.Rows.Add(newRow);
                    }
                    break;
                }
            }

            // Se a referência não existe na tabela, adiciona uma nova linha
            if (!found)
            {
                String[] row = { referencia, desc, qtd, lote };
                dataGridView1.Rows.Add(row);
            }
        }


        private void Apagar_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dataGridView1.SelectedRows)
            {
                dataGridView1.Rows.RemoveAt(row.Index);
            }
        }

        private void Preencher(string input)
        {
            Console.WriteLine(input);
            string referencia = input.Substring(3, 7);
            string desc = "";
            string qtd = input.Substring(21);
            string lote = input.Substring(13);
            lote = lote.Substring(0, 6);


            addLinha(referencia, desc, qtd, lote);
        }
        


        }
    }
