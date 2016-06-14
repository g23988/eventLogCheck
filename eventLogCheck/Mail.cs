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
        //站存設定檔
        private Config _config;
        private SmtpClient _mySmtp;
        

        public Mail(Config config) {
            _config = config;
            //設定smtp主機
            _mySmtp = new SmtpClient(config.SMTPserver);
            //設定smtp帳密
            if (config.SMTPauth)
            {
                _mySmtp.Credentials = new System.Net.NetworkCredential(config.SMTPuser, config.SMTPpasswd);
            }
            
        }

        /// <summary>
        /// 發送信件
        /// </summary>
        /// <param name="context">信件內容</param>
        /// <returns>是否成功</returns>
        public bool send(string context) {
            bool check = false;
            try
            {
                string today = DateTime.Now.ToLocalTime().ToString();
                string pcontect = DateTime.Now.ToLocalTime().ToString()+"<hr>";
                pcontect += context.Replace(System.Environment.NewLine, "<br>");
                //設定mail內容
                MailMessage msgMail = new MailMessage();
                //寄件者
                msgMail.From = new MailAddress(_config.SMTPfrom);
                //收件者
                foreach (string mailto_addr in _config.SMTPto)
                {
                    msgMail.To.Add(new MailAddress(mailto_addr));
                }
                //主旨
                msgMail.Subject = _config.SMTPsubject + " " + today;
                //信件內容(含HTML時)
                AlternateView alt = AlternateView.CreateAlternateViewFromString(pcontect, null, "text/html");
                msgMail.AlternateViews.Add(alt);
                //寄mail
                _mySmtp.Send(msgMail);
                check = true;
            }
            catch (Exception)
            {
                check = false;
            }
            
            return check;
        } 
    }
}
