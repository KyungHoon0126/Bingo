using System;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;

namespace Bingo.Utils
{
    public class Notifier
    {
        private NotifyIcon NotifyIcon;

        public Notifier()
        {
            NotifyIcon = new NotifyIcon();
            NotifyIcon.Icon = Icon.ExtractAssociatedIcon(Assembly.GetExecutingAssembly().Location);
            NotifyIcon.BalloonTipClosed += NotifyIcon_BalloonTipClosed;
        }

        private void NotifyIcon_BalloonTipClosed(object sender, EventArgs e)
        {
            NotifyIcon.Visible = false;
        }

        public void ShowNotifyMessage(string tipTitle, string tipMessage)
        {
            NotifyIcon.Visible = true;
            NotifyIcon.ShowBalloonTip(300, tipTitle, tipMessage, ToolTipIcon.Warning);
        }
    }
}
