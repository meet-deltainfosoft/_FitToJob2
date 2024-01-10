using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Guest_InterviewAssessment : System.Web.UI.Page
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
        }
    }

    public class Assessment
    {
        public int srNo { get; set; }
        public string AssessmentDescription { get; set; }
        public int Points { get; set; }
        public int HR{ get; set; }
        public int HOD { get; set; }
    }

}