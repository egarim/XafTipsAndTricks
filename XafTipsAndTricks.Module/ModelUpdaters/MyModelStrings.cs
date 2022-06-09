using DevExpress.ExpressApp.Model;
using DevExpress.ExpressApp.Model.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevExpress.ExpressApp.Model.NodeGenerators;

namespace XafTipsAndTricks.Module.ModelUpdaters
{
    public class MyModelStrings : ModelNodesGeneratorUpdater<ModelLocalizationNodesGenerator>
    {
        public const string HelloWorld = "Hello Xaf World";
        public const string MyStringsNodeName = "MyStrings";

        public override void UpdateNode(ModelNode node)
        {
            IModelLocalization modelLocalization = (IModelLocalization)node;
            IModelLocalizationGroup MyStrings = modelLocalization[MyStringsNodeName];


            if (MyStrings == null)
            {
                MyStrings = modelLocalization.AddNode<IModelLocalizationGroup>(MyStringsNodeName);
            }
            AddNode(MyStrings, HelloWorld, HelloWorld);
            

        }

        private static void AddNode(IModelLocalizationGroup ParentNodel, string NodeName, string stringValue)
        {
            IModelLocalizationItem Node = (IModelLocalizationItem)ParentNodel[NodeName];
            if (Node == null)
            {
                Node = ParentNodel.AddNode<IModelLocalizationItem>(NodeName);
                Node.Value = stringValue;
            }
        }
    }
}
