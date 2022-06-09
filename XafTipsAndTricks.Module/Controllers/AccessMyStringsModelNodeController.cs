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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XafTipsAndTricks.Module.ModelUpdaters;

namespace XafTipsAndTricks.Module.Controllers
{
    // For more typical usage scenarios, be sure to check out https://documentation.devexpress.com/eXpressAppFramework/clsDevExpressExpressAppViewControllertopic.aspx.
    public partial class AccessMyStringsModelNodeController : ViewController
    {
        SimpleAction GetNodeValue;
        // Use CodeRush to create Controllers and Actions with a few keystrokes.
        // https://docs.devexpress.com/CodeRushForRoslyn/403133/
        public AccessMyStringsModelNodeController()
        {
            InitializeComponent();
            GetNodeValue = new SimpleAction(this, "Get Node Value", "View");
            GetNodeValue.Execute += GetNodeValue_Execute;
            
            // Target required Views (via the TargetXXX properties) and create their Actions.
        }
        private void GetNodeValue_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            var Value=CaptionHelper.GetLocalizedText(MyModelStrings.MyStringsNodeName, MyModelStrings.HelloWorld, "This is the default message if not is not found");


            MessageOptions options = new MessageOptions();
            options.Duration = 2000;
            options.Message = $"Value of the node:{Value}";
            options.Type = InformationType.Success;
            options.Web.Position = InformationPosition.Right;
            options.Win.Caption = "Success";
            options.Win.Type = WinMessageType.Toast;

            Application.ShowViewStrategy.ShowMessage(options);

        }
        protected override void OnActivated()
        {
            base.OnActivated();
            // Perform various tasks depending on the target View.
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
