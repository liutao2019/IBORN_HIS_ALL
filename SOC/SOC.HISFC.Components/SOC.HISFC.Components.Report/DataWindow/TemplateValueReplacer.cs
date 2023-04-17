using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Collections;

namespace FS.SOC.HISFC.Components.Report.DataWindow
{
    class TemplateValueReplacer
    {
        /**
     * Replaces variables in the template with values from the passed in map and
     * the global map.
     * 
     * @param template
     * @param map
     * @return
     */
        public static String ReplaceValues(String template, Dictionary<String, Object> map)
        {
            if (HasReplaceableValues(template))
            {
                StringBuilder tempSb = new StringBuilder(template);
                string tempKey=string.Empty;
                foreach (string key in map.Keys)
                {
                    tempKey=string.Concat('&',key);
                    if (template.Contains(tempKey))
                    {
                        if (map[key] != null)
                        {
                            tempSb.Replace(tempKey, map[key].ToString());
                        }
                    }
                }

                return tempSb.ToString();

                //VelocityContext context = new VelocityContext();
                //LoadContextFromMap(context, map);
                //return Evaluate(context, template);
            }
            else
            {
                return template;
            }
        }

        public static bool HasReplaceableValues(String str)
        {
            return ((str != null) && (str.IndexOf("&") > -1));
        }

        public static bool HasSelect(string str)
        {
            return ((str != null) && (str.ToUpper().IndexOf("SELECT") > -1));
        }

        //private static String Evaluate(VelocityContext context, String template)
        //{
        //    StringWriter writer = new StringWriter();

        //    try
        //    {
        //        Velocity.Init();
        //        Velocity.Evaluate(context, writer, "LOG", template);
        //    }
        //    catch (Exception e)
        //    {
        //        return template;
        //    }

        //    return writer.ToString();
        //}

        //private static void LoadContextFromMap(VelocityContext context, Dictionary<String, Object> map)
        //{
        //    foreach (KeyValuePair<String, Object> entry in map)
        //    {
        //        context.Put(entry.Key, entry.Value);
        //    }
        //}

    }
}
