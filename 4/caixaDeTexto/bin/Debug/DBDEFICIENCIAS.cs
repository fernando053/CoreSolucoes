
using Core.Con;
using Core.OGlobais;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.BDToClass.CARTESTSMF
{
    [Serializable]
    public class DBDEFICIENCIAS : cTabela
    {
        //Origem
        public int ID { get; set; }
        public int ID_DEFICIENCIAS_SUBGRUPOS { get; set; }
        public string COD_DGV { get; set; }
        public string DESCRICAO { get; set; }
        public string DESC_PALM { get; set; }
        public int GRAU { get; set; }
        public bool REINCIDENCIA { get; set; }
        public int AUMENTA_GRAU { get; set; }
        public bool LIVRETE { get; set; }
        public bool IMPRIME { get; set; }
        public int ID_DEFICIENCIAS_LEGISLACAO { get; set; }
        public int NIVEL_FRASE { get; set; }
        public bool ACTIVO { get; set; }
        public int NIVEL_FRASE_REIN { get; set; }
        public DateTime ULTIMA { get; set; }
        public int ID_FUNCIONARIO { get; set; }
        public bool SEGURO { get; set; }
        public int HITS { get; set; }
        public string COD_IMT { get; set; }
        
        
        //DESTINO
        public int DEFICIENCIAID { get; set; }
        public string DEFICIENCIACD { get; set; }
        public string DEFICIENCIA { get; set; }
        public string GRAU { get; set; }
        public string AFETASEGURANCA { get; set; }
        public string AFETAIDENTIFICACAO { get; set; }
        public string ANOTACAO { get; set; }
        public string REINCIDENCIAS { get; set; }
        public string REINCIDENCIASGRAUS { get; set; }
        public string TIPOCENTRO { get; set; }
        public string INCREMENTAGRAU { get; set; }
        public string IMPLICAREINCIDENCIA { get; set; }
        public DateTime MATRICULADTINICIO { get; set; }
        public DateTime MATRICULADTFIM { get; set; }
        public string EQTPCD { get; set; }
        public DateTime DEFICIENCIADTINI { get; set; }
        public DateTime DEFICIENCIADTFIM { get; set; }
        public string GRUPOCD { get; set; }
        public DateTime COREDTADD { get; set; }
        public DateTime COREDTEDIT { get; set; }
        public string COREUSER { get; set; }
        public string CORETERMINAL { get; set; }
        public string COREACTIVO { get; set; }
        public string CODIGO_IMT { get; set; }
        
        
        public override void SelectFromOrigin(cGlobais pGlobais = null) {
            LoadGlobais(pGlobais);
            
            vQuery = "SELECT * FROM vImporta_Deficiencias WHERE IGID = @IGID";
            
            vDataOrigem.AddParameter("@IGID", System.Data.DbType.Int32, ID, dbOrigem);
            
            DataTable vDataTable = vDataOrigem.GetDataTable(vQuery, dbOrigem, vDataOrigem.GetAddParameter(), out vErro);
            
            if (!Validate(vDataTable))
            return;
            
            GetField vReg = new GetField(vDataTable.Rows[0]);
            DEFICIENCIACD = vReg["DEFICIENCIACD"].ToString();
            DEFICIENCIA = vReg["DEFICIENCIA"].ToString();
            GRAU = vReg["GRAU"].ToString();
            AFETASEGURANCA = vReg["AFETASEGURANCA"].ToString();
            AFETAIDENTIFICACAO = vReg["AFETAIDENTIFICACAO"].ToString();
            ANOTACAO = vReg["ANOTACAO"].ToString();
            REINCIDENCIAS = vReg["REINCIDENCIAS"].ToString();
            REINCIDENCIASGRAUS = vReg["REINCIDENCIASGRAUS"].ToString();
            TIPOCENTRO = vReg["TIPOCENTRO"].ToString();
            INCREMENTAGRAU = vReg["INCREMENTAGRAU"].ToString();
            IMPLICAREINCIDENCIA = vReg["IMPLICAREINCIDENCIA"].ToString();
            MATRICULADTINICIO = vReg["MATRICULADTINICIO"].ToDateTime();
            MATRICULADTFIM = vReg["MATRICULADTFIM"].ToDateTime();
            EQTPCD = vReg["EQTPCD"].ToString();
            DEFICIENCIADTINI = vReg["DEFICIENCIADTINI"].ToDateTime();
            DEFICIENCIADTFIM = vReg["DEFICIENCIADTFIM"].ToDateTime();
            GRUPOCD = vReg["GRUPOCD"].ToString();
            COREDTADD = vReg["COREDTADD"].ToDateTime();
            COREDTEDIT = vReg["COREDTEDIT"].ToDateTime();
            COREUSER = vReg["COREUSER"].ToString();
            CORETERMINAL = vReg["CORETERMINAL"].ToString();
            COREACTIVO = vReg["COREACTIVO"].ToString();
            CODIGO_IMT = vReg["CODIGO_IMT"].ToString();
            
        }
        
        public override void Delete(cGlobais pGlobais = null) {
            LoadGlobais(vGlobais);
        }
        
        protected override bool CheckUpdate() {
            vQuery = "SELECT * FROM INSPDEFICIENCIAS WHERE IGID = @IGID";
            vDataDestino.AddParameter("@IGID", System.Data.DbType.Int32, IGID, dbDestino);
            DataTable vDataTable = vDataDestino.GetDataTable(vQuery, dbDestino, vDataDestino.GetAddParameter(), out vErro);
            return Validate(vDataTable);
        }
        
        protected override void Insert() {
            vQuery = "INSERT INTO INSPDEFICIENCIAS ([DEFICIENCIACD], [DEFICIENCIA], [GRAU], [AFETASEGURANCA], [AFETAIDENTIFICACAO], [ANOTACAO], [REINCIDENCIAS], [REINCIDENCIASGRAUS], [TIPOCENTRO], [INCREMENTAGRAU], [IMPLICAREINCIDENCIA], [MATRICULADTINICIO], [MATRICULADTFIM], [EQTPCD], [DEFICIENCIADTINI], [DEFICIENCIADTFIM], [GRUPOCD], [COREDTADD], [COREDTEDIT], [COREUSER], [CORETERMINAL], [COREACTIVO], [CODIGO_IMT]) VALUES (@DEFICIENCIACD, @DEFICIENCIA, @GRAU, @AFETASEGURANCA, @AFETAIDENTIFICACAO, @ANOTACAO, @REINCIDENCIAS, @REINCIDENCIASGRAUS, @TIPOCENTRO, @INCREMENTAGRAU, @IMPLICAREINCIDENCIA, @MATRICULADTINICIO, @MATRICULADTFIM, @EQTPCD, @DEFICIENCIADTINI, @DEFICIENCIADTFIM, @GRUPOCD, @COREDTADD, @COREDTEDIT, @COREUSER, @CORETERMINAL, @COREACTIVO, @CODIGO_IMT);";
            
            vDataDestino.AddParameter("@DEFICIENCIACD", DbType.String, DEFICIENCIACD, dbDestino);
            vDataDestino.AddParameter("@DEFICIENCIA", DbType.String, DEFICIENCIA, dbDestino);
            vDataDestino.AddParameter("@GRAU", DbType.String, GRAU, dbDestino);
            vDataDestino.AddParameter("@AFETASEGURANCA", DbType.String, AFETASEGURANCA, dbDestino);
            vDataDestino.AddParameter("@AFETAIDENTIFICACAO", DbType.String, AFETAIDENTIFICACAO, dbDestino);
            vDataDestino.AddParameter("@ANOTACAO", DbType.String, ANOTACAO, dbDestino);
            vDataDestino.AddParameter("@REINCIDENCIAS", DbType.String, REINCIDENCIAS, dbDestino);
            vDataDestino.AddParameter("@REINCIDENCIASGRAUS", DbType.String, REINCIDENCIASGRAUS, dbDestino);
            vDataDestino.AddParameter("@TIPOCENTRO", DbType.String, TIPOCENTRO, dbDestino);
            vDataDestino.AddParameter("@INCREMENTAGRAU", DbType.String, INCREMENTAGRAU, dbDestino);
            vDataDestino.AddParameter("@IMPLICAREINCIDENCIA", DbType.String, IMPLICAREINCIDENCIA, dbDestino);
            vDataDestino.AddParameter("@MATRICULADTINICIO", DbType.DateTime, MATRICULADTINICIO, dbDestino);
            vDataDestino.AddParameter("@MATRICULADTFIM", DbType.DateTime, MATRICULADTFIM, dbDestino);
            vDataDestino.AddParameter("@EQTPCD", DbType.String, EQTPCD, dbDestino);
            vDataDestino.AddParameter("@DEFICIENCIADTINI", DbType.DateTime, DEFICIENCIADTINI, dbDestino);
            vDataDestino.AddParameter("@DEFICIENCIADTFIM", DbType.DateTime, DEFICIENCIADTFIM, dbDestino);
            vDataDestino.AddParameter("@GRUPOCD", DbType.String, GRUPOCD, dbDestino);
            vDataDestino.AddParameter("@COREDTADD", DbType.DateTime, COREDTADD, dbDestino);
            vDataDestino.AddParameter("@COREDTEDIT", DbType.DateTime, COREDTEDIT, dbDestino);
            vDataDestino.AddParameter("@COREUSER", DbType.String, COREUSER, dbDestino);
            vDataDestino.AddParameter("@CORETERMINAL", DbType.String, CORETERMINAL, dbDestino);
            vDataDestino.AddParameter("@COREACTIVO", DbType.String, COREACTIVO, dbDestino);
            vDataDestino.AddParameter("@CODIGO_IMT", DbType.String, CODIGO_IMT, dbDestino);
            
            vDataDestino.ExecCommand(vQuery, dbDestino, Core.Con.cData.CmdQuery.Scalar, vDataDestino.GetAddParameter(), null, out vErro);
        }
        
        protected override void Update() {
            vQuery = "UPDATE INSPDEFICIENCIAS SET DEFICIENCIACD = @DEFICIENCIACD,DEFICIENCIA = @DEFICIENCIA,GRAU = @GRAU,AFETASEGURANCA = @AFETASEGURANCA,AFETAIDENTIFICACAO = @AFETAIDENTIFICACAO,ANOTACAO = @ANOTACAO,REINCIDENCIAS = @REINCIDENCIAS,REINCIDENCIASGRAUS = @REINCIDENCIASGRAUS,TIPOCENTRO = @TIPOCENTRO,INCREMENTAGRAU = @INCREMENTAGRAU,IMPLICAREINCIDENCIA = @IMPLICAREINCIDENCIA,MATRICULADTINICIO = @MATRICULADTINICIO,MATRICULADTFIM = @MATRICULADTFIM,EQTPCD = @EQTPCD,DEFICIENCIADTINI = @DEFICIENCIADTINI,DEFICIENCIADTFIM = @DEFICIENCIADTFIM,GRUPOCD = @GRUPOCD,COREDTADD = @COREDTADD,COREDTEDIT = @COREDTEDIT,COREUSER = @COREUSER,CORETERMINAL = @CORETERMINAL,COREACTIVO = @COREACTIVO,CODIGO_IMT = @CODIGO_IMT WHERE IGID = @IGID";
            vDataDestino.AddParameter("@DEFICIENCIACD", DbType.String, DEFICIENCIACD, dbDestino);
            vDataDestino.AddParameter("@DEFICIENCIA", DbType.String, DEFICIENCIA, dbDestino);
            vDataDestino.AddParameter("@GRAU", DbType.String, GRAU, dbDestino);
            vDataDestino.AddParameter("@AFETASEGURANCA", DbType.String, AFETASEGURANCA, dbDestino);
            vDataDestino.AddParameter("@AFETAIDENTIFICACAO", DbType.String, AFETAIDENTIFICACAO, dbDestino);
            vDataDestino.AddParameter("@ANOTACAO", DbType.String, ANOTACAO, dbDestino);
            vDataDestino.AddParameter("@REINCIDENCIAS", DbType.String, REINCIDENCIAS, dbDestino);
            vDataDestino.AddParameter("@REINCIDENCIASGRAUS", DbType.String, REINCIDENCIASGRAUS, dbDestino);
            vDataDestino.AddParameter("@TIPOCENTRO", DbType.String, TIPOCENTRO, dbDestino);
            vDataDestino.AddParameter("@INCREMENTAGRAU", DbType.String, INCREMENTAGRAU, dbDestino);
            vDataDestino.AddParameter("@IMPLICAREINCIDENCIA", DbType.String, IMPLICAREINCIDENCIA, dbDestino);
            vDataDestino.AddParameter("@MATRICULADTINICIO", DbType.DateTime, MATRICULADTINICIO, dbDestino);
            vDataDestino.AddParameter("@MATRICULADTFIM", DbType.DateTime, MATRICULADTFIM, dbDestino);
            vDataDestino.AddParameter("@EQTPCD", DbType.String, EQTPCD, dbDestino);
            vDataDestino.AddParameter("@DEFICIENCIADTINI", DbType.DateTime, DEFICIENCIADTINI, dbDestino);
            vDataDestino.AddParameter("@DEFICIENCIADTFIM", DbType.DateTime, DEFICIENCIADTFIM, dbDestino);
            vDataDestino.AddParameter("@GRUPOCD", DbType.String, GRUPOCD, dbDestino);
            vDataDestino.AddParameter("@COREDTADD", DbType.DateTime, COREDTADD, dbDestino);
            vDataDestino.AddParameter("@COREDTEDIT", DbType.DateTime, COREDTEDIT, dbDestino);
            vDataDestino.AddParameter("@COREUSER", DbType.String, COREUSER, dbDestino);
            vDataDestino.AddParameter("@CORETERMINAL", DbType.String, CORETERMINAL, dbDestino);
            vDataDestino.AddParameter("@COREACTIVO", DbType.String, COREACTIVO, dbDestino);
            vDataDestino.AddParameter("@CODIGO_IMT", DbType.String, CODIGO_IMT, dbDestino);
            
            vDataDestino.ExecCommand(vQuery, dbDestino, Core.Con.cData.CmdQuery.Scalar, vDataDestino.GetAddParameter(), null, out vErro);
        }
        
        protected override void CreateTable() {
            
            vQuery = "CREATE TABLE INSPDEFICIENCIAS ([DEFICIENCIACD] varchar(50) NULL, [DEFICIENCIA] varchar(500) NULL, [GRAU] char(1) NULL, [AFETASEGURANCA] char(1) NULL, [AFETAIDENTIFICACAO] char(1) NULL, [ANOTACAO] varchar(250) NULL, [REINCIDENCIAS] char(1) NULL, [REINCIDENCIASGRAUS] char(2) NULL, [TIPOCENTRO] varchar(50) NULL, [INCREMENTAGRAU] char(1) NULL, [IMPLICAREINCIDENCIA] char(1) NULL, [MATRICULADTINICIO] datetime2 NULL, [MATRICULADTFIM] datetime2 NULL, [EQTPCD] char(50) NULL, [DEFICIENCIADTINI] datetime2 NULL, [DEFICIENCIADTFIM] datetime2 NULL, [GRUPOCD] varchar(50) NULL, [COREDTADD] datetime2 NULL, [COREDTEDIT] datetime2 NULL, [COREUSER] varchar(250) NULL, [CORETERMINAL] varchar(250) NULL, [COREACTIVO] char(1) NULL, [CODIGO_IMT] varchar(50) NULL, ) ON [PRIMARY]";
            
            vDataDestino.ExecCommand(vQuery, dbDestino, Core.Con.cData.CmdQuery.Scalar, vDataDestino.GetAddParameter(), null, out vErro);
        }
        
        protected override string GetTableName() {
            return "INSPDEFICIENCIAS";
        }
    }
}