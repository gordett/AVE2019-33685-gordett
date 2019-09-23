class A {}
class B extends A {}
class C extends B {
    public int x, y;
    public void Foo() {} 
}

class App { 	
	
	public static void main(String args[])
	{
		// PrintBaseTypes("Ola"); 
        // PrintBaseTypes(19);
        // PrintBaseTypes(new C());
        // PrintBaseTypes(new System.IO.DirectoryInfo("."));

        // PrintMembers(new C());
        // PrintMethods(new C()); 
        // PrintFields(new C());
	}
	
	static void PrintMethods(Object obj)
	{
		//Class<?> c = Class.forName(args[0]);
		Class<Object> c = obj.class;
		Method[] methods = c.getDeclaredMethods();
		for (Method method : methods) {
			System.out.println(method.toGenericString());
		}
	} 
 
	static void PrintFields(Object obj)
	{
		Class<Object> c = obj.class;
		Field[] fields = c.getDeclaredFields();
		for (Field field : fields) {
			System.out.println(field.toGenericString()); 
		}
	}

	public static void PrintBaseTypes(object obj)
	{
		Class<Object> c = obj.class
		Class parent = c.getSuperclass();
		while (parent != null) { 
			System.out.println(parent.toGenericString()); 
			parent = parent.getSuperclass(); 
        }
		System.out.println(" ");
	}
	
}