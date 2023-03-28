using DevExpress.Internal.WinApi.Windows.UI.Notifications;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace caixaDeTexto
{
    public partial class Form1 : DevExpress.XtraEditors.XtraForm
    {
        #region Variaveis
        public string mVariaveis = "";
        //tabelas
        public string mTabelaOrigem = "";
        public string mTabelaDestino = "";
        public string mView = "";
        private string mConnectionStringOrigem;
        private string mConnectionStringDestino;
        //Resposta
        public string mRes = "";

        public class ColumnInfo
        {
            public string Name { get; set; }
            public string DataType { get; set; }
            public DbType Type { get; set; }
        }

        public List<ColumnInfo> mColumnListOrigem = new List<ColumnInfo>();
        public List<ColumnInfo> mColumnListDestino = new List<ColumnInfo>();
        private List<string> mListColums;
        private string dbDestino;
        private string dbOrigem;
        #endregion
        #region Arranque
        public Form1()
        {
            InitializeComponent();
        }
        public void ini()
        {
            mView = TextBoxView01.Text;

            mConnectionStringOrigem = File.ReadAllText("connectionstringOrigem.txt");
            mConnectionStringDestino = File.ReadAllText("connectionstringDestino.txt");
           
        }
        #endregion
        #region Gets
        private string GetDatabaseName(string connectionString)
        {
            Match match = Regex.Match(connectionString, @"Catalog=([^;]+)");
            return match.Groups[1].Value;
        }
        private List<ColumnInfo> GetColumns(string connectionString, string tableName)
        {
            List<ColumnInfo> columns = new List<ColumnInfo>();

            string sql = $"SELECT COLUMN_NAME, DATA_TYPE FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = '{tableName}'";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            ColumnInfo vColumn = new ColumnInfo();
                            vColumn.Name = reader.GetString(0);
                            vColumn.DataType = reader.GetString(1);
                            string dbTypeName = reader.GetString(1);
                            DbType dbType;

                            switch (dbTypeName.ToLower())
                            {
                                case "int":
                                    dbType = DbType.Int32;
                                    break;
                                case "decimal":
                                    dbType = DbType.Decimal;
                                    break;
                                case "bit":
                                    dbType = DbType.Boolean;
                                    break;
                                case "datetime":
                                    dbType = DbType.DateTime;
                                    break;
                                case string s when s.StartsWith("varchar"):
                                    dbType = DbType.String;
                                    break;
                                default:
                                    // Default to string if the dbTypeName is not recognized
                                    dbType = DbType.String;
                                    break;
                            }

                            vColumn.Type = dbType;
                            columns.Add(vColumn);
                        }
                    }
                }
            }

            return columns;
        }
        private string GetParameterStringsSelect()
        {
            string result = "";

            // Conexão com a tabela de destino
            // Obtém as colunas da tabela de destino
            using (SqlConnection connection = new SqlConnection(mConnectionStringDestino))
            {
                connection.Open();

                // Comando SQL para obter as colunas da tabela de destino
                SqlCommand command = new SqlCommand($"SELECT COLUMN_NAME, DATA_TYPE FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = '{mTabelaDestino}'", connection);

                // Leitura dos dados das colunas
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    string nomeColuna = reader.GetString(0);
                    string tipoDados = reader.GetString(1);
                    DbType dbType = GetDbType(tipoDados);
                    foreach (string vItem in mListColums)
                    {
                        if (vItem == nomeColuna)
                        {
                            // Adiciona os parâmetros na string de resultado
                            result += $"{nomeColuna} = vReg[\"{nomeColuna}\"].To{dbType}();" + Environment.NewLine;
                        }
                    }
                }
                reader.Close();
                connection.Close();
            }
            return result;
        }
        private string GetParameterStrings()
        {
            string result = "";
            // Conexão com a tabela de destino
            using (SqlConnection connection = new SqlConnection(mConnectionStringDestino))
            {
                connection.Open();

                // Comando SQL para obter as colunas da tabela de destino
                SqlCommand command = new SqlCommand($"SELECT COLUMN_NAME, DATA_TYPE FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = '{mTabelaDestino}'", connection);

                // Leitura dos dados das colunas
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    string nomeColuna = reader.GetString(0);
                    string tipoDados = reader.GetString(1);
                    DbType dbType = GetDbType(tipoDados);
                    foreach (string vItem in mListColums)
                    {
                        if (vItem == nomeColuna)
                        {
                            // Adiciona os parâmetros na string de resultado
                            result += $"vDataDestino.AddParameter(\"@{nomeColuna}\", DbType.{dbType}, {nomeColuna}, dbDestino);" + Environment.NewLine;
                        }
                    }

                }
                reader.Close();
                connection.Close();
            }
            return result;
        }
        private DbType GetDbType(string tipoDados)
        {
            switch (tipoDados.ToLower())
            {
                case "bigint":
                    return DbType.Int64;
                case "binary":
                    return DbType.Binary;
                case "bit":
                    return DbType.Boolean;
                case "char":
                    return DbType.String;
                case "date":
                case "datetime":
                case "datetime2":
                case "smalldatetime":
                    return DbType.DateTime;
                case "decimal":
                case "money":
                case "numeric":
                    return DbType.Decimal;
                case "float":
                    return DbType.Double;
                case "image":
                    return DbType.Binary;
                case "int":
                    return DbType.Int32;
                case "nchar":
                    return DbType.String;
                case "ntext":
                case "nvarchar":
                case "text":
                case "varchar":
                    return DbType.String;
                case "real":
                    return DbType.Single;
                case "smallint":
                    return DbType.Int16;
                case "sql_variant":
                    return DbType.Object;
                case "time":
                    return DbType.Time;
                case "timestamp":
                    return DbType.Binary;
                case "tinyint":
                    return DbType.Byte;
                case "uniqueidentifier":
                    return DbType.Guid;
                case "xml":
                    return DbType.Xml;
                default:
                    throw new Exception($"Tipo de dados desconhecido: {tipoDados}");
            }
        }
        private string GetSqlTypeName(DbType dbType, int maxLength)
        {
            switch (dbType)
            {
                case DbType.String:
                    return maxLength == -1 ? "nvarchar(max)" : $"nvarchar({maxLength})";
                case DbType.AnsiString:
                    return maxLength == -1 ? "varchar(max)" : $"varchar({maxLength})";
                case DbType.AnsiStringFixedLength:
                    return maxLength == -1 ? "char(max)" : $"char({maxLength})";
                case DbType.Binary:
                    return maxLength == -1 ? "varbinary(max)" : $"varbinary({maxLength})";
                case DbType.Boolean:
                    return "bit";
                case DbType.Byte:
                    return "tinyint";
                case DbType.Currency:
                    return "money";
                case DbType.Date:
                    return "date";
                case DbType.DateTime:
                    return "datetime2";
                case DbType.Decimal:
                    return "decimal(18, 2)";
                case DbType.Double:
                    return "float";
                case DbType.Guid:
                    return "uniqueidentifier";
                case DbType.Int16:
                    return "smallint";
                case DbType.Int32:
                    return "int";
                case DbType.Int64:
                    return "bigint";
                case DbType.Object:
                    return "sql_variant";
                case DbType.Single:
                    return "real";
                case DbType.Time:
                    return "time";
                default:
                    throw new ArgumentException($"Unsupported DbType: {dbType}");
            }
        }
        private DbType GetDbTypeFromDbTypeName(string dbTypeName)
        {
            switch (dbTypeName.ToLower())
            {
                case "bigint":
                    return DbType.Int64;
                case "binary":
                    return DbType.Binary;
                case "bit":
                    return DbType.Boolean;
                case "char":
                    return DbType.AnsiStringFixedLength;
                case "date":
                    return DbType.Date;
                case "datetime":
                    return DbType.DateTime;
                case "datetime2":
                    return DbType.DateTime2;
                case "datetimeoffset":
                    return DbType.DateTimeOffset;
                case "decimal":
                    return DbType.Decimal;
                case "float":
                    return DbType.Double;
                case "image":
                    return DbType.Binary;
                case "int":
                    return DbType.Int32;
                case "money":
                    return DbType.Currency;
                case "nchar":
                    return DbType.StringFixedLength;
                case "ntext":
                    return DbType.String;
                case "numeric":
                    return DbType.Decimal;
                case "nvarchar":
                    return DbType.String;
                case "real":
                    return DbType.Single;
                case "smalldatetime":
                    return DbType.DateTime;
                case "smallint":
                    return DbType.Int16;
                case "smallmoney":
                    return DbType.Currency;
                case "sql_variant":
                    return DbType.Object;
                case "text":
                    return DbType.AnsiString;
                case "time":
                    return DbType.Time;
                case "timestamp":
                    return DbType.Binary;
                case "tinyint":
                    return DbType.Byte;
                case "uniqueidentifier":
                    return DbType.Guid;
                case "varbinary":
                    return DbType.Binary;
                case "varchar":
                    return DbType.AnsiString;
                case "xml":
                    return DbType.Xml;
                default:
                    throw new ArgumentException($"Unknown database type name: {dbTypeName}");
            }
        }
        #endregion
        #region Events
        

        private List<string> GetTables(string connectionString)
        {
            List<string> tables = new List<string>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                DataTable schema = connection.GetSchema("Tables");

                foreach (DataRow row in schema.Rows)
                {
                    string tableName = row["TABLE_NAME"].ToString();

                    tables.Add(tableName);
                }
            }

            return tables;
        }


        private string MainCheckUpdate()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("vQuery = \"SELECT * FROM {0} WHERE IGID = @IGID\"", mTabelaDestino);
            return sb.ToString() + ";";
        }
        private string MainInsert()
        {
            string Insert = "";
            string vQuery = "";
            string colunas = "";
            string parametros = "";
            using (SqlConnection connection = new SqlConnection(mConnectionStringDestino))
            {
                connection.Open();
                SqlCommand command = new SqlCommand($"SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = '{mTabelaDestino}'", connection);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    string columnName = reader.GetString(0);
                    if (mListColums.Contains(columnName)) // Replace mListColums with columnStrings
                    {
                        colunas += $"[{columnName}], ";
                        parametros += $"@{columnName}, ";
                    }
                }

                // Remove the extra comma at the end of colunas and parametros
                colunas = colunas.TrimEnd(',', ' ');
                parametros = parametros.TrimEnd(',', ' ');

                vQuery = $"INSERT INTO {mTabelaDestino} ({colunas}) VALUES ({parametros});";
                Insert += $"vQuery = \"{vQuery}\";\n".PadLeft(4);

                reader.Close();
                connection.Close();
                return Insert;
            }
        




    }
        private string MainUpdate()
        {
            string vQuery = "";
            string colunas = "";
            string parametros = "";
            // Obtém as colunas da tabela de destino
            using (SqlConnection connection = new SqlConnection(mConnectionStringDestino))
            {
                connection.Open();
                SqlCommand command = new SqlCommand($"SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = '{mTabelaDestino}'", connection);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    foreach (string vItem in mListColums)
                    {
                        if (vItem == reader.GetString(0))
                        {
                            colunas += $"{reader.GetString(0)} = @{reader.GetString(0)},";
                        }
                    }
                }
                reader.Close();
                connection.Close();
            }

            // Remove a vírgula extra no final das colunas e dos parâmetros
            colunas = colunas.TrimEnd(',', ' ');
            parametros = parametros.TrimEnd(',', ' ');

            // Cria a consulta INSERT com as colunas e parâmetros
            vQuery = $"UPDATE {mTabelaDestino} SET {colunas} WHERE IGID = @IGID";

            return $"vQuery = \"{vQuery}\";";
        }
        private string MainSelect()
        {
            string vQuery = "";
            string colunas = "";
            string parametros = "";
            // Obtém as colunas da tabela de destino
            using (SqlConnection connection = new SqlConnection(mConnectionStringDestino))
            {
                connection.Open();
                SqlCommand command = new SqlCommand($"SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = '{mTabelaDestino}'", connection);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    foreach (string vItem in mListColums)
                    {
                        if (vItem == reader.GetString(0))
                        {
                            colunas += $"[{reader.GetString(0)}], ";
                            parametros += $"@{reader.GetString(0)}, ";
                        }
                    }
                }
                reader.Close();
                connection.Close();
            }

            // Remove a vírgula extra no final das colunas e dos parâmetros
            colunas = colunas.TrimEnd(',', ' ');
            parametros = parametros.TrimEnd(',', ' ');

            // Cria a consulta INSERT com as colunas e parâmetros
            vQuery = $"SELECT * FROM " + mView + " WHERE IGID = @IGID";

            return $"\nvQuery = \"{vQuery}\";\n";
        }
        public string MainCreateTable()
        {
            StringBuilder queryBuilder = new StringBuilder();
            StringBuilder columnsBuilder = new StringBuilder();
            StringBuilder primaryKeyColumnsBuilder = new StringBuilder();
            using (SqlConnection connection = new SqlConnection(mConnectionStringDestino))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("SELECT COLUMN_NAME, DATA_TYPE, CHARACTER_MAXIMUM_LENGTH, IS_NULLABLE, COLUMN_DEFAULT, COLUMNPROPERTY(object_id(TABLE_NAME), COLUMN_NAME, 'IsIdentity') AS IsIdentity, " +
                                                             "(SELECT COUNT(*) FROM INFORMATION_SCHEMA.KEY_COLUMN_USAGE WHERE TABLE_NAME = @TableName AND COLUMN_NAME = c.COLUMN_NAME) AS IsPrimaryKey " +
                                                             "FROM INFORMATION_SCHEMA.COLUMNS c WHERE TABLE_NAME = @TableName", connection))
                {
                    command.Parameters.Add("@TableName", SqlDbType.NVarChar).Value = mTabelaDestino;

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string columnName = reader.GetString(0);
                            foreach (string vItem in mListColums)
                            {
                                if (vItem == columnName)
                                {
                                    string dbTypeName = reader.GetString(1);
                                    int maxLength = reader.IsDBNull(2) ? -1 : reader.GetInt32(2);
                                    bool isNullable = reader.IsDBNull(3) ? true : reader.GetString(3) == "YES";
                                    string columnDefault = reader.IsDBNull(4) ? null : reader.GetString(4);
                                    bool isIdentity = false;
                                    if (!reader.IsDBNull(5))
                                    {
                                        object value = reader.GetValue(5);
                                        if (value != null && value != DBNull.Value)
                                        {
                                            if (value.GetType() == typeof(bool))
                                            {
                                                isIdentity = (bool)value;
                                            }
                                            else if (value.GetType() == typeof(int))
                                            {
                                                isIdentity = ((int)value == 1);
                                            }
                                            // add more data type checks as needed
                                        }
                                    }
                                    bool isPrimaryKey = reader.GetInt32(6) > 0;

                                    // Map the database type name to a corresponding DbType value
                                    DbType dbType = GetDbTypeFromDbTypeName(dbTypeName);

                                    string typeName = GetSqlTypeName(dbType, maxLength);

                                    if (!isIdentity)
                                    {
                                        columnsBuilder.Append($"[{columnName}] {typeName} {(isNullable ? "NULL" : "NOT NULL")}, ");
                                    }
                                    else
                                    {
                                        columnsBuilder.Append($"[{columnName}] {typeName} IDENTITY(1,1) NOT NULL, ");
                                    }

                                    if (isPrimaryKey)
                                    {
                                        primaryKeyColumnsBuilder.Append($"[{columnName}] ASC, ");
                                    }
                                }
                            }
                        }
                    }
                }
            }

            string columns = columnsBuilder.ToString().TrimEnd(',', ' ');
            string primaryKeyColumns = primaryKeyColumnsBuilder.ToString().TrimEnd(',', ' ');

            if (!string.IsNullOrEmpty(primaryKeyColumns))
            {
                primaryKeyColumns = $"CONSTRAINT [PK_{mTabelaDestino}] PRIMARY KEY CLUSTERED ({primaryKeyColumns}) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]";
            }

            queryBuilder.Append($"vQuery = \"CREATE TABLE {mTabelaDestino} ({columns}, {primaryKeyColumns}) ON [PRIMARY]\";");

            return queryBuilder.ToString();
        }
        private string ConversorDataTypes(List<ColumnInfo> columnList)
        {
            StringBuilder sb = new StringBuilder();

            foreach (ColumnInfo col in columnList)
            {
                string dataType = col.DataType.ToLower();

                // Conversão dos tipos de dados
                switch (dataType)
                {
                    case "varchar":
                    case "nvarchar":
                    case "char":
                        dataType = "string";
                        break;
                    case "datetime":
                        dataType = "DateTime";
                        break;
                    case "uniqueidentifier":
                        dataType = "Guid";
                        break;
                    case "image":
                        dataType = "byte[]";
                        break;
                    case "bit":
                        dataType = "bool";
                        break;
                    case "tinyint":
                        dataType = "byte";
                        break;
                    case "money":
                        dataType = "decimal";
                        break;
                    case "bigint":
                        dataType = "long";
                        break;
                    default:
                        // Deixar o tipo de dados original para tipos não suportados
                        break;
                }

                // Adiciona as propriedades na string de resultado
                sb.AppendLine($"public {dataType} {col.Name} {{ get; set; }}");
            }

            return sb.ToString();
        }
        private static string TextToCodeFormat(string code, int spacesPerIndent = 4)
        {
            string indent = new string(' ', spacesPerIndent);
            int level = 0;
            StringBuilder result = new StringBuilder();

            foreach (string line in code.Split('\n'))
            {
                string trimmedLine = line.Trim();

                if (trimmedLine.StartsWith("}"))
                {
                    level = Math.Max(0, level - 1);
                }

                if (level >= 0)
                {
                    result.AppendLine($"{new string(' ', level * spacesPerIndent)}{trimmedLine}");
                }

                if (trimmedLine.EndsWith("{"))
                {
                    level++;
                }
            }

            return result.ToString().TrimEnd();
        }
        public List<string> ObterColunasIguais(string conexaoView, string conexaoTabela, string nomeView, string nomeTabela)
        {
            List<string> colunasIguais = new List<string>();

            // Obter as colunas da view
            List<string> colunasView = ObterColunas(conexaoView, nomeView);

            // Obter as colunas da tabela
            List<string> colunasTabela = ObterColunas(conexaoTabela, nomeTabela);

            // Verificar quais colunas são iguais
            foreach (string coluna in colunasView)
            {
                if (colunasTabela.Contains(coluna))
                {
                    colunasIguais.Add(coluna);
                }
            }

            return colunasIguais;
        }

        private List<string> ObterColunas(string conexao, string nomeTabela)
        {
            List<string> colunas = new List<string>();

            using (SqlConnection conexaoBD = new SqlConnection(conexao))
            {
                conexaoBD.Open();

                DataTable schemaTable = conexaoBD.GetSchema("Columns", new string[] { null, null, nomeTabela });

                foreach (DataRow row in schemaTable.Rows)
                {
                    colunas.Add(row["COLUMN_NAME"].ToString());
                }
            }

            return colunas;
        }
        public List<string> ObterColunasView()
        {
            //query para obter as colunas da view e da tabela
            string query = "SELECT v.COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS v " +
                           "JOIN INFORMATION_SCHEMA.COLUMNS t ON v.COLUMN_NAME = t.COLUMN_NAME " +
                           "WHERE v.TABLE_NAME = '" + mView + "' AND t.TABLE_NAME = '" + mTabelaDestino + "'";

            //lista que armazenará as colunas da view
            List<string> colunasView = new List<string>();

            using (SqlConnection conexao = new SqlConnection(mConnectionStringOrigem))
            {
                try
                {
                    conexao.Open();

                    SqlCommand comando = new SqlCommand(query, conexao);
                    SqlDataReader leitor = comando.ExecuteReader();

                    while (leitor.Read())
                    {
                        colunasView.Add(leitor["COLUMN_NAME"].ToString());
                    }

                    leitor.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Erro ao obter as colunas da view: " + ex.Message);
                }
            }

            return colunasView;
        }
        private void Form1_Load(object sender, EventArgs e)
        {

        }
        #endregion
        private void simpleButton1_Click(object sender, EventArgs e)
        {
            ini();
            // Obter todas as tabelas existentes na base de dados
            List<string> tablesOrigem = GetTables(mConnectionStringOrigem);
            List<string> tablesDestino = GetTables(mConnectionStringDestino);

            foreach (string tableOrigem in tablesOrigem)
            {
                // Verificar se a tabela existe no destino
                if (tablesDestino.Contains(tableOrigem))
                {
                    string newTabelaDestino = "";
                    int index = tablesDestino.IndexOf(tableOrigem);
                    if (index >= 0)
                    {
                        newTabelaDestino = tablesDestino[index];
                    }
                    mTabelaDestino = newTabelaDestino;
                    mTabelaOrigem = tableOrigem;
                    mView = TextBoxView01.Text;

                    // Obter as colunas da tabela de origem e destino
                    mColumnListOrigem = GetColumns(mConnectionStringOrigem, mTabelaOrigem);
                    mColumnListDestino = GetColumns(mConnectionStringDestino, mTabelaDestino);

                    // Obter as colunas iguais entre a view e a tabela de destino
                    mListColums = ObterColunasIguais(mConnectionStringOrigem, mConnectionStringDestino, mView, mTabelaDestino);
                    StringBuilder sb = new StringBuilder();
                    sb.AppendLine($@"using Core.Con;
using Core.OGlobais;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.BDToClass.{dbOrigem}
{{");



                    sb.AppendLine($@"
    [Serializable]
    public class DB{mTabelaOrigem} : cTabela
    {{
        //Origem
        {ConversorDataTypes(mColumnListOrigem)}

        //DESTINO
        {ConversorDataTypes(mColumnListDestino)}

        public override void SelectFromOrigin(cGlobais pGlobais = null) {{
            LoadGlobais(pGlobais);
            {MainSelect()}
            vDataOrigem.AddParameter(""@IGID"", System.Data.DbType.Int32, ID, dbOrigem);

            DataTable vDataTable = vDataOrigem.GetDataTable(vQuery, dbOrigem, vDataOrigem.GetAddParameter(), out vErro);

            if (!Validate(vDataTable))
                return;

            GetField vReg = new GetField(vDataTable.Rows[0]);
            {GetParameterStringsSelect()}
        }}

        public override void Delete(cGlobais pGlobais = null) {{
            LoadGlobais(vGlobais);
        }}

        protected override bool CheckUpdate() {{ 
            {MainCheckUpdate()}
            vDataDestino.AddParameter(""@IGID"", System.Data.DbType.Int32, IGID, dbDestino);
            DataTable vDataTable = vDataDestino.GetDataTable(vQuery, dbDestino, vDataDestino.GetAddParameter(), out vErro);
            return Validate(vDataTable);
        }}

        protected override void Insert() {{
            {MainInsert()}
            {GetParameterStrings()}
            vDataDestino.ExecCommand(vQuery, dbDestino, Core.Con.cData.CmdQuery.Scalar, vDataDestino.GetAddParameter(), null, out vErro);
        }}

        protected override void Update() {{
            {MainUpdate()}
            {GetParameterStrings()}
            vDataDestino.ExecCommand(vQuery, dbDestino, Core.Con.cData.CmdQuery.Scalar, vDataDestino.GetAddParameter(), null, out vErro);
        }}

        protected override void CreateTable() {{
                
                {MainCreateTable()}

            vDataDestino.ExecCommand(vQuery, dbDestino, Core.Con.cData.CmdQuery.Scalar, vDataDestino.GetAddParameter(), null, out vErro);
        }}

        protected override string GetTableName() {{
            return ""{mTabelaDestino}"";
        }}
    }}");
                    sb.AppendLine("}");

                    try
                    {
                        Directory.CreateDirectory("Classes");
                    }
                    catch (Exception)
                    {
                        // Tratar erros aqui
                    }

                    string fileName = $"DB{mTabelaOrigem}.cs";
                    string indentedCode = TextToCodeFormat(sb.ToString(), 4);
                    File.WriteAllText("Classes\\" + fileName, indentedCode);

                    MessageBox.Show(fileName + " inserido com sucesso!");
                }
            }
        }
    }
}