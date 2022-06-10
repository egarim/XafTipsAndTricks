using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Xpo;
using DevExpress.Xpo;
using DevExpress.Xpo.DB;
using DevExpress.Xpo.Generators;
using DevExpress.Xpo.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System
{
    public static class DeleteExtensions
    {
        public static ModificationResult Delete<T>(this IObjectSpace Os, CriteriaOperator criteria = null) where T : IXPObject
        {
            var XpoOs = Os as XPObjectSpace;
            if (XpoOs == null)
            {
                throw new InvalidOperationException("this extension requires an XpoObjectSpace");
            }
            return XpoOs.Session.Delete<T>(criteria);

        }
        //HACK ZeroSharp blog http://blog.zerosharp.com/fast-batch-deletions-with-devexpress-xpo/
        public static ModificationResult Delete<T>(this Session session, CriteriaOperator criteria = null) where T : IXPObject
        {
            if (ReferenceEquals(criteria, null))
                criteria = CriteriaOperator.Parse("True");

            XPClassInfo classInfo = session.GetClassInfo(typeof(T));
            var batchWideData = new BatchWideDataHolder4Modification(session);
            int recordsAffected = (int)session.Evaluate<T>(CriteriaOperator.Parse("Count()"), criteria);
            //HACK now this overload gets a criteria set
            List<ModificationStatement> collection = DeleteQueryGenerator.GenerateDelete(classInfo, ObjectGeneratorCriteriaSet.GetCommonCriteriaSet(criteria), batchWideData);
            foreach (ModificationStatement item in collection)
            {
                item.RecordsAffected = recordsAffected;
            }

            ModificationStatement[] collectionToArray = collection.ToArray<ModificationStatement>();
            ModificationResult result = session.DataLayer.ModifyData(collectionToArray);
            return result;
        }
    }
}
