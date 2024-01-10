using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

public partial class Exams_InterviewAssessment : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            List<Assessment> educationDetails = new List<Assessment>
            {
                new Assessment { srNo =1 ,AssessmentDescription = "Required Qualification",Points =20 },
                 new Assessment { srNo =2 ,AssessmentDescription = "Department & Designation Specific Skill",Points =30 },
                 new Assessment { srNo =3 ,AssessmentDescription = "Related Work Experiance",Points =20 },
                 new Assessment { srNo =4 ,AssessmentDescription = "Communication Skill & Computer Literacy",Points =10 },
                 new Assessment { srNo =5 ,AssessmentDescription = "Personality & Body Language",Points =5 },
                 new Assessment { srNo =6 ,AssessmentDescription = "Leardership/Team Work Traits",Points =5 },
                 new Assessment { srNo =7 ,AssessmentDescription = "Decision Skill",Points =5 },
                 new Assessment { srNo =8 ,AssessmentDescription = "Discipline, Work and Life Values",Points =5 }
            };
            gridPoints.DataSource = educationDetails;
            gridPoints.DataBind();
            GetDropdown();
        }
    }

    public class Assessment
    {
        public int srNo { get; set; }
        public string AssessmentDescription { get; set; }
        public int Points { get; set; }
        public int HR { get; set; }
        public int HOD { get; set; }
    }

    protected void lnkBtnSubmit_click(object sender, EventArgs e)
    {
        try
        {
            bool IsValidated = true;
            DataTable AssessmentTable = new DataTable();
            AssessmentTable.Columns.Add("srNo", typeof(int));
            AssessmentTable.Columns.Add("Description", typeof(string));
            AssessmentTable.Columns.Add("Points", typeof(int));
            AssessmentTable.Columns.Add("HrPoint", typeof(int));
            // AssessmentTable.Columns.Add("HOD", typeof(int));

            foreach (GridViewRow row in gridPoints.Rows)
            {
                Label lblsrNo = (Label)row.FindControl("lblsrNo");
                Label lblAssessmentDescription = (Label)row.FindControl("lblAssessmentDescription");
                TextBox txtPoints = (TextBox)row.FindControl("txtPoints");
                TextBox txtHR = (TextBox)row.FindControl("txtHR");
                //TextBox txtHOD = (TextBox)row.FindControl("txtHOD");

                if (txtPoints.Text.Trim() == "" || txtHR.Text.Trim() == "")
                {
                    IsValidated = false;
                }
                else
                {
                    DataRow dataRow = AssessmentTable.NewRow();
                    dataRow["srNo"] = lblsrNo.Text;
                    dataRow["Description"] = lblAssessmentDescription.Text;
                    dataRow["Points"] = txtPoints.Text;
                    dataRow["HrPoint"] = txtHR.Text;
                    AssessmentTable.Rows.Add(dataRow);
                }
            }
            if (IsValidated == false)
            {
            }
            else
            {
                SqlCommand sqlCmd = new SqlCommand();
                GeneralDAL objDal = new GeneralDAL();
                objDal.OpenSQLConnection();
                sqlCmd.Connection = objDal.ActiveSQLConnection();
                sqlCmd.CommandType = CommandType.StoredProcedure;
                sqlCmd.CommandText = "FitToJob_Master_InterviewAssessment";
                sqlCmd.Parameters.AddWithValue("@Action", "Insert_Interview_Assessment");
                SqlParameter tvpParam = new SqlParameter("@UT_InterviewAssessment", SqlDbType.Structured);
                tvpParam.Value = AssessmentTable;
                tvpParam.TypeName = "dbo.UT_InterviewAssessment";
                sqlCmd.Parameters.Add(tvpParam);
                SqlDataAdapter dataAdapter = new SqlDataAdapter(sqlCmd);
                DataSet dataSet = new DataSet();
                dataAdapter.Fill(dataSet);
                objDal.CloseSQLConnection();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error: " + ex.Message);
        }
    }

    protected void GetDropdown()
    {
        try
        {
            SqlCommand sqlCmd = new SqlCommand();
            GeneralDAL objDal = new GeneralDAL();
            objDal.OpenSQLConnection();
            sqlCmd.Connection = objDal.ActiveSQLConnection();
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.CommandText = "FitToJob_Master_InterviewAssessment";
            sqlCmd.Parameters.AddWithValue("@Action", "GetDropdown");
            SqlDataAdapter dataAdapter = new SqlDataAdapter(sqlCmd);
            DataSet dataSet = new DataSet();
            dataAdapter.Fill(dataSet);
            if (dataSet.Tables[0].Rows.Count > 0)
            {
                DDLCandidate.DataSource = dataSet.Tables[0];
                DDLCandidate.DataTextField = "Candidate";
                DDLCandidate.DataValueField = "RegistrationId";
                DDLCandidate.DataBind();
            }
            objDal.CloseSQLConnection();
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error: " + ex.Message);
        }
    }

    protected void GetDetailsById(Guid UserId)
    {
        try
        {
            SqlCommand sqlCmd = new SqlCommand();
            GeneralDAL objDal = new GeneralDAL();
            objDal.OpenSQLConnection();
            sqlCmd.Connection = objDal.ActiveSQLConnection();
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.CommandText = "FitToJob_Master_InterviewAssessment";
            sqlCmd.Parameters.AddWithValue("@Action", "GetDetailsById");
            sqlCmd.Parameters.AddWithValue("@UserId", UserId);
            SqlDataAdapter dataAdapter = new SqlDataAdapter(sqlCmd);
            DataSet dataSet = new DataSet();
            dataAdapter.Fill(dataSet);
            if (dataSet.Tables[0].Rows.Count > 0)
            {
                gridPoints.DataSource = dataSet.Tables[0];
                gridPoints.DataBind();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error: " + ex.Message);
        }
    }
}