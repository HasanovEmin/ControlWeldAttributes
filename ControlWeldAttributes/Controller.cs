using Aveva.Core.Database;
using System;
namespace ControlWeldAttributes
{
    public static class Controller
    {
        public static DbAttribute WeldPositionFlag { get; set; }
        public static DbAttribute WeldPosition { get; set; }
        public static void Run()
        {
            WeldPositionFlag = DbAttribute.GetDbAttribute(":WeldPositionFlag");
            WeldPosition = DbAttribute.GetDbAttribute(":WeldPosition");
            if (WeldPositionFlag == null || WeldPosition == null)
            {
                return;
            }

            DbPostElementChange.AddPostAttributeChange(DbAttributeInstance.POS, new DbPostElementChange.PostAttributeChangeDelegate(PositionChanged));
            DbPostElementChange.AddPostAttributeChange(DbAttribute.GetDbAttribute(":WeldPositionFlag"), new DbPostElementChange.PostAttributeChangeDelegate(WeldPositionFlagChanged));

        }
        private static void WeldPositionFlagChanged(DbElement ele, DbAttribute att)
        {
            if (ele.ElementType != DbElementTypeInstance.WELD)
                return;

            var flag = ele.GetBool(att);
            try
            {
                if (flag == true)
                {
                    var position = ele.GetAsString(DbAttributeInstance.POS);
                    ele.SetAttribute(WeldPosition, position);
                }
                else
                {
                    ele.SetAttribute(WeldPosition, "");
                }
            }
            catch (Exception)
            {

            }

        }

        private static void PositionChanged(DbElement ele, DbAttribute att)
        {
            if (ele.ElementType != DbElementTypeInstance.WELD)
                return;

            var flag = ele.GetBool(WeldPositionFlag);
            try
            {
                if (flag == true)
                {
                    var position = ele.GetAsString(att);
                    ele.SetAttribute(WeldPosition, position);
                }
            }
            catch (Exception)
            {

            }


        }




    }
}
