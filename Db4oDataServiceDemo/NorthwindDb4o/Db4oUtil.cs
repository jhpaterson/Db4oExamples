using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Db4objects.Db4o;
using Db4objects.Db4o.Query;

namespace NorthwindDb4o
{
    class Db4oUtil
    {
        public static IObjectSet GetAllInstances(IObjectContainer oc, System.Type type)
        {
            IQuery query = oc.Query();
            query.Constrain(type);
            return query.Execute();
        }

        public static Object GetByStringID(IObjectContainer oc, System.Type type, string idField, string id)
        {
            IQuery query = oc.Query();
            query.Constrain(type);
            query.Descend(idField).Constrain(id);
            IObjectSet result = query.Execute();
            if (result.HasNext())
                return result.Next();
            return null;
        }

        public static Object GetByNumericalID(IObjectContainer oc, System.Type type, string idField, long id)
        {
            IQuery query = oc.Query();
            query.Constrain(type);
            query.Descend(idField).Constrain(id);
            IObjectSet result = query.Execute();
            if (result.HasNext())
                return result.Next();
            return null;
        }
    }
}
