using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Layout;
using DevExpress.ExpressApp.Model.NodeGenerators;
using DevExpress.ExpressApp.SystemModule;
using DevExpress.ExpressApp.Templates;
using DevExpress.ExpressApp.Utils;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XafTipsAndTricks.Module.BusinessObjects;

namespace XafTipsAndTricks.Module.Controllers
{
    // For more typical usage scenarios, be sure to check out https://documentation.devexpress.com/eXpressAppFramework/clsDevExpressExpressAppViewControllertopic.aspx.
    public partial class FastDeleteController : ViewController
    {
        SimpleAction GenerateData;
        SimpleAction FastDeleteAction;
        // Use CodeRush to create Controllers and Actions with a few keystrokes.
        // https://docs.devexpress.com/CodeRushForRoslyn/403133/
        public FastDeleteController()
        {
            InitializeComponent();
            FastDeleteAction = new SimpleAction(this, "Fast Delete", "View");
            FastDeleteAction.Execute += FastDeleteAction_Execute;


            GenerateData = new SimpleAction(this, "Generate Records", "View");
            GenerateData.Execute += GenerateData_Execute;
            
            // Target required Views (via the TargetXXX properties) and create their Actions.
        }
        private void GenerateData_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            for (int i = 0; i < 1000; i++)
            {
                this.View.ObjectSpace.CreateObject<Customer>().Name = Guid.NewGuid().ToString();
            }
            this.View.ObjectSpace.CommitChanges();
            // Execute your business logic (https://docs.devexpress.com/eXpressAppFramework/112737/).
        }
        protected override void OnActivated()
        {
            base.OnActivated();
            
            // Perform various tasks depending on the target View.
        }
        private void FastDeleteAction_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            var StartTime = DateTime.Now;
          this.ObjectSpace.Delete<Customer>(new BinaryOperator("Active", true));
            var TotalTime=(DateTime.Now -StartTime).TotalMilliseconds;
        

            MessageOptions options = new MessageOptions();
            options.Duration = 4000;
            options.Message = $"Records deleted in {TotalTime} milliseconds";
            options.Type = InformationType.Success;
            options.Web.Position = InformationPosition.Right;
            options.Win.Caption = "Success";
            options.Win.Type = WinMessageType.Toast;

            Application.ShowViewStrategy.ShowMessage(options);
        }
        protected override void OnViewControlsCreated()
        {
            base.OnViewControlsCreated();
            // Access and customize the target View control.
        }
        protected override void OnDeactivated()
        {
            // Unsubscribe from previously subscribed events and release other references and resources.
            base.OnDeactivated();
        }
    }
}
