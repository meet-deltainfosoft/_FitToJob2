using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mail;

/// <summary>
/// Summary description for EmailClass
/// </summary>
public class EmailClass
{
    public static bool SendEmail
        (
           string pEmailServer, int pEmailPort,
               string pEmailEmail,
           string pEmailPassword,
           string pTo,
               string pCC,
               string pBCC,
           string pSubject,
           string pBody,
           System.Web.Mail.MailFormat pFormat,
           string pAttachmentPath)
    {
        try
        {
            System.Web.Mail.MailMessage myMail = new System.Web.Mail.MailMessage();
            myMail.Fields.Add("http://schemas.microsoft.com/cdo/configuration/smtpserver", pEmailServer);
            myMail.Fields.Add("http://schemas.microsoft.com/cdo/configuration/smtpserverport", pEmailPort);
            myMail.Fields.Add("http://schemas.microsoft.com/cdo/configuration/sendusing", "2");
            //sendusing: cdoSendUsingPort, value 2, for sending the message using 
            //the network.

            //smtpauthenticate: Specifies the mechanism used when authenticating 
            //to an SMTP 
            //service over the network. Possible values are:
            //- cdoAnonymous, value 0. Do not authenticate.
            //- cdoBasic, value 1. Use basic clear-text authentication. 
            //When using this option you have to provide the user name and password 
            //through the sendusername and sendpassword fields.
            //- cdoNTLM, value 2. The current process security context is used to 
            // authenticate with the service.
            myMail.Fields.Add("http://schemas.microsoft.com/cdo/configuration/smtpauthenticate", "1");
            //Use 0 for anonymous
            myMail.Fields.Add("http://schemas.microsoft.com/cdo/configuration/sendusername", pEmailEmail);
            myMail.Fields.Add("http://schemas.microsoft.com/cdo/configuration/sendpassword", pEmailPassword);
            myMail.Fields.Add("http://schemas.microsoft.com/cdo/configuration/smtpusessl", "true");
            myMail.From = pEmailEmail;
            myMail.To = pTo;

            if (pCC.ToString().Length > 0)
                myMail.Cc = pCC;
            if (pBCC.ToString().Length > 0)
                myMail.Bcc = pBCC;

            myMail.Subject = pSubject;
            myMail.BodyFormat = pFormat;
            myMail.Body = pBody;
            if (pAttachmentPath.Trim() != "")
            {
                MailAttachment MyAttachment =
                        new MailAttachment(pAttachmentPath);
                myMail.Attachments.Add(MyAttachment);
                myMail.Priority = System.Web.Mail.MailPriority.High;
            }

            System.Web.Mail.SmtpMail.SmtpServer = pEmailServer + ":" + pEmailPort.ToString().Replace(".00", "");
            System.Web.Mail.SmtpMail.Send(myMail);
            return true;
        }
        catch (Exception ex)
        {
            throw;
        }
    }
}