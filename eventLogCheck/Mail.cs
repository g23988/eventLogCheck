using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Mail;

namespace eventLogCheck
{
    /// <summary>
    /// 寄送mail相關函數
    /// </summary>
    class Mail
    {
        public Mail(Config config) {
            //設定smtp主機
            SmtpClient mySmtp = new SmtpClient();
            //設定smtp帳密
            mySmtp.Credentials = new System.Net.NetworkCredential("", "password");
            //信件內容
            string pcontect = "string or html";
            //設定mail內容
            MailMessage msgMail = new MailMessage();
            //寄件者
            msgMail.From = new MailAddress("sys@hinet.net");
            //收件者
            msgMail.To.Add("user@hinet.net");
            //主旨
            msgMail.Subject = "信件主旨";
            //信件內容(含HTML時)
            AlternateView alt = AlternateView.CreateAlternateViewFromString(pcontect, null, "text/html");
            msgMail.AlternateViews.Add(alt);
            //寄mail
            mySmtp.Send(msgMail);
        }
    }
}
