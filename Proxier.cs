using System;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;

using Proxier.Core;
using BrightIdeasSoftware;
using Proxier.Storage;
using System.Drawing;
using Proxier.Helpers;
using Proxier.Configuration;
using Fiddler;

namespace Proxier
{
    public partial class Proxier : Form
    {
        private Thread ProcMonitorThread { get; set; }
        private ProxierRouter ProxyRouter = new ProxierRouter();

        public Proxier()
        {
            InitializeComponent();

            this.Text = $"Proxier v{Assembly.GetExecutingAssembly().GetName().Version}";
            SerializationHandler.DeserializeConfiguration();
            ProxyRouter.RoutingRequest += (o, e) => RequestsLog.AddObject((e as RoutingEventArgs));
        }

        private void Proxier_Load(object sender, EventArgs e)
        {
            RequestsLog.FullRowSelect = true;
            RequestsLog.UseFiltering = true;

            OLVColumn RequestDateTime = new OLVColumn("Date", "Date")
            {
                AspectGetter = new AspectGetterDelegate((o) =>
                {
                    if (o is RoutingEventArgs r)
                        return r.Dt;

                    return null;
                }),

                //ImageGetter = new ImageGetterDelegate((o) =>
                //{
                //    if (o is RoutingEventArgs r)
                //        return r.Caller.Icon;

                //    return null;
                //}),

                Width = 140,
                Searchable = true,
            };

            OLVColumn RequestProcessName = new OLVColumn("Process", "Process")
            {
                AspectGetter = new AspectGetterDelegate((o) =>
                {
                    if (o is RoutingEventArgs r)
                        return r.Caller.ProcessName;

                    return null;
                }),

                ImageGetter = new ImageGetterDelegate((o) =>
                {
                    if (o is RoutingEventArgs r)
                        return r.Caller.Icon;

                    return null;
                }),

                Width = 160,
                Searchable = true,
            };

            OLVColumn RequestUrl = new OLVColumn("Request", "Request")
            {
                AspectGetter = new AspectGetterDelegate((o) =>
                {
                    if (o is RoutingEventArgs r)
                        return r.Url;

                    return null;
                }),

                Searchable = true,
                FillsFreeSpace = true,
            };

            RequestsLog.Columns.Add(RequestDateTime);
            RequestsLog.Columns.Add(RequestProcessName);
            RequestsLog.Columns.Add(RequestUrl);

            AvailableProcesses.FullRowSelect = true;
            AvailableProcesses.UseFiltering = true;
            AvailableProcesses.UseCellFormatEvents = true;
            AvailableProcesses.FormatCell += new EventHandler<FormatCellEventArgs>((o, args) =>
            {
                if (args.Model is string p)
                {
                    if (ProcessHandler.TryMatchProcess(p, out PProcess oProcess))
                    {
                        if (oProcess.RoutingEnabled && ProxyRouter.IsAttached)
                            args.Item.BackColor = Color.Honeydew;
                        else if (oProcess.RoutingEnabled && !ProxyRouter.IsAttached)
                            args.Item.BackColor = Color.LightYellow;
                        else
                            args.Item.BackColor = Color.White;
                    }
                    else
                        args.Item.BackColor = Color.White;
                }
            });

            OLVColumn ProcessNameCol = new OLVColumn("Process Name", "Process Name")
            {
                AspectGetter = new AspectGetterDelegate((o) =>
                {
                    if (o is string p)
                        if (ProcessHandler.CachedProcesses.ContainsKey(p))
                            return $"{p} ({ProcessHandler.CachedProcesses[p].Values.Count})";

                    return null;
                }),

                ImageGetter = new ImageGetterDelegate((o) =>
                {
                    if (o is string p)
                        if (ProcessHandler.CachedProcesses.ContainsKey(p))
                            return ProcessHandler.CachedProcesses[p].Values.First().Icon;

                    return null;
                }),

                Searchable = true,
                FillsFreeSpace = true,
            };

            OLVColumn ProxierEnabled = new OLVColumn("Proxier", "Proxier")
            {
                AspectGetter = new AspectGetterDelegate((o) =>
                {
                    if (o is string p)
                        if (ProcessHandler.CachedProcesses.ContainsKey(p))
                            return ProcessHandler.CachedProcesses[p].Values.First().RoutingEnabled;

                    return null;
                }),

                AspectPutter = new AspectPutterDelegate((r, o) =>
                {
                    if (o is bool state)
                        if (r is string p)
                            if (ProcessHandler.CachedProcesses.ContainsKey(p))
                                foreach (var process in ProcessHandler.CachedProcesses[p].Values)
                                    process.RoutingEnabled = state;
                }),

                TextAlign = HorizontalAlignment.Center,
                CheckBoxes = true,
                FillsFreeSpace = false,
                Width = 70,
            };

            AvailableProcesses.Columns.Add(ProcessNameCol);
            AvailableProcesses.Columns.Add(ProxierEnabled);
        }

        private void OnProcessesMonitorUpdate(object sender, EventArgs e)
        {
            AvailableProcesses.SetObjects(ProcessHandler.CachedProcesses.Keys, true);

            this.LblInbound.Text = $"{ProxyRouter.InboundTotal.SizeSuffix()}";
            this.LblOutbound.Text = $"{ProxyRouter.OutboundTotal.SizeSuffix()}";
        }

        private void Proxier_Shown(object sender, EventArgs e)
        {
            ProxyRouter.ProxyOnline = ProxyOnline;
            ProxyRouter.ProxyOffline = ProxyOffline;
            ProxyRouter.FiddlerPort = 5656;
            ProxyRouter.Start();

            ProcessHandler.ProcessesUpdated += OnProcessesMonitorUpdate;
            ProcMonitorThread = new Thread(ProcessHandler.Start);
            ProcMonitorThread.Start();
        }

        private void ProxyOffline(object sender, EventArgs e)
        {
            this.BtnMenuModifyState.Text = "Resume";
            this.BtnMenuModifyState.Image = Properties.Resources.Resume20;
            this.LblStatus.Text = "Not Running";
            this.LblStatus.ForeColor = Color.OrangeRed;
            this.AvailableProcesses.RefreshObjects(ProcessHandler.CachedProcesses.Keys.ToArray());
        }

        private void ProxyOnline(object sender, EventArgs e)
        {
            this.BtnMenuModifyState.Text = "Pause";
            this.BtnMenuModifyState.Image = Properties.Resources.Pause20;
            this.LblStatus.Text = "Running";
            this.LblStatus.ForeColor = Color.Green;
            this.AvailableProcesses.RefreshObjects(ProcessHandler.CachedProcesses.Keys.ToArray());
        }

        private void TxtProcessesFilter_TextChanged(object sender, EventArgs e)
        {
            RefreshFilter();
        }

        private void RefreshFilter()
        {
            AvailableProcesses.ModelFilter = new ModelFilter(delegate (object x)
            {
                if (x is string p)
                {
                    if (string.IsNullOrEmpty(TxtProcessesFilter.Text))
                        return true;

                    return p.ToLower().Contains(TxtProcessesFilter.Text.ToLower());
                }
                else
                    return false;
            });
        }

        private void AvailableProcesses_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Space)
            {
                if (AvailableProcesses.SelectedObjects != null)
                {
                    foreach (var selectedObject in AvailableProcesses.SelectedObjects.OfType<string>())
                    {
                        if (ProcessHandler.CachedProcesses.ContainsKey(selectedObject))
                            foreach (var child in ProcessHandler.CachedProcesses[selectedObject])
                                child.Value.RoutingEnabled = !child.Value.RoutingEnabled;

                        AvailableProcesses.RefreshObject(selectedObject);
                    }
                }

                e.Handled = true;
            }
        }

        private void TxtProcessesFilter_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Escape)
            {
                TxtProcessesFilter.Text = "";
                e.Handled = true;
            }
        }

        private void BtnConfigMenu_Click(object sender, EventArgs e)
        {
            using (ConfigurationForm configF = new ConfigurationForm(SerializationHandler.Configuration.ProxyServer))
            {
                if (configF.ShowDialog() == DialogResult.OK)
                {
                    SerializationHandler.Configuration.ProxyServer.IPAddress = configF.IPAddress;
                    SerializationHandler.Configuration.ProxyServer.Port = configF.Port.ToString();
                    SerializationHandler.Configuration.ProxyServer.Username = configF.Username;
                    SerializationHandler.Configuration.ProxyServer.Password = configF.Password;

                    if (ProxyRouter.IsAttached)
                    {
                        ProxyRouter?.Dispose();
                        ProxyRouter.Start();
                    }
                }
            }
        }

        private void BtnMenuModifyState_Click(object sender, EventArgs e)
        {
            if (sender is ToolStripItem btn)
            {
                switch (btn.Text)
                {
                    case "Pause":
                        ProxyRouter?.Dispose();
                        break;
                    case "Resume":
                        ProxyRouter.Start();
                        break;
                }
            }
        }

        private void Proxier_FormClosing(object sender, FormClosingEventArgs e)
        {
            SerializationHandler.SaveCurrent(ProxyRouter);
            SerializationHandler.Flush();
            ProcessHandler.ProcessesUpdated -= OnProcessesMonitorUpdate;
            ProxyRouter?.Dispose();
            try { ProcessHandler.Stop(); } catch { }
            try { ProcMonitorThread.Join(0); } catch { }
            try { ProcMonitorThread.Interrupt(); } catch { }
        }

        private void BtnFileMenu_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
