1， Metadata usage：

      不用像C/C++那样，需要头文件，所有信息都在library文件里面
    
      Visual studio使用metadata做智能感知

GC会使用metadata来判断对象间的关联

An assembly’s modules also include information about referenced assemblies (including their version numbers). This information makes an assembly self-describing


Chapter 5:

Value types are derived from System.ValueType. This type offers the same methods as defined by System.Object. However, System.ValueType overrides the Equals
method so that it returns true if the values of the two objects’ fields match. In addition, System.ValueType overrides the GetHashCode method to produce a hash code value by using an algorithm that takes into account the values in the object’s instance fields. Due to performance issues with this default implementation, when defining your own value types, you should override and provide explicit implementations for the Equals and GetHashCode methods.


You tell the CLR what to do by applying the System.Runtime.InteropServices. StructLayoutAttribute attribute on the class or structure you’re defining. To this
attribute’s constructor, you can pass LayoutKind.Auto to have the CLR arrange the fields, LayoutKind.Sequential to have the CLR preserve your field layout, or
LayoutKind.Explicit to explicitly arrange the fields in memory by using offsets. If you don’t explicitly specify the StructLayoutAttribute on a type that you’re defining,
your compiler selects whatever layout it determines is best. You should be aware that Microsoft’s C# compiler selects LayoutKind.Auto for reference
types (classes) and LayoutKind.Sequential for value types (structures). It is obvious that the C# compiler team believes that structures are commonly used when
interoperating with unmanaged code, and for this to work, the fields must stay in the order defined by the programmer.


It’s possible to convert a value type to a reference type by using a mechanism called boxing.
Internally, here’s what happens when an instance of a value type is boxed:
1. Memory is allocated from the managed heap. The amount of memory allocated is the size required by the value type’s fields plus the two additional overhead members (the type object pointer and the sync block index) required by all objects on the managed heap.
2. The value type’s fields are copied to the newly allocated heap memory.
3. The address of the object is returned. This address is now a reference to an object; the value type is now a reference type.


Internally, here’s exactly what happens when a boxed value type instance is unboxed:
1. If the variable containing the reference to the boxed value type instance is null, a NullReferenceException is thrown.
2. If the reference doesn’t refer to an object that is a boxed instance of the desired value type, an InvalidCastException is thrown


Do not confuse dynamic and var. Declaring a local variable using var is just a syntactical shortcut that has the compiler infer the specific data type from an expression. The
var keyword can be used only for declaring local variables inside a method while the dynamic keyword can be used for local variables, fields, and arguments. You cannot cast an expression to var but you can cast an expression to dynamic. You must explicitly initialize a variable declared using var while you do not have to initialize a variable declared with dynamic


Chapter 8

If the class is abstract, the compiler-produced default constructor has protected accessibility; otherwise, the constructor is given public accessibility. If the base class doesn’t offer a parameterless constructor, the derived class must explicitly call a base class constructor or the compiler will issue an error. If the class is static (sealed and abstract), the compiler will not emit a default constructor at all into the class definition.


In a few situations, an instance of a type can be created without an instance constructor being called. In particular, calling Object’s MemberwiseClone method allocates memory, initializes the object’s overhead fields, and then copies the source object’s bytes to the new object. Also, a constructor is usually not called when deserializing an object with the runtime serializer. The deserialization code allocates memory for the object without calling a constructor using the System.Runtime.Serialization.FormatterServices type's GetUninitializedObject or GetSafeUninitializedObject methods


You should not call any virtual methods within a constructor that can affect the object being constructed. The reason is if the virtual method is overridden in the type being
instantiated, the derived type’s implementation of the overridden method will execute, but all of the fields in the hierarchy have not been fully initialized. Calling a virtual method would therefore result in unpredictable behavior.


For performance reasons, the CLR doesn’t attempt to call a constructor for each value type field contained within the reference type. But as I mentioned earlier, the
fields of the value types are initialized to 0/null


value types don’t actually even need to have a constructor defined within them, and the C# compiler doesn't emit default parameterless constructors for value types.

The CLR does allow you to define constructors on value types. The only way that these constructors will execute is if you write code to explicitly call one of them,


C# doesn’t allow a value type to define a parameterless constructor. The C# compiler produces the following message when attempting to compile that code: "error CS0568: Structs cannot contain explicit parameterless constructors." C# purposely disallows value types from defining parameterless constructors to remove any
confusion a developer might have about when that constructor gets called.

Because C# doesn’t allow value types with parameterless constructors, compiling the following type produces the following message: "error CS0573: 'SomeValType.m_x': cannot have instance field initializers in structs."
internal struct SomeValType {
// You cannot do inline instance field initialization in a value type
private Int32 m_x = 5;
}


Within a single thread, there is a potential problem that can occur if two type constructors contain code that reference each other. For example, ClassA has a type constructor containing code that references ClassB, and ClassB has a type constructor containing code that references ClassA. In this situation, the CLR still guarantees that each type constructor’s code executes only once; however, it cannot guarantee that ClassA’s type constructor code has run to completion before executing ClassB’s type constructor. You should certainly try to avoid writing code that sets up this scenario. In fact, since the CLR is responsible for calling type constructors, you should  always avoid writing any code that requires type constructors to be called in a specific order.


Chapter 10

Encapsulating the data as shown earlier has two disadvantages.First, you have to write more code because you now have to implement additional methods.Second, users of the type must now call methods rather than simply refer to a single field name. 

e.SetName("Jeffrey Richter");  // updates the employee's name  
String EmployeeName = e.GetName(); // retrieves the employee's name  
e.SetAge(41);  // Updates the employee's age 
e.SetAge(-5);  // Throws ArgumentOutOfRangeException 
Int32 EmployeeAge = e.GetAge();  // retrieves the employee's age
Personally, I think these disadvantages are quite minor.Nevertheless, programming languages and the CLR offer a mechanism called propertiesthat alleviates the first disadvantage a little and removes the second disadvantage entirely. 


When you define a property, depending on its definition, the compiler will emit either two or three of the following items into the resulting managed assembly: 

A method representing the property’s getaccessor method.This is emitted only if you define a getaccessor method for the property. 

A method representing the property’s setaccessor method.This is emitted only if you define a setaccessor method for the property. 

A property definition in the managed assembly’s metadata.This is always emitted.


If you are creating a property to simply encapsulate a backing field, then C# offers a simpli-fied syntax known as automatically implemented properties(AIPs), as shown here for the Name property:
public sealed class Employee { 
// This property is an automatically implemented property
public String Name { get; set; } 

... ...


When you declare a property and do not provide an implementation for the get/setmeth-ods, then the C# compiler will automatically declare for you a private field.In this example, the field will be of type String, the type of the property.And, the compiler will automatically implement the get_Nameand set_Namemethods for you to return the value in the field and to set the field’s value, respectively.


Now, let’s focus on what the compiler is actually doing.When you write a line of code like this:
var o = new { property1 = expression1, ..., propertyN = expressionN };
the compiler infers the type of each expression, creates private fields of these inferred types, creates public read-only properties for each of the fields, and creates a constructor that  accepts all these expressions.The constructor’s code initializes the private read-only fields from the expression results passed in to it.In addition, the  compiler overrides Object’s Equals, GetHashCode, and ToStringmethods and generates code inside all these methods. In effect, the class that the compiler generates  looks like this: 

[CompilerGenerated]
internal sealed class <>f__AnonymousType0<...>: Object {
private readonly t1 f1;
public t1 p1 { get { return f1; } }
...
private readonly tn fn;
public tn pn { get { return fn; } }
public <>f__AnonymousType0<...>(t1 a1, ..., tn an) { 
f1 = a1; ...; fn = an; // Set all fields
}
public override Boolean Equals(Object value) {
// Return false if any fields don't match; else true
}
public override Int32 GetHashCode() {
// Returns a hash code generated from each fields' hash code
}
public override String ToString() {
// Return comma-separated set of property name = value pairs
}
}


The main reason why properties cannot introduce their own generic type parameters is because they don’t make sense conceptually.A property is supposed to represent a characteristic of an object that can be queried or set.Introducing a generic type parameter would mean that the behavior of the querying/setting could be changed, but conceptually, a property is not supposed to have behavior.If you want your object to expose some behavior—generic or not—define a method, not a property.


Chapter 11

The common language runtime’s (CLR’s) event model is based on delegates.A delegate is a type-safe way to invoke a callback method.Callback methods are the means by which objects receive the notifications they subscribed to.


To efficiently store event delegates, each object that exposes events will maintain a collection (usually a dictionary) with some sort of event identifier as the key and a delegate list as the value.When a new object is constructed, this collection is empty.When interest in an event is registered, the event’s identifier is looked up in the collection.If the event identifier is there, the new delegate is combined with the list of delegates for this event.If the event identifier isn’t in the collection, the event identifier is added with the delegate.


Chapter 12

The CLR supports generic delegates to ensure that any type of object can be passed to a callback method in a type-safe way.Furthermore, generic delegates allow a value type instance to be passed to a callback method without any boxing.

（注： 协变是指调用函数的返回类型是 委托的返回类型的子类， 抗变就是指 实际函数的参数用的是子类，但是委托是父类的参数）

A generic type parameter can be any one of the following:
Invariant  Meaning that that generic type parameter cannot be changed.I have shown only invariant generic type parameters so far in this chapter.
Contravariant  Meaning that the generic type parameter can change from a class to a class derived from it.In C#, you indicate contravariant generic type parameters with the inkeyword.Contravariant generic type parameters can appear only in input positions such as a method’s argument.
Covariant  Meaning that the generic type argument can change from a class to one of its base classes.In C#, you indicate covariant generic type parameters with the outkey-word.Covariant generic type parameters can appear only in output positions such as a method’s return type.


Chapter 26

When a thread calls the Wait method, the system checks if the Task that the thread is waiting for has started executing. If it has, then the thread calling Wait will block until the Task has completed running. But if the Task has not started executing yet, then the system may (depending on the TaskScheduler) execute the Task using the thread that called Wait. If this happens, then the thread calling Wait does not block; it executes the Task and returns immediately. This is good in that no thread has blocked, thereby reducing resource usage (by not creating a thread to replace the blocked thread) while improving performance (no time is spent to create a thread and there is no context switching). But it can also be bad if, for example, the thread has taken a thread synchronization lock before calling Wait and then the Task tries to take the same lock, resulting in a deadlocked thread!

---------------------
作者：tyj1982 
来源：CSDN 
原文：https://blog.csdn.net/tyj1982/article/details/8074423 
版权声明：本文为博主原创文章，转载请附上博文链接！