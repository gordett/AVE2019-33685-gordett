using System;
using System.Reflection;
using System.Collections;
using System.Collections.Generic;

public interface IGetter {
    string GetName();
    object GetValue(object target);
}
public class GetterField : IGetter{
    FieldInfo f; 
    public GetterField(FieldInfo f) { this.f = f;}
    public string GetName() { return f.Name; }
    public object GetValue(object target) {
        return f.GetValue(target);
    }
}
public class GetterMethod : IGetter{
    MethodInfo m;
    public GetterMethod(MethodInfo m) {this.m = m;}
    public string GetName() { return m.Name; }
    public object GetValue(object target) { 
        return m.Invoke(target, new object[0]);
    }
}
public class GetterProperty : IGetter{
    PropertyInfo p;
    public GetterProperty(PropertyInfo p) {this.p = p;}
    public string GetName() { return p.Name; }
    public object GetValue(object target) { 
        return p.GetValue(target); 
    }
}

public class Logger {
	
	List<IGetter> getters; 
	bool fields, methods, properties;
	
	public void InitGetters(Type t)
	{
		if(this.fields)
			this.getters.AddRange(InitFields(t));  	
		
		if(this.methods)
			this.getters.AddRange(InitMethods(t));  

		if(this.properties)
			this.getters.AddRange(InitProperties(t));  	 	
	}

    public void Log(object o, bool fields=true, bool methods=true, bool properties=true) {
		
		this.fields=fields;
		this.methods=methods; 
		this.properties=properties;
		
		this.getters = new List<IGetter>(); 
		
        Type t = o.GetType();
        if(t.IsArray) LogArray((IEnumerable) o);
        else {       
			InitGetters(t);
            LogObject(o);
        }
    }
    
    public IEnumerable<IGetter> InitFields(Type t) {
        List<IGetter> l = new List<IGetter>();
        foreach(FieldInfo m in t.GetFields()) {
            l.Add(new GetterField(m));
        }
        return l;
    }
    public List<IGetter> InitMethods(Type t) {
        List<IGetter> l = new List<IGetter>();
        foreach(MethodInfo m in t.GetMethods()) {
            if(m.ReturnType != typeof(void) && m.GetParameters().Length == 0) {
                l.Add(new GetterMethod(m));
            }
        }
        return l;
    }
	public List<IGetter> InitProperties(Type t) {
        List<IGetter> l = new List<IGetter>(); 
        foreach(PropertyInfo p in t.GetProperties()) l.Add(new GetterProperty(p));       
        return l;
    }
    
    public void LogArray(IEnumerable o) {
        Type elemType = o.GetType().GetElementType(); // Tipo dos elementos do Array
		InitGetters(elemType); 
        Console.WriteLine("Array of " + elemType.Name + "[");
        foreach(object item in o) LogObject(item); // *  
        Console.WriteLine("]");
    }
    
    public void LogObject(object o) { 
        Type t = o.GetType();
        Console.Write(t.Name + "{");
        foreach(IGetter g in this.getters) {
            Console.Write(g.GetName() + ": ");
            Console.Write(g.GetValue(o) + ", ");
        }
        Console.WriteLine("}");
    }
}