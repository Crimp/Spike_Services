using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

public class ODataDemoServiceOptions {
    public static Assembly Assembly {
        get;
        set;
    }
    public static string ConnectionString {
        get;
        set;
    }
    public static string NamespaceName {
        get;
        set;
    }
}
