using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Guest_EducationDetails : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            // Set up sample data for the GridView
            List<EducationDetail> educationDetails = new List<EducationDetail>
            {
                new EducationDetail { EducationLevel = "STD.10" },
                new EducationDetail { EducationLevel = "STD.12" },
                new EducationDetail { EducationLevel = "Graduate/Diploma" },
                new EducationDetail { EducationLevel = "Post Graduate" },
                new EducationDetail { EducationLevel = "ITI/Others" }
            };

            gvEducationDetails.DataSource = educationDetails;
            gvEducationDetails.DataBind();
        }
    }

    public class EducationDetail
    {
        public string EducationLevel { get; set; }
        public string BoardUniversityName { get; set; }
        public string Percentage { get; set; }
        public string PassingYear { get; set; }

    }


}