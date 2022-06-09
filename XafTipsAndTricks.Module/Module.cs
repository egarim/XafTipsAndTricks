using System;
using System.Text;
using System.Linq;
using DevExpress.ExpressApp;
using System.ComponentModel;
using DevExpress.ExpressApp.DC;
using System.Collections.Generic;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.BaseImpl.PermissionPolicy;
using DevExpress.ExpressApp.Model;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Updating;
using DevExpress.ExpressApp.Model.Core;
using DevExpress.ExpressApp.Model.DomainLogics;
using DevExpress.ExpressApp.Model.NodeGenerators;
using DevExpress.Xpo;
using DevExpress.ExpressApp.Xpo;

namespace XafTipsAndTricks.Module {
    // For more typical usage scenarios, be sure to check out https://docs.devexpress.com/eXpressAppFramework/DevExpress.ExpressApp.ModuleBase.
    public sealed partial class XafTipsAndTricksModule : ModuleBase {
        public XafTipsAndTricksModule() {
            InitializeComponent();
        }
        public override IEnumerable<ModuleUpdater> GetModuleUpdaters(IObjectSpace objectSpace, Version versionFromDB) {
            ModuleUpdater updater = new DatabaseUpdate.Updater(objectSpace, versionFromDB);
            return new ModuleUpdater[] { updater };
        }
        public override void Setup(XafApplication application) {
            base.Setup(application);
            // Manage various aspects of the application UI and behavior at the module level.
        }
        public override void CustomizeTypesInfo(ITypesInfo typesInfo)
        {
            base.CustomizeTypesInfo(typesInfo);
            CalculatedPersistentAliasHelper.CustomizeTypesInfo(typesInfo);
            AddDomainClassOptionsAttribute(typesInfo);

            ExtendedPersistentClass(typesInfo);

        }

        private static void ExtendedPersistentClass(ITypesInfo typesInfo)
        {
            TypeInfo ClassWithoutAttributesTypeInfo = typesInfo.FindTypeInfo("XafTipsAndTricks.Module.BusinessObjects.ClassWithoutAttributes") as TypeInfo;
            if (typesInfo != null)
            {
                ClassWithoutAttributesTypeInfo.CreateMember("LastName", typeof(string));
                ClassWithoutAttributesTypeInfo.CreateMember("Age", typeof(int)).AddAttribute(new ImmediatePostDataAttribute());
                ClassWithoutAttributesTypeInfo.CreateMember("FullName", typeof(string), "Name + LastName");
            }
        }

        private static void AddDomainClassOptionsAttribute(ITypesInfo typesInfo)
        {
            ITypeInfo ClassWithoutAttributesTypeInfo = typesInfo.FindTypeInfo("XafTipsAndTricks.Module.BusinessObjects.ClassWithoutAttributes");
            if (ClassWithoutAttributesTypeInfo != null)
            {
                ClassWithoutAttributesTypeInfo.AddAttribute(new DefaultClassOptionsAttribute());
            }
        }
    }
}
